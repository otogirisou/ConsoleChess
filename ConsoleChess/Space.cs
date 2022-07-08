using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Space
    {
        public Piece occupyingPiece { get; set; } //also make private version?
        public bool white { get; set; } //also make private version?

        public Space(bool w)
        {
            white = w;
            occupyingPiece = null;
        }
    }
}
