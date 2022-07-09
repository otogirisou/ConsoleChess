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
                    letter = 'p';
                    break;
                case PieceType.Tower:
                    letter = 't';
                    break;
                case PieceType.Knight:
                    letter = 'k';
                    break;
                case PieceType.Bishop:
                    letter = 'b';
                    break;
                case PieceType.King:
                    letter = 'K';
                    break;
                case PieceType.Queen:
                    letter = 'Q';
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
                if(!hasMoved)
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
