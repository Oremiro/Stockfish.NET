using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Stockfish.NET
{
    public class Stockfish : IStockfishClient
    {
        # region private properties

        private StockfishProcess _stockfish { get; set; }

        #endregion

        # region constructor

        public Stockfish(
            string path =
                @"D:\Projects\Stockfish\Stockfish.NET\Stockfish.NET\Stockfish\win\stockfish_12_win_x64\stockfish_20090216_x64.exe")
        {
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
                if (tries > 100)
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

        private void startNewGame()
        {
            send("ucinewgame");
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
                if (tries > 100)
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

        public string GetSkillLevel(int skillLevel = 20)
        {
            throw new System.NotImplementedException();
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
                if (tries > 100)
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

        public void GetEvaluation()
        {
            throw new System.NotImplementedException();
        }

        public void SetDepth()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}