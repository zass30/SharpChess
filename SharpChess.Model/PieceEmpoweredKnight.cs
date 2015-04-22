// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PieceKnight.cs" company="SharpChess.com">
//   SharpChess.com
// </copyright>
// <summary>
//   The piece knight.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region License

// SharpChess
// Copyright (C) 2012 SharpChess.com
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

using System.Collections.Generic;

namespace SharpChess.Model
{
    /// <summary>
    /// The piece knight.
    /// </summary>
    public class PieceEmpoweredKnight : IPieceTop
    {
        #region Constants and Fields

        /// <summary>
        /// Simple positional piece-square score values.
        /// </summary>
        private static readonly int[] SquareValues =
        { 
            1, 1,  1,  1,  1,  1, 1, 1,    0, 0, 0, 0, 0, 0, 0, 0, 
            1, 7,  7,  7,  7,  7, 7, 1,    0, 0, 0, 0, 0, 0, 0, 0, 
            1, 7, 18, 18, 18, 18, 7, 1,    0, 0, 0, 0, 0, 0, 0, 0, 
            1, 7, 18, 27, 27, 18, 7, 1,    0, 0, 0, 0, 0, 0, 0, 0, 
            1, 7, 18, 27, 27, 18, 7, 1,    0, 0, 0, 0, 0, 0, 0, 0, 
            1, 7, 18, 18, 18, 18, 7, 1,    0, 0, 0, 0, 0, 0, 0, 0, 
            1, 7,  7,  7,  7,  7, 7, 1,    0, 0, 0, 0, 0, 0, 0, 0,
            1, 1,  1,  1,  1,  1, 1, 1,    0, 0, 0, 0, 0, 0, 0, 0
        };

        /// <summary>
        /// Directional vectors of where the piece can go
        /// </summary>
        public static int[] moveVectors = { 33, 18, -14, -31, -33, -18, 14, 31 };
        public static int[] empoweredAdjacencyVectors = { 1, -1, 16, -16 };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PieceKnight"/> class.
        /// </summary>
        /// <param name="pieceBase">
        /// The piece base.
        /// </param>
        public PieceEmpoweredKnight(Piece pieceBase)
        {
            this.Base = pieceBase;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Abbreviation.
        /// </summary>
        public string Abbreviation
        {
            get
            {
                return "N";
            }
        }

        /// <summary>
        /// Gets the base part of the piece. i.e. the bit that sits on the chess square.
        /// </summary>
        public Piece Base { get; private set; }

        /// <summary>
        /// Gets basic value of the piece. e.g. pawn = 1, bishop = 3, queen = 9
        /// </summary>
        public int BasicValue
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Gets the image index for this piece. Used to determine which graphic image is displayed for thie piece.
        /// </summary>
        public int ImageIndex
        {
            get
            {
                return this.Base.Player.Colour == Player.PlayerColourNames.White ? 7 : 6;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the piece is capturable. Kings aren't, everything else is.
        /// </summary>
        public bool IsCapturable
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the piece's name.
        /// </summary>
        public Piece.PieceNames Name
        {
            get
            {
                return Piece.PieceNames.Knight;
            }
        }

        /// <summary>
        /// Gets the piece's name.
        /// </summary>
        public Piece.PieceNames Role
        {
            get
            {
                return Piece.PieceNames.EmpoweredKnight;
            }
        }

        /// <summary>
        /// Gets the positional points assigned to this piece.
        /// </summary>
        public int PositionalPoints
        {
            get
            {
                int intPoints = 0;

                if (Game.Stage == Game.GameStageNames.End)
                {
                    intPoints -= this.Base.TaxiCabDistanceToEnemyKingPenalty() << 4;
                }
                else
                {
                    intPoints += SquareValues[this.Base.Square.Ordinal] << 3;

                    if (this.Base.CanBeDrivenAwayByPawn())
                    {
                        intPoints -= 30;
                    }
                }

                intPoints += this.Base.DefensePoints;

                return intPoints;
            }
        }

        /// <summary>
        /// Gets the material value of this piece.
        /// </summary>
        public int Value
        {
            get
            {
                return 3250; // + ((m_Base.Player.PawnsInPlay-5) * 63);  // raise the knight's value by 1/16 for each pawn above five of the side being valued, with the opposite adjustment for each pawn short of five;
            }
        }

        #endregion

        #region Public Methods

        public bool IsEmpoweredAsBishop()
        {
            Square square;
            for (int i = 0; i < empoweredAdjacencyVectors.Length; i++)
            {
                square = Board.GetSquare(this.Base.Square.Ordinal + empoweredAdjacencyVectors[i]);
                if (square != null && (square.Piece != null && (square.Piece.Player.Colour == this.Base.Player.Colour) && (square.Piece.Role == Piece.PieceNames.EmpoweredBishop)))
                    return true;
            }
            return false;
        }

        public bool IsEmpoweredAsRook()
        {
            Square square;
            for (int i = 0; i < empoweredAdjacencyVectors.Length; i++)
            {
                square = Board.GetSquare(this.Base.Square.Ordinal + empoweredAdjacencyVectors[i]);
                if (square != null && (square.Piece != null && (square.Piece.Player.Colour == this.Base.Player.Colour) && (square.Piece.Role == Piece.PieceNames.EmpoweredRook)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Generate "lazy" moves for this piece, which is all usual legal moves, but also includes moves that put the king in check.
        /// </summary>
        /// <param name="moves">
        /// Moves list that will be populated with lazy moves.
        /// </param>
        /// <param name="movesType">
        /// Types of moves to include. e.g. All, or captures-only.
        /// </param>
        public void GenerateLazyMoves(Moves moves, Moves.MoveListNames movesType)
        {
            Square square;

            switch (movesType)
            {
                case Moves.MoveListNames.All:
                    for (int i = 0; i < moveVectors.Length; i++)
                    {
                        square = Board.GetSquare(this.Base.Square.Ordinal + moveVectors[i]);
                        if (square != null && (square.Piece == null || (square.Piece.Player.Colour != this.Base.Player.Colour && square.Piece.IsCapturable)))
                        {
                            moves.Add(0, 0, Move.MoveNames.Standard, this.Base, this.Base.Square, square, square.Piece, 0, 0);
                        }
                    }
                    break;

                case Moves.MoveListNames.CapturesPromotions:
                    for (int i = 0; i < moveVectors.Length; i++)
                    {
                        square = Board.GetSquare(this.Base.Square.Ordinal + moveVectors[i]);
                        if (square != null && (square.Piece != null && (square.Piece.Player.Colour != this.Base.Player.Colour && square.Piece.IsCapturable)))
                        {
                            moves.Add(0, 0, Move.MoveNames.Standard, this.Base, this.Base.Square, square, square.Piece, 0, 0);
                        }
                    }
                    break;
            }

            // get empowered status
            if (IsEmpoweredAsBishop())
            {
                PieceBishop b = new PieceBishop(this.Base);
                b.GenerateLazyMoves(moves, movesType);
            }
            if (IsEmpoweredAsRook())
            {
                PieceRook r = new PieceRook(this.Base);
                r.GenerateLazyMoves(moves, movesType);
            }

        }



        public bool CanAttackSquare(Square target_square)
        {
            Square square;
            for (int i = 0; i < moveVectors.Length; i++)
            {
                square = Board.GetSquare(this.Base.Square.Ordinal + moveVectors[i]);
                if (square != null && square.Ordinal == target_square.Ordinal)
                    return true;
            }
            // get empowered status
            if (IsEmpoweredAsBishop())
            {
                PieceBishop b = new PieceBishop(this.Base);
                if (b.CanAttackSquare(target_square))
                    return true;
            }
            if (IsEmpoweredAsRook())
            {
                PieceRook r = new PieceRook(this.Base);
                if (r.CanAttackSquare(target_square))
                    return true;
            }

            return false;
        }

        #endregion

        #region Static methods

        static private Piece.PieceNames _pieceType = Piece.PieceNames.Knight;

        #endregion 
    }
}