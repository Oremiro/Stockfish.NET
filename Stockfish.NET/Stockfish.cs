using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Stockfish.NET
{
    public class Stockfish : IStockfishClient
    {
        private StockfishProcess _stockfish { get; set; }

        public Stockfish(
            string path =
                @"D:\Projects\Stockfish\Stockfish.NET\Stockfish.NET\Stockfish\win\stockfish_12_win_x64\stockfish_20090216_x64.exe")
        {
            _stockfish = new StockfishProcess(path);
        }
        
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
            _stockfish.WriteLine("d");
            _stockfish.Wait(50);
            var tries = 0;
            while (true)
            {
                if(tries > 100)
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
            _stockfish.WriteLine($"position fen {fenPosition}");
        }

        private void startNewGame()
        {
            
        }

        private List<string> readLineAsList()
        {
            var data = _stockfish.ReadLine();
            return data.Split(" ").ToList();
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
            _stockfish.WriteLine($"go depth 1 searchmoves {moveValue}");
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
    }
}