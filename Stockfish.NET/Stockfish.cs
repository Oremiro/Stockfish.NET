using System;
using System.Collections.Generic;
using System.Linq;
using Stockfish.NET.Models;

namespace Stockfish.NET
{

    public class Stockfish : IStockfish
    {
        #region private variables

        private const int MAX_TRIES = 1000;
        private int _skillLevel;

        #endregion

        # region private properties

        private StockfishProcess _stockfish { get; set; }

        #endregion

        #region public properties

        public Settings Settings { get; set; }
        public int Depth { get; set; }

        public int SkillLevel
        {
            get => _skillLevel;
            set
            {
                _skillLevel = value;
                Settings.SkillLevel = SkillLevel;
                setOption("Skill level", SkillLevel.ToString());
            }
        }

        #endregion

        # region constructor

        public Stockfish(
            string path =
                @"D:\Projects\Stockfish\Stockfish.NET\Stockfish.NET\Stockfish\win\stockfish_12_win_x64\stockfish_20090216_x64.exe",
            int depth = 2,
            Settings settings = null)
        {
            Depth = depth;
            _stockfish = new StockfishProcess(path);
            _stockfish.Start();
            _stockfish.ReadLine();

            if (settings == null)
            {
                Settings = new Settings();
            }
            else
            {
                Settings = settings;
            }

            SkillLevel = Settings.SkillLevel;
            foreach (var property in Settings.GetPropertiesAsDictionary())
            {
                setOption(property.Key, property.Value);
            }

            startNewGame();
        }

        #endregion

        #region private

        private void send(string command, int estimatedTime = 100)
        {
            _stockfish.WriteLine(command);
            _stockfish.Wait(estimatedTime);
        }

        private bool isReady()
        {
            send("isready");
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new StackOverflowException();
                }

                var data = _stockfish.ReadLine();
                if (data == "readyok")
                {
                    return true;
                }

                return false;
            }
        }

        private void setOption(string name, string value)
        {
            send($"setoption name {name} value {value}");
            if (!isReady())
            {
                throw new ApplicationException();
            }
        }

        private string movesToString(List<string> moves)
        {
            return string.Join(" ", moves);
        }

        private void startNewGame()
        {
            send("ucinewgame");
            if (!isReady())
            {
                throw new ApplicationException();
            }
        }

        private void go()
        {
            send($"go depth {Depth}");
        }

        private void goTime(int time)
        {
            send($"go movetime {time}", estimatedTime: time + 100);
        }

        private List<string> readLineAsList()
        {
            var data = _stockfish.ReadLine();
            return data.Split(" ").ToList();
        }

        #endregion

        #region public

        public void SetPosition(List<string> moves = null)
        {
            startNewGame();
            if (moves == null)
            {
                moves = new List<string>();
            }

            send($"position startpos moves {movesToString(moves)}");
        }

        public string GetBoardVisual()
        {
            send("d");
            var board = "";
            var lines = 0;
            var tries = 0;
            while (lines < 17)
            {
                if (tries > MAX_TRIES)
                {
                    throw new StackOverflowException();
                }
                var data = _stockfish.ReadLine();
                if (data.Contains("+") || data.Contains("|"))
                {
                    lines++;
                    board += $"{data}\n";
                }

                tries++;
            }

            return board;
        }

        public string GetFenPosition()
        {
            send("d");
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new StackOverflowException();
                }

                var data = readLineAsList();
                if (data[0] == "Fen:")
                {
                    return string.Join(" ", data.GetRange(1, data.Count - 1));
                }

                tries++;
            }
        }

        public void SetFenPosition(string fenPosition)
        {
            startNewGame();
            send($"position fen {fenPosition}");
        }

        public string GetBestMove()
        {
            go();
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new StackOverflowException();
                }

                var data = readLineAsList();

                if (data[0] == "bestmove")
                {
                    if (data[1] == "(none)")
                    {
                        return null;
                    }

                    return data[1];
                }

                tries++;
            }
        }

        public string GetBestMoveTime(int time = 1000)
        {
            goTime(time);
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new StackOverflowException();
                }
                var data = readLineAsList();
                if (data[0] == "bestmove")
                {
                    if (data[1] == "(none)")
                    {
                        return null;
                    }

                    return data[1];
                }
            }
        }

        public bool IsMoveCorrect(string moveValue)
        {
            send($"go depth 1 searchmoves {moveValue}");
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new StackOverflowException();
                }

                var data = readLineAsList();
                if (data[0] == "bestmove")
                {
                    if (data[1] == "(none)")
                    {
                        return false;
                    }

                    return true;
                }

                tries++;
            }
        }

        public Evaluation GetEvaluation()
        {
            Evaluation evaluation = new Evaluation();
            var fen = GetFenPosition();
            Color compare;
            //fen sequence for white always contains w
            if (fen.Contains("w"))
            {
                compare = Color.White;
            }
            else
            {
                compare = Color.Black;
            }

            send($"position {fen}\n go");
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new StackOverflowException();
                }

                var data = readLineAsList();
                if (data[0] == "info")
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i] == "score")
                        {
                            //don't use ternary operator here for readability
                            int k;
                            if (compare == Color.White)
                            {
                                k = 1;
                            }
                            else
                            {
                                k = -1;
                            }

                            evaluation = new Evaluation(data[i + 1], Convert.ToInt32(data[i + 2]) * k);
                        }
                    }
                }
                else if (data[0] == "bestmove")
                {
                    return evaluation;
                }

                tries++;
            }
        }

        #endregion
    }
}