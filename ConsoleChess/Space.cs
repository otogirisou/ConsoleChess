using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Space
    {
        public Piece OccupyingPiece { get; set; } //also make private version?
        public bool? WhiteSpace 
        {
            get 
            { 
                return whiteSpace;
            }
            set 
            {
                if (whiteSpace == null)
                {
                    whiteSpace = value;
                }
            } 
        } //also make private version?
        private bool? whiteSpace;

        public Space(bool? whitespace)
        {
            whiteSpace = whitespace;
        }
    }
}
