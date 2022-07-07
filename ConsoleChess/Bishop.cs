using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Bishop : Piece
    {
        public Bishop(bool w)
        {
            letter = 'b';
            white = w;
            pieceType = PieceType.Bishop;
        }
    }
}
