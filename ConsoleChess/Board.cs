using Force.DeepCloner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Board
    {
        public Space[] grid { get; set; } //also make private version?


        public void InitialSetup()
        {
            grid = new Space[64];
            bool whiteSpace = true;
            for (int i = 0; i < 64; i++)
            {
                if (i % 8 != 0)
                {
                    whiteSpace = !whiteSpace;
                }
                grid[i] = new Space(whiteSpace);
            }
            for (int i = 0; i < 8; i++)
            {
                grid[48 + i].OccupyingPiece = new Piece(true, false, PieceType.Pawn);
                grid[8 + i].OccupyingPiece = new Piece(false, false, PieceType.Pawn);
            }
            //white pieces
            grid[56].OccupyingPiece = new Piece(true, false, PieceType.Tower);
            grid[63].OccupyingPiece = new Piece(true, false, PieceType.Tower);
            grid[57].OccupyingPiece = new Piece(true, false, PieceType.Knight);
            grid[62].OccupyingPiece = new Piece(true, false, PieceType.Knight);
            grid[58].OccupyingPiece = new Piece(true, false, PieceType.Bishop);
            grid[61].OccupyingPiece = new Piece(true, false, PieceType.Bishop);
            grid[59].OccupyingPiece = new Piece(true, false, PieceType.King);
            grid[60].OccupyingPiece = new Piece(true, false, PieceType.Queen);
            //black pieces
            grid[0].OccupyingPiece = new Piece(false, false, PieceType.Tower);
            grid[7].OccupyingPiece = new Piece(false, false, PieceType.Tower);
            grid[1].OccupyingPiece = new Piece(false, false, PieceType.Knight);
            grid[6].OccupyingPiece = new Piece(false, false, PieceType.Knight);
            grid[2].OccupyingPiece = new Piece(false, false, PieceType.Bishop);
            grid[5].OccupyingPiece = new Piece(false, false, PieceType.Bishop);
            grid[3].OccupyingPiece = new Piece(false, false, PieceType.King);
            grid[4].OccupyingPiece = new Piece(false, false, PieceType.Queen);

            
        }

        public Board Clone()
        {
            return this.DeepClone();
        }
    }
}
