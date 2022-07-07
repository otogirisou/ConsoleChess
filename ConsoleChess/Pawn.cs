using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Pawn : Piece
    {
        public Pawn(bool w)
        {
            letter = 'p';
            white = w;
            pieceType = PieceType.Pawn;
        }

        
    }
}
