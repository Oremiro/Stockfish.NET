using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Stockfish.NET
{
    public enum Color
    {
        Black,
        White
    }

    public class Evaluation
    {
        public string Type { get; set; }
        public int Value { get; set; }

        public Evaluation()
        {
        }

        public Evaluation(string type, int value)
        {
            Type = type;
            Value = value;
        }
    }

    public class Stockfish : IStockfishClient
    {
        #region private variables

        private const int MAX_TRIES = 1000;

        #endregion

        # region private properties

        private StockfishProcess _stockfish { get; set; }

        #endregion

        #region public properties

        public int Depth { get; set; }

        public int SkillLevel
        {
            get => SkillLevel;
            set
            {
                SkillLevel = value;
                setOption("Skill level", SkillLevel);
            }
        }

        #endregion

        # region constructor

        public Stockfish(
            string path =
                @"D:\Projects\Stockfish\Stockfish.NET\Stockfish.NET\Stockfish\win\stockfish_12_win_x64\stockfish_20090216_x64.exe",
            int depth = 2)
        {
            Depth = depth;
            _stockfish = new StockfishProcess(path);
            _stockfish.Start();
        }

        #endregion

        #region private

        private void send(string command, int estimatedTime = 50)
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

                if (_stockfish.ReadLine() == "readyok")
                {
                    return true;
                }

                return false;
            }
        }
        
        private void setOption(string name, int value)
        {
            send($"setoption name {name} value {value}");
            if (!isReady())
            {
                throw new ApplicationException();
            }
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
            send($"go movetime {time}");
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
            throw new System.NotImplementedException();
        }

        public string GetBoardVisual()
        {
            throw new System.NotImplementedException();
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

        public void GetBestMove()
        {
            throw new System.NotImplementedException();
        }

        public void GetBestMoveTime(int time = 1000)
        {
            throw new System.NotImplementedException();
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