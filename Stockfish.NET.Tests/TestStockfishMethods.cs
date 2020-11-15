using System;
using System.Collections.Generic;
using System.IO;
using Stockfish.NET.Models;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Stockfish.NET.Tests
{
    public class TestStockfishMethods
    {
        private IStockfish Stockfish { get; set; }

        public TestStockfishMethods(ITestOutputHelper testOutputHelper)
        {
            var path = Utils.GetStockfishDir();
            Stockfish = new Core.Stockfish(path, depth: 2);
        }

        [Fact(Timeout = 2000)]
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

        [Fact(Timeout = 2000)]
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

        [Fact(Timeout = 2000)]
        public void TestGetBestMoveNotFirstMove()
        {
            Stockfish.SetPosition(
                "e2e4", "e7e6"
            );
            var bestMove = Stockfish.GetBestMove();
            Assert.Contains(bestMove, new List<string>
            {
                "d2d4", "g1f3"
            });
        }


        [Fact(Timeout = 2000)]
        public void TestGetBestMoveTimeNotFirstMove()
        {
            Stockfish.SetPosition(
                "e2e4", "e7e6"
            );
            var bestMove = Stockfish.GetBestMoveTime();
            Assert.Contains(bestMove, new List<string>
            {
                "d2d4", "g1f3"
            });
        }

        [Fact(Timeout = 2000)]
        public void TestGetBestMoveMate()
        {
            Stockfish.SetPosition(
                "f2f3", "e7e5", "g2g4", "d8h4"
            );
            var bestMove = Stockfish.GetBestMove();
            Assert.Null(bestMove);
        }

        [Fact(Timeout = 2000)]
        public void TestSetFenPosition()
        {
            Stockfish.SetFenPosition("7r/1pr1kppb/2n1p2p/2NpP2P/5PP1/1P6/P6K/R1R2B2 w - - 1 27");
            var move1 = Stockfish.IsMoveCorrect("f4f5");
            var move2 = Stockfish.IsMoveCorrect("a1c1");
            Assert.True(move1);
            Assert.False(move2);
        }

        [Fact(Timeout = 2000)]
        public void TestGetBoardVisual()
        {
            Stockfish.SetPosition("e2e4", "e7e6", "d2d4", "d7d5");
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

        [Fact(Timeout = 2000)]
        public void TestGetFenPosition()
        {
            var defaultFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            var fen = Stockfish.GetFenPosition();
            Assert.Equal(defaultFen, fen);
        }

        [Fact(Timeout = 2000)]
        public void TestGetFenPositionAfterMoves()
        {
            Stockfish.SetPosition(
                "e2e4", "e7e6"
            );
            var fen = "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2";
            var fenPosition = Stockfish.GetFenPosition();
            Assert.Equal(fen, fenPosition);
        }

        [Fact(Timeout = 2000)]
        public void TestGetEvaluationMate()
        {
            var wrongFen = "6k1/p4p1p/6p1/5r2/3b4/6PP/4qP2/5RK1 b - - 14 36";
            Stockfish.SetFenPosition(wrongFen);
            var estimatedEvaluation = new Evaluation();
            estimatedEvaluation.Type = "mate";
            estimatedEvaluation.Value = -3;
            var evaluation = Stockfish.GetEvaluation();
            Assert.True(estimatedEvaluation.Type == evaluation.Type && estimatedEvaluation.Value == evaluation.Value);
        }

        [Fact(Timeout = 2000)]
        public void TestGetEvaluationCP()
        {
            var wrongFen = "r4rk1/pppb1p1p/2nbpqp1/8/3P4/3QBN2/PPP1BPPP/R4RK1 w - - 0 11";
            Stockfish.SetFenPosition(wrongFen);
            var evaluation = Stockfish.GetEvaluation();
            Assert.True(evaluation.Type == "cp" && evaluation.Value > 0);
        }

        [Fact(Timeout = 2000)]
        public void TestGetEvaluationStalemate()
        {
            var wrongFen = "1nb1kqn1/pppppppp/8/6r1/5b1K/6r1/8/8 w - - 2 2";
            Stockfish.SetFenPosition(wrongFen);
            var estimatedEvaluation = new Evaluation();
            estimatedEvaluation.Type = "cp";
            estimatedEvaluation.Value = 0;
            var evaluation = Stockfish.GetEvaluation();
            Assert.True(estimatedEvaluation.Type == evaluation.Type && estimatedEvaluation.Value == evaluation.Value);
        }

        [Fact(Timeout = 2000)]
        public void TestGetBestMoveWrongPositon()
        {
            var wrongFen = "3kk3/8/8/8/8/8/8/3KK3 w - - 0 0";
            Stockfish.SetFenPosition(wrongFen);
            var bestMove = Stockfish.GetBestMove();
            Assert.Contains(bestMove, new List<string>
            {
                "d1e2",
                "d1c1"
            });
        }
    }
}