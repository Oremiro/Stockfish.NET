namespace Stockfish.NET
{
    public interface IStockfishProcessBuilder
    {
        void AddStandartInput();
        void AddStandartOutput();
        void AddPathToProcessFile();
    }
}