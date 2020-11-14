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
    }
}