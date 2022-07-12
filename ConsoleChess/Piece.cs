using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Piece
    {
        public Piece(bool White, bool hasmoved, PieceType piecetypepublic)
        {
            white = White;
            pieceType = piecetypepublic;
            hasMoved = hasmoved;
            switch (piecetypepublic)
            {
                case PieceType.Pawn:
                    letter = '♙';
                    break;
                case PieceType.Tower:
                    letter = '♖';
                    break;
                case PieceType.Knight:
                    letter = '♘';
                    break;
                case PieceType.Bishop:
                    letter = '♗';
                    break;
                case PieceType.King:
                    letter = '♔';
                    break;
                case PieceType.Queen:
                    letter = '♕';
                    break;
                default:
                    return;
            }
        }
        protected char letter;
        protected bool white;
        protected bool hasMoved;
        protected PieceType pieceType;
        public char Letter
        {
            get { return letter; }
        }
        public bool White
        {
            get { return white; }
        }
        public bool HasMoved
        {
            get { return hasMoved; }
            set
            {
                if (!hasMoved)
                {
                    hasMoved = value;
                }
            }
        }
        public PieceType PieceTypePublic
        {
            get { return pieceType; }
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
