using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Stockfish.NET.Tests
{
    public class TestStockfishMethods
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private IStockfish Stockfish { get; set; }

        public TestStockfishMethods(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            Stockfish = new Stockfish(depth: 2);
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

        [Fact]
        public void TestGetBestMoveMate()
        {
            Stockfish.SetPosition(new List<string>
            {
                "f2f3", "e7e5", "g2g4", "d8h4"
            });
            var bestMove = Stockfish.GetBestMove();
            Assert.Null(bestMove);
        }

        [Fact]
        public void TestSetFenPosition()
        {
            Stockfish.SetFenPosition("7r/1pr1kppb/2n1p2p/2NpP2P/5PP1/1P6/P6K/R1R2B2 w - - 1 27");
            var move1 = Stockfish.IsMoveCorrect("f4f5");
            var move2 = Stockfish.IsMoveCorrect("a1c1");
            Assert.True(move1);
            Assert.False(move2);
        }

        [Fact]
        public void TestGetBoardVisual()
        {
            Stockfish.SetPosition(new List<string>
            {
                "e2e4", "e7e6", "d2d4", "d7d5"
            });
            var expected = " +---+---+---+---+---+---+---+---+\n" +
                           " | r | n | b | q | k | b | n | r | 8\n" +
                           " +---+---+---+---+---+---+---+---+\n" +
                           " | p | p | p |   |   | p | p | p | 7\n" +
                           " +---+---+---+---+---+---+---+---+\n" +
                           " |   |   |   |   | p |   |   |   | 6\n" +
                           " +---+---+---+---+---+---+---+---+\n" +
                           " |   |   |   | p |   |   |   |   | 5\n" +
                           " +---+---+---+---+---+---+---+---+\n" +
                           " |   |   |   | P | P |   |   |   | 4\n" +
                           " +---+---+---+---+---+---+---+---+\n" +
                           " |   |   |   |   |   |   |   |   | 3\n" +
                           " +---+---+---+---+---+---+---+---+\n" +
                           " | P | P | P |   |   | P | P | P | 2\n" +
                           " +---+---+---+---+---+---+---+---+\n" +
                           " | R | N | B | Q | K | B | N | R | 1\n" +
                           " +---+---+---+---+---+---+---+---+\n";
            var board = Stockfish.GetBoardVisual();
            Assert.Equal(expected, board);
        }
    }
}