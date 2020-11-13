using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Stockfish.NET
{
    public class Stockfish: IStockfishClient
    {
        public Stockfish()
        {
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

        public async Task SetPositionAsync(List<string> moves = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetBoardVisualAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetFenPositionAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetSkillLevelAsync(int skillLevel = 20)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetFenPositionAsync(string fenPosition)
        {
            throw new System.NotImplementedException();
        }

        public async Task GetBestMoveAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task GetBestMoveTimeAsync(int time = 1000)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsMoveCorrectAsync(string moveValue)
        {
            throw new System.NotImplementedException();
        }

        public async Task GetEvaluationAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task SetDepthAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}