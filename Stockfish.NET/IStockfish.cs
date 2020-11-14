using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stockfish.NET
{
    public interface IStockfishClient
    {
        int Depth { get; set; };
        int SkillLevel { get; set; }
        void SetPosition(List<string> moves = null);
        string GetBoardVisual();
        string GetFenPosition();
        void SetFenPosition(string fenPosition);
        void GetBestMove();
        void GetBestMoveTime(int time = 1000);
        bool IsMoveCorrect(string moveValue);
        Evaluation GetEvaluation();
    }
}