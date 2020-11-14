using System;
using System.Collections.Generic;
using Xunit;

namespace Stockfish.NET.Tests
{
    public class TestStockfishMethods
    {
        private IStockfish Stockfish { get; set; }
        public TestStockfishMethods()
        {
            Stockfish = new Stockfish(depth: 20);
        }

        [Fact]
        public void TestGetBestMoveFirstMove()
        {
            var bestMove = Stockfish.GetBestMove();
            Assert.Contains(bestMove, new List<string>
            {
                "e2e3",
                "e2e4",
                "g1f3",
                "b1c3",
                "d2d4"
            });
        }

        [Fact]
        public void TestGetBestMoveTimeFirstMove()
        {
            var bestMove = Stockfish.GetBestMoveTime();
            Assert.Contains(bestMove, new List<string>
            {
                "e2e3",
                "e2e4",
                "g1f3",
                "b1c3",
                "d2d4"
            });
        }

        [Fact]
        public void TestGetBestMoveNotFirstMove()
        {
            Stockfish.SetPosition(new List<string>
            {
                "e2e4", "e7e6" 
            });
            var bestMove = Stockfish.GetBestMove();
            Assert.Contains(bestMove, new List<string>
            {
                "d2d4", "g1f3"
            });
        }
        
        
        [Fact]
        public void TestGetBestMoveTimeNotFirstMove()
        {
            Stockfish.SetPosition(new List<string>
            {
                "e2e4", "e7e6" 
            });
            var bestMove = Stockfish.GetBestMoveTime();
            Assert.Contains(bestMove, new List<string>
            {
                "d2d4", "g1f3"
            });
        }
    }
}