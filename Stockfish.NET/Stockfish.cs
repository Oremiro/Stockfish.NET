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
                var data = _stockfish.ReadLine();
                var splittedData = data.Split(" ").ToList();
                if (splittedData[0] == "Fen:")
                {
                    return string.Join(" ", splittedData.GetRange(1, splittedData.Count - 1));
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
            throw new System.NotImplementedException();
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