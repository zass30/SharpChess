using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpChess.Model
{
    public static class Armies
    {

        #region Enums
        public enum ArmyNames
        {
            Classic,
            Chancellor,
            Empowered
        }
        #endregion

        // given a piece (like Queen) and an army (like Animals), return a role (like Chancellor)
        public static IPieceTop PieceTopForArmy(Armies.ArmyNames army, Piece basePiece)
        {
            Piece.PieceNames name = basePiece.Name;
            if (army == ArmyNames.Classic)
            {
                switch (name)
                {
                    case Piece.PieceNames.Pawn:
                        return new PiecePawn(basePiece);
                    case Piece.PieceNames.Bishop:
                        return new PieceBishop(basePiece);
                    case Piece.PieceNames.Knight:
                        return new PieceKnight(basePiece);
                    case Piece.PieceNames.Rook:
                        return new PieceRook(basePiece);
                    case Piece.PieceNames.Queen:
                        return new PieceQueen(basePiece);
                    case Piece.PieceNames.King:
                        return new PieceKing(basePiece);
                    default:
                        throw new Exception("Unknown piece type: " + name + " " + army);
                }
            }
            else if (army == ArmyNames.Chancellor)
            {
                switch (name)
                {
                    case Piece.PieceNames.Pawn:
                        return new PiecePawn(basePiece);
                    case Piece.PieceNames.Bishop:
                        return new PieceBishop(basePiece);
                    case Piece.PieceNames.Knight:
                        return new PieceKnight(basePiece);
                    case Piece.PieceNames.Rook:
                        return new PieceRook(basePiece);
                    case Piece.PieceNames.Queen:
                        return new PieceChancellor(basePiece);
                    case Piece.PieceNames.King:
                        return new PieceKing(basePiece);
                    default:
                        throw new Exception("Unknown piece type: " + name + " " + army);
                }
            }
            else
            {
                throw new Exception("Unknown army type: " + name + " " + army);
            }
        }
    }
}
