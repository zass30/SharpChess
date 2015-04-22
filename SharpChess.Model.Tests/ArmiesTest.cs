using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpChess.Model.Tests
{
    #region Using

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SharpChess;
    using SharpChess.Model;

    #endregion

    [TestClass]
    public class ArmiesTests
    {

        [TestMethod]
        public void Armies_Setup_Classic()
        {
            // set up a starting position
            Game_Accessor.NewInternal();

            // check default army is classic
            Assert.AreEqual(Game_Accessor.PlayerWhite.Army, Armies.ArmyNames.Classic);

            // test queen is queen
            Piece p = Board_Accessor.GetPiece(3, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.Queen);

            p = Board_Accessor.GetPiece(4, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.King);

            p = Board_Accessor.GetPiece(0, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.Rook);

            p = Board_Accessor.GetPiece(1, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.Knight);

            p = Board_Accessor.GetPiece(2, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.Bishop);
        }

        [TestMethod]
        public void Armies_Setup_Chancellor()
        {
            // set armies to chancellor
            Game_Accessor.PlayerWhite.Army = Armies.ArmyNames.Chancellor;

            // set up a starting position
            Game_Accessor.NewInternal();

            // test queen is chancellor
            Piece p = Board_Accessor.GetPiece(3, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.Chancellor);

            p = Board_Accessor.GetPiece(4, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.King);

            p = Board_Accessor.GetPiece(0, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.Rook);

            p = Board_Accessor.GetPiece(1, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.Knight);

            p = Board_Accessor.GetPiece(2, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.Bishop);

            Game_Accessor.PlayerWhite.Army = Armies.ArmyNames.Classic;

            // test queen is queen
            p = Board_Accessor.GetPiece(3, 0);
            Assert.AreEqual(p.Role, Piece.PieceNames.Queen);
        }

        [TestMethod]
        public void Armies_Moves_Chancellor()
        {
            string fen = "8/8/3k4/2q5/3Q4/8/1K6/8 b - - 0 1";
            Game_Accessor.PlayerWhite.Army = Armies.ArmyNames.Chancellor;
            Game_Accessor.NewInternal(fen);
            Square s = Board_Accessor.GetSquare("d6");
            Piece p = s.Piece;

            // king should only have 3 legal moves here
            Assert.AreEqual(p.Role, Piece.PieceNames.King);
            Moves moves = new Moves();
            p.GenerateLegalMoves(moves);
            Assert.AreEqual(moves.Count, 3);

            // todo: still a bug if you load this position into the game ui (classic), then switch to chancellor, the king has 4 moves not 3.
            Game_Accessor.PlayerWhite.Army = Armies.ArmyNames.Classic;
        }

        [TestMethod]
        public void Armies_Moves_Empowered()
        {
            string fen = "8/8/8/1k6/8/2BN4/8/K7 b - - 0 1";

            // black king should be in check by knight empowered to bishop
            Game_Accessor.PlayerWhite.Army = Armies.ArmyNames.Empowered;
            Game_Accessor.NewInternal(fen);
            Assert.IsTrue(Game_Accessor.PlayerBlack.IsInCheck);


            // black king should be in check by bishop empowered to knight
            fen = "8/8/8/3k4/8/2BN4/8/K7 b - - 0 1";
            Game_Accessor.NewInternal(fen);
            Assert.IsTrue(Game_Accessor.PlayerBlack.IsInCheck);

            Game_Accessor.PlayerWhite.Army = Armies.ArmyNames.Classic;
        }
    }
}
