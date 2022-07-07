using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Space
    {
        public Piece occupyingPiece = null;
        public bool white;
        public Space(bool w)
        {
            white = w;
        }
    }
}
