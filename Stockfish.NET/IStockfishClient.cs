using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stockfish.NET
{
    public interface IStockfishClient
    {
        # region Step by step methods
        void SetPosition(List<string> moves = null);
        string GetBoardVisual();
        string GetFenPosition();
        void SetFenPosition(string fenPosition);
        
        string SetSkillLevel(int skillLevel = 20);
        void GetBestMove();
        void GetBestMoveTime(int time = 1000);
        bool IsMoveCorrect(string moveValue);
        Evaluation GetEvaluation();
        # endregion
    }
}