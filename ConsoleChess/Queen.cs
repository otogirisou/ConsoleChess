using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Queen : Piece
    {
        public Queen(bool w)
        {
            letter = 'Q';
            white = w;
            pieceType = PieceType.Queen;
        }
    }
}
