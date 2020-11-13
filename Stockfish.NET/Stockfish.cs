using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Stockfish.NET
{
    public class Stockfish: IStockfishClient
    {
        private StockfishProcess _stockfishProcess { get; set; }
        public Stockfish(string path = @"D:\Projects\Stockfish\Stockfish.NET\Stockfish.NET\Stockfish\win\stockfish_12_win_x64\stockfish_20090216_x64.exe")
        {
            _stockfishProcess = new StockfishProcess(path);
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
            throw new System.NotImplementedException();
        }

        public string GetSkillLevel(int skillLevel = 20)
        {
            throw new System.NotImplementedException();
        }

        public void SetFenPosition(string fenPosition)
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