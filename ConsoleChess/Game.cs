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
            int firstMove;
            int secondMove;
            while (true)
            {
                firstMove = GetFirstMove();
                secondMove = GetSecondMove(firstMove);

                PerformMove(firstMove, secondMove);
                whitesTurn = !whitesTurn;
                DisplayBoard(currentBoard, DeadWhitePieces, DeadBlackPieces, -1, (bool)whitesTurn, new List<int>());

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

        private void DisplayMessage(string message) //to add modularity
        {
            Console.WriteLine(message);
        }

        private void DisplayBoard(Board board, List<Piece> deadWhitePieces, List<Piece> deadBlackPieces, int selectedPiece, bool whitesTurn, List<int> possibleMoves) //to add modularity
        {
            ConsoleHelper.PrintBoard(board, deadWhitePieces, deadBlackPieces, selectedPiece, whitesTurn, possibleMoves);
        }

        private int GetIndexFromOutput() //to add modularity
        {
            return ConsoleHelper.GetIndexFromInput();
        }

        private int GetFirstMove()
        {
            while (true)
            {
                DisplayMessage("Where is the piece you want to move? (A-H)(1-8)");
                int firstMove = GetIndexFromOutput();
                if (firstMove == -1)
                {
                    DisplayMessage("Not a valid input! Try again");
                }
                else if (firstMove == -2)
                {
                    SaveGameAndExit();
                }
                else if (currentBoard.grid[firstMove].OccupyingPiece == null)
                {
                    DisplayBoard(currentBoard, deadWhitePieces, deadBlackPieces, -1, (bool)whitesTurn, new List<int>());
                    DisplayMessage("There is no piece to move there! Try again");
                }
                else if (currentBoard.grid[firstMove].OccupyingPiece.White != whitesTurn) //dont have to check elsewhere but i think currently it does...
                {
                    DisplayBoard(currentBoard, deadWhitePieces, deadBlackPieces, -1, (bool)whitesTurn, new List<int>());
                    DisplayMessage("The piece there is not yours! Try again");
                }
                else
                {
                    return firstMove;
                }
            }
        }

        private int GetSecondMove(int firstMove)
        {
            while(true)
            {
                DisplayBoard(currentBoard, deadWhitePieces, deadBlackPieces, firstMove, (bool)whitesTurn, Rules.PossibleMoves(firstMove, currentBoard));
                DisplayMessage("Where do you want to move? (A - H)(1 - 8)");
                int secondMove = GetIndexFromOutput();
                if (secondMove == -1)
                {
                    DisplayBoard(currentBoard, deadWhitePieces, deadBlackPieces, -1, (bool)whitesTurn, new List<int>());
                    DisplayMessage("Not a valid input! Try again");
                }
                else if (secondMove == -2)
                {
                    SaveGameAndExit();
                }
                else if (Rules.CheckMove(firstMove, secondMove, currentBoard))
                {
                    if (!Rules.CheckCheck(firstMove, secondMove, currentBoard, (bool)whitesTurn))
                    {
                        DisplayBoard(currentBoard, deadWhitePieces, deadBlackPieces, -1, (bool)whitesTurn, new List<int>());
                        DisplayMessage("That move would leave your king checked! Try again");
                    }
                    else
                    {
                        return secondMove;
                    }
                }
                else
                {
                    DisplayBoard(currentBoard, deadWhitePieces, deadBlackPieces, -1, (bool)whitesTurn, new List<int>());
                    DisplayMessage("Not a valid move! Try again");
                }
                firstMove = GetFirstMove();
            }
        }

        private void SaveGameAndExit() //to add modularity
        {
            ConsoleHelper.SaveGameAndExit(this);
            Environment.Exit(0);
        }
    }
}
