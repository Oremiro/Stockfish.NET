using System;
using System.Diagnostics;

namespace Stockfish.NET
{
    public class StockfishProcessBuilder: IStockfishProcessBuilder
    {
        private ProcessStartInfo _processStartInfo { get; set; }
        public StockfishProcessBuilder()
        {
            _processStartInfo = new ProcessStartInfo();
        }

        public void AddPathToProcessFile()
        {
            var path =
                @"D:\Projects\Stockfish\Stockfish.NET\Stockfish.NET\Stockfish\stockfish_12_win_x64_bmi2\stockfish_20090216_x64_bmi2.exe";
            _processStartInfo.FileName = path;
        }
        public void AddStandartInput()
        {
            _processStartInfo.RedirectStandardInput = true;
        }

        public void AddStandartOutput()
        {
            _processStartInfo.RedirectStandardOutput = true;
        }
        
        public Stockfish GetStockfish()
        {
            throw new NotImplementedException();
        }

        private void getStockfishProcess()
        {
            
        }
    }
}