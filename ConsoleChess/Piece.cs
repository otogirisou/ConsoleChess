using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Piece
    {
        protected char letter;
        protected bool white;
        protected PieceType pieceType;
        public char Letter
        {
            get
            {
                return letter;
            }
        }
        public bool White
        {
            get
            {
                return white;
            }
        }
        public bool hasMoved = false;
        public PieceType PieceTypePublic
        {
            get
            {
                return pieceType;

            }
        }
    }
    public enum PieceType
    {
        Pawn,
        Tower,
        Bishop,
        Knight,
        King,
        Queen,
    }
}
