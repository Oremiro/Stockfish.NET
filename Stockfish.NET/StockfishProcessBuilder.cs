using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Stockfish.NET
{
    public class StockfishProcessBuilder
    {
        private ProcessStartInfo _processStartInfo { get; set; }
        private Process _process { get; set; }

        public StockfishProcessBuilder()
        {
            _processStartInfo = new ProcessStartInfo
            {
                FileName = @"D:\Projects\Stockfish\Stockfish.NET\Stockfish.NET\Stockfish\win\stockfish_12_win_x64\stockfish_20090216_x64.exe",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };
            _process = new Process {StartInfo = _processStartInfo};
            _process.OutputDataReceived += (sender, args) => this.DataReceived.Invoke(sender, args);
        }

        public event DataReceivedEventHandler DataReceived = (sender, args) => { Console.WriteLine(args.Data); };

        public void Start()
        {
            _process.Start();
            _process.BeginOutputReadLine();
        }

        public void Wait(int millisecond)
        {
            this._process.WaitForExit(millisecond);
        }

        public void SendUciCommand(string command)
        {
            _process.StandardInput.WriteLine(command);
            _process.StandardInput.Flush();
        }
    }
}