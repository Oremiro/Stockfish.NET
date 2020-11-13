using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Stockfish.NET
{
    public class StockfishProcessOracle
    {
        private ProcessStartInfo _processStartInfo { get; set; }
        private Process _process { get; set; }
        private static List<string> _history { get; set; }

        public StockfishProcessOracle()
        {
            _history = new List<string>();
            _processStartInfo = new ProcessStartInfo
            {
                FileName =
                    @"D:\Projects\Stockfish\Stockfish.NET\Stockfish.NET\Stockfish\win\stockfish_12_win_x64\stockfish_20090216_x64.exe",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };
            _process = new Process {StartInfo = _processStartInfo};
         //   _process.OutputDataReceived += new DataReceivedEventHandler(DataReceived);
         //   _process.ErrorDataReceived += new DataReceivedEventHandler(DataReceived);
        }

        public event DataReceivedEventHandler DataReceived = (sender, args) => { _history.Add(args.Data); };

        public void Start()
        {
            _process.Start();
          //  _process.BeginOutputReadLine();
          //  _process.BeginErrorReadLine();
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

        public string ReadLine()
        {
            return _process.StandardOutput.ReadLine();
        }

        public void Close()
        {
            _process.Close();
        }
    }
}