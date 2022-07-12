using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    static class Rules
    {
        public static List<int> PossibleMoves(int firstInput, Board board)
        {
            List<int> possibleMoves = new List<int>();
            for (int i = 0; i < 64; i++)
            {
                if (CheckMove(firstInput, i, board))
                {
                    possibleMoves.Add(i);
                }
            }
            return possibleMoves;
        }
        public static bool CheckMove(int firstInput, int secondInput, Board board) //true means valid
        {
            switch (board.grid[firstInput].OccupyingPiece.PieceTypePublic)
            {
                case PieceType.Pawn:
                    return CheckPawn(firstInput, secondInput, board);
                case PieceType.Tower:
                    return CheckTower(firstInput, secondInput, board);
                case PieceType.Knight:
                    return CheckKnight(firstInput, secondInput, board);
                case PieceType.Bishop:
                    return CheckBishop(firstInput, secondInput, board);
                case PieceType.King:
                    return CheckKing(firstInput, secondInput, board);
                case PieceType.Queen:
                    return CheckQueen(firstInput, secondInput, board);
                default:
                    return false;
            }
        }

        public static bool CheckCheck(int firstInput, int secondInput, Board board, bool whitesTurn) //true means no check
        {
            Game tempGame = new Game(new List<Piece>(), new List<Piece>());
            tempGame.CurrentBoard = board.Clone();
            tempGame.PerformMove(firstInput, secondInput);
            for (int kingIndex = 0; kingIndex < 64; kingIndex++)
            {
                if (tempGame.CurrentBoard.grid[kingIndex].OccupyingPiece != null)
                {
                    if (tempGame.CurrentBoard.grid[kingIndex].OccupyingPiece.PieceTypePublic == PieceType.King && tempGame.CurrentBoard.grid[kingIndex].OccupyingPiece.White == whitesTurn)
                    {
                        for (int i = 0; i < 64; i++)
                        {
                            if (tempGame.CurrentBoard.grid[i].OccupyingPiece != null)
                            {
                                if (CheckMove(i, kingIndex, tempGame.CurrentBoard))
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                }
            }
            return false; //unreachable i think
        }

        private static bool CheckPawn(int firstInput, int secondInput, Board board)
        {
            if (board.grid[secondInput].OccupyingPiece == null) //maybe switches here?
            {
                if (board.grid[firstInput].OccupyingPiece.White) //checks here because white pawns move up and black pawns move down
                {
                    if (secondInput + 8 == firstInput  //dest is one up
                        || (secondInput + 16 == firstInput && !(bool)board.grid[firstInput].OccupyingPiece.HasMoved && board.grid[secondInput].OccupyingPiece == null)) //dest is two up and the pawn hasnt moved yet
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (secondInput == firstInput + 8 //dest is one down
                        || (secondInput == firstInput + 16 && !(bool)board.grid[firstInput].OccupyingPiece.HasMoved && board.grid[firstInput + 8].OccupyingPiece == null)) //dest is two down and the pawn hasnt moved yet
                    {
                        return true;
                    }
                    return false;
                }
            }
            else
            {
                if (board.grid[firstInput].OccupyingPiece.White == board.grid[secondInput].OccupyingPiece.White)
                {
                    return false;
                }
                else
                {
                    if (board.grid[firstInput].OccupyingPiece.White)
                    {
                        if ((firstInput == secondInput + 7 && (firstInput + 1) % 8 != 0) || (firstInput == secondInput + 9 && firstInput % 8 != 0)) //dest is diag-up left or right
                        {
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        if ((secondInput == firstInput + 7 && (firstInput + 1) % 8 != 0) || (secondInput == firstInput + 9 && firstInput % 8 != 0)) //dest is diag-down left or right
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }
        private static bool CheckTower(int firstInput, int secondInput, Board board)
        {
            //towering probably by editing here
            if (board.grid[secondInput].OccupyingPiece != null)
            {
                if (board.grid[firstInput].OccupyingPiece.White == board.grid[secondInput].OccupyingPiece.White)
                {
                    return false;
                }
            }
            //^^^^
            if (secondInput / 8 == firstInput / 8) //same row
            {
                int horrizontalTower = 1;
                if (secondInput < firstInput)
                {
                    while (firstInput - horrizontalTower != secondInput) //check in between current and dest
                    {
                        if (board.grid[firstInput - horrizontalTower].OccupyingPiece != null)
                        {
                            return false;
                        }
                        horrizontalTower++;
                    }
                }
                else
                {
                    while (firstInput + horrizontalTower != secondInput) //check in between current and dest
                    {
                        if (board.grid[firstInput + horrizontalTower].OccupyingPiece != null)
                        {
                            return false;
                        }
                        horrizontalTower++;
                    }
                }
                if (board.grid[secondInput].OccupyingPiece == null)
                {
                    return true;
                }
                if (board.grid[firstInput].OccupyingPiece.White != board.grid[secondInput].OccupyingPiece.White)
                {
                    return true;
                }
            }
            else if (secondInput % 8 == firstInput % 8) //same col
            {
                int verticalTower = 1;
                if (secondInput < firstInput)
                {
                    while (firstInput - (verticalTower * 8) != secondInput) //check in between current and dest
                    {
                        if (board.grid[firstInput - (verticalTower * 8)].OccupyingPiece != null)
                        {
                            return false;
                        }
                        verticalTower++;
                    }
                }
                else
                {
                    while (firstInput + (verticalTower * 8) != secondInput) //check in between current and dest
                    {
                        if (board.grid[firstInput + (verticalTower * 8)].OccupyingPiece != null)
                        {
                            return false;
                        }
                        verticalTower++;
                    }
                }
                if (board.grid[secondInput].OccupyingPiece == null)
                {
                    return true;
                }
                if (board.grid[firstInput].OccupyingPiece.White != board.grid[secondInput].OccupyingPiece.White)
                {
                    return true;
                }
            }
            return false;

        }
        private static bool CheckBishop(int firstInput, int secondInput, Board board)
        {
            if (board.grid[secondInput].OccupyingPiece != null)
            {
                if (board.grid[firstInput].OccupyingPiece.White == board.grid[secondInput].OccupyingPiece.White)
                {
                    return false;
                }
            }
            if (secondInput < firstInput)
            {
                int rowsToMove = firstInput / 8 - secondInput / 8;
                if (firstInput % 8 == (secondInput % 8) + rowsToMove || (firstInput % 8) + rowsToMove == (secondInput % 8)) //check if its diagonal
                {
                    if (secondInput % 8 < firstInput % 8)
                    {
                        for (int i = 1; i < rowsToMove; i++) //check all spaces in between current and dest
                        {
                            if (board.grid[firstInput - 7 * i].OccupyingPiece != null)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < rowsToMove; i++) //check all spaces in between current and dest
                        {
                            if (board.grid[firstInput - 9 * i].OccupyingPiece != null)
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                return false;
            }
            else
            {
                int rowsToMove = secondInput / 8 - firstInput / 8;
                if (firstInput % 8 == (secondInput % 8) + rowsToMove || (firstInput % 8) + rowsToMove == (secondInput % 8))
                {
                    if (secondInput % 8 < firstInput % 8)
                    {
                        for (int i = 1; i < rowsToMove; i++) //check all spaces in between current and dest
                        {
                            if (board.grid[firstInput + 7 * i].OccupyingPiece != null)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < rowsToMove; i++) //check all spaces in between current and dest
                        {
                            if (board.grid[firstInput + 9 * i].OccupyingPiece != null)
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                return false;
            }
        }
        private static bool CheckKnight(int firstInput, int secondInput, Board board)
        {
            if (board.grid[secondInput].OccupyingPiece != null)
            {
                if (board.grid[firstInput].OccupyingPiece.White == board.grid[secondInput].OccupyingPiece.White)
                {
                    return false;
                }
            }
            if (secondInput % 8 == 1 + firstInput % 8 || 1 + secondInput % 8 == firstInput % 8) //dest is one to either side
            {
                if (2 + secondInput / 8 == firstInput / 8 || 2 + firstInput / 8 == secondInput / 8) //dest is two up or down
                {
                    return true;
                }
            }
            else if (1 + secondInput / 8 == firstInput / 8 || 1 + firstInput / 8 == secondInput / 8) //dest is one up or down
            {
                if (secondInput % 8 == 2 + firstInput % 8 || 2 + secondInput % 8 == firstInput % 8) //dest is two to either side
                {
                    return true;
                }
            }
            return false;
        }
        private static bool CheckKing(int firstInput, int secondInput, Board board)
        {
            if (board.grid[secondInput].OccupyingPiece != null)
            {
                if (board.grid[firstInput].OccupyingPiece.White == board.grid[secondInput].OccupyingPiece.White)
                {
                    return false;
                }
            }
            if ((secondInput == firstInput + 1 && firstInput % 8 < 7) //dest is one to the right
                || (secondInput + 1 == firstInput && firstInput % 8 > 0) //dest is one to the left
                || secondInput + 8 == firstInput //dest is one up
                || secondInput == firstInput + 8 //dest is one down
                || (secondInput == firstInput + 9 && firstInput % 8 < 7) //dest is diagonal down right
                || (secondInput + 9 == firstInput && firstInput % 8 > 0) //dest is diagonal up left
                || (secondInput == firstInput + 7 && firstInput % 8 > 0) //dest is diagonal down left
                || (secondInput + 7 == firstInput && firstInput % 8 < 7)) //dest is diagonal up right
            {
                return true;
            }

            return false;
        }
        private static bool CheckQueen(int firstInput, int secondInput, Board board)
        {
            if (CheckKing(firstInput, secondInput, board) || CheckTower(firstInput, secondInput, board) || CheckBishop(firstInput, secondInput, board)) //queens moveset is a combination of these three
            {
                return true;
            }
            return false;
        }
    }
}
