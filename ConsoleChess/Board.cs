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

        public Board()
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
        }

        public void InitialSetup()
        {
            for (int i = 0; i < 8; i++)
            {
                grid[48 + i].occupyingPiece = new Pawn(true);
                grid[8 + i].occupyingPiece = new Pawn(false);
            }
            //white pieces
            grid[56].occupyingPiece = new Tower(true);
            grid[63].occupyingPiece = new Tower(true);
            grid[57].occupyingPiece = new Knight(true);
            grid[62].occupyingPiece = new Knight(true);
            grid[58].occupyingPiece = new Bishop(true);
            grid[61].occupyingPiece = new Bishop(true);
            grid[59].occupyingPiece = new King(true);
            grid[60].occupyingPiece = new Queen(true);
            //black pieces
            grid[0].occupyingPiece = new Tower(false);
            grid[7].occupyingPiece = new Tower(false);
            grid[1].occupyingPiece = new Knight(false);
            grid[6].occupyingPiece = new Knight(false);
            grid[2].occupyingPiece = new Bishop(false);
            grid[5].occupyingPiece = new Bishop(false);
            grid[3].occupyingPiece = new King(false);
            grid[4].occupyingPiece = new Queen(false);
        }

        public Board Clone()
        {
            return this.DeepClone();
        }
    }
}
