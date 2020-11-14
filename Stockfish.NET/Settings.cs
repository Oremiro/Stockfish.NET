namespace Stockfish.NET
{
    public class Settings
    {
        public bool WriteDebugLog { get; set; }
        public int Contempt { get; set; }
        public int MinSplitDepth { get; set; }
        public int Threads { get; set; }
        public bool Ponder { get; set; }
        public int Hash { get; set; }
        public int MultiPV { get; set; }
        public int SkillLevel { get; set; }
        public int MoveOverhead { get; set; }
        public int MinimumThinkingTime { get; set; }
        public int SlowMover { get; set; }
        public bool UCIChess960 { get; set; }

        public Settings(
            bool writeDebugLog = false,
            int contempt = 0,
            int minSplitDepth = 0,
            int threads = 0,
            bool ponder = false,
            int hash = 16,
            int multiPV = 1,
            int skillLevel = 20,
            int moveOverhead = 30,
            int minimumThinkingTime = 20,
            int slowMover = 80,
            bool uciChess960 = false
        )
        {
            WriteDebugLog = writeDebugLog;
            Contempt = contempt;
            MinSplitDepth = minSplitDepth;
            Ponder = ponder;
            Threads = threads;
            Hash = hash;
            MultiPV = multiPV;
            SkillLevel = skillLevel;
            MoveOverhead = moveOverhead;
            MinimumThinkingTime = minimumThinkingTime;
            SlowMover = slowMover;
            UCIChess960 = uciChess960;
        }
    }
}