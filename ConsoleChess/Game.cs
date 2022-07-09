using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleChess
{
    class Game
    {
        public List<Piece> DeadWhitePieces
        {
            get
            {
                return deadWhitePieces;
            }
        }
        public List<Piece> DeadBlackPieces
        {
            get
            {
                return deadBlackPieces;
            }
        }

        int firstMove;
        int secondMove;
        private List<Piece> deadWhitePieces;
        private List<Piece> deadBlackPieces;

        public Game(List<Piece> deadwhitepieces, List<Piece> deadblackpieces)
        {
            deadWhitePieces = deadwhitepieces;
            deadBlackPieces = deadblackpieces;
        }

        private bool? whitesTurn;
        private Board currentBoard;
        public Board CurrentBoard
        {
            get
            {
                return currentBoard;
            }
            set
            {
                if (currentBoard == null)
                {
                    currentBoard = value;
                }
            }
        }
        public bool? WhitesTurn
        {
            get
            {
                return whitesTurn;
            }
            set
            {
                if (whitesTurn == null)
                {
                    whitesTurn = value;
                }
            }
        }

        public void StartGame()
        {
            while (true)
            {
                Console.WriteLine("Where is the piece you want to move? (A-H)(1-8)");
                firstMove = ConsoleHelper.GetIndexFromInput();
                if (firstMove == -1)
                {
                    Console.WriteLine("Not a valid input! Try again");
                }
                else if (firstMove == -2)
                {
                    SaveLoad.Save(this);
                    Console.WriteLine("Game saved!");
                    Console.WriteLine("Window closing in:");
                    for (int i = 5; i > 0; i--)
                    {
                        Console.WriteLine(i);
                        Thread.Sleep(250);
                    }
                    break;
                }

                else
                {
                    Console.WriteLine("Where do you want to move? (A - H)(1 - 8)");
                    secondMove = ConsoleHelper.GetIndexFromInput();
                    if (secondMove == -1)
                    {
                        Console.WriteLine("Not a valid input! Try again");
                    }
                    else if (Rules.CheckMove(firstMove, secondMove, currentBoard, (bool)whitesTurn))
                    {
                        if (!Rules.CheckCheck(firstMove, secondMove, currentBoard, (bool)whitesTurn))
                        {
                            Console.WriteLine("That move would leave your king checked! Try again");
                        }
                        else
                        {
                            PerformMove(firstMove, secondMove);
                            Console.Clear();
                            ConsoleHelper.PrintBoard(currentBoard, DeadWhitePieces, DeadBlackPieces);
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

        public void PerformMove(int firstInput, int secondInput)
        {
            currentBoard.grid[firstInput].OccupyingPiece.HasMoved = true;
            if (currentBoard.grid[secondInput].OccupyingPiece != null)
            {
                if (currentBoard.grid[secondInput].OccupyingPiece.White)
                {
                    deadWhitePieces.Add(currentBoard.grid[secondInput].OccupyingPiece);
                }
                else
                {
                    deadBlackPieces.Add(currentBoard.grid[secondInput].OccupyingPiece);
                }
            }
            currentBoard.grid[secondInput].OccupyingPiece = currentBoard.grid[firstInput].OccupyingPiece;
            currentBoard.grid[firstInput].OccupyingPiece = null;
        }
    }
}
