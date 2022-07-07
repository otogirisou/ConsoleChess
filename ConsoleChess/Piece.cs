using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Piece
    {
        public char letter;
        public bool white;
        public bool hasMoved = false;
        public PieceType pieceType;
        
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
