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
            Assert.AreEqual(p.Name, Piece.PieceNames.Queen);

            p = Board_Accessor.GetPiece(4, 0);
            Assert.AreEqual(p.Name, Piece.PieceNames.King);

            p = Board_Accessor.GetPiece(0, 0);
            Assert.AreEqual(p.Name, Piece.PieceNames.Rook);

            p = Board_Accessor.GetPiece(1, 0);
            Assert.AreEqual(p.Name, Piece.PieceNames.Knight);

            p = Board_Accessor.GetPiece(2, 0);
            Assert.AreEqual(p.Name, Piece.PieceNames.Bishop);
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
            Assert.AreEqual(p.Name, Piece.PieceNames.Chancellor);

            p = Board_Accessor.GetPiece(4, 0);
            Assert.AreEqual(p.Name, Piece.PieceNames.King);

            p = Board_Accessor.GetPiece(0, 0);
            Assert.AreEqual(p.Name, Piece.PieceNames.Rook);

            p = Board_Accessor.GetPiece(1, 0);
            Assert.AreEqual(p.Name, Piece.PieceNames.Knight);

            p = Board_Accessor.GetPiece(2, 0);
            Assert.AreEqual(p.Name, Piece.PieceNames.Bishop);
        }
    }
}
