using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    class Game
    {
        public Game()
        {
            currentBoard.InitialSetup();
            whitesTurn = true;
            ConsoleHelper.PrintBoard(currentBoard);
            while (true)
            {
                Console.WriteLine("Where is the piece you want to move? (A-H)(1-8)");
                firstMove = ConsoleHelper.GetIndexFromInput();
                if(firstMove == -1)
                {
                    Console.WriteLine("Not a valid input! Try again");
                }
                else
                {
                    Console.WriteLine("Where do you want to move? (A - H)(1 - 8)");
                    secondMove = ConsoleHelper.GetIndexFromInput();
                    if(secondMove == -1)
                    {
                        Console.WriteLine("Not a valid input! Try again");
                    } 
                    else if (Rules.CheckMove(firstMove, secondMove, currentBoard, whitesTurn))
                    {
                        if (!Rules.CheckCheck(firstMove, secondMove, currentBoard, whitesTurn))
                        {
                            Console.WriteLine("That move would leave your king checked! Try again");
                        }
                        else
                        {
                            PerformMove(firstMove, secondMove, currentBoard);
                            Console.Clear();
                            ConsoleHelper.PrintBoard(currentBoard);
                            whitesTurn = !whitesTurn;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not a valid move! Try again");
                    }
                }
                
            }
        }

        int firstMove;
        int secondMove;
        bool whitesTurn;
        Board currentBoard = new Board();

        public static void PerformMove(int firstInput, int secondInput, Board board)
        {
            board.grid[firstInput].occupyingPiece.hasMoved = true;
            //code here to keep track of dead pieces
            board.grid[secondInput].occupyingPiece = board.grid[firstInput].occupyingPiece;
            board.grid[firstInput].occupyingPiece = null;
        }
    }
}
