using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Tower : Piece
    {
        public Tower(bool w)
        {
            letter = 't';
            white = w;
            pieceType = PieceType.Tower;
        }
    }
}
