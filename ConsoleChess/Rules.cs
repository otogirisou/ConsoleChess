using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    static class Rules
    {
        public static bool CheckMove(int firstInput, int secondInput, Board board, bool whitesTurn) //true means valid
        {
            if(board.grid[firstInput].occupyingPiece == null)
            {
                return false;
            }
            else
            {
                if (board.grid[firstInput].occupyingPiece.White != whitesTurn)
                {
                    return false;
                }
                switch (board.grid[firstInput].occupyingPiece.PieceTypePublic)
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
        }

        public static bool CheckCheck(int firstInput, int secondInput, Board board, bool whitesTurn)
        {
            Board tempCopy = board.Clone();
            Game.PerformMove(firstInput, secondInput, tempCopy);
            for (int kingIndex = 0; kingIndex < 64; kingIndex++)
            {
                if (tempCopy.grid[kingIndex].occupyingPiece != null)
                {
                    if (tempCopy.grid[kingIndex].occupyingPiece.PieceTypePublic == PieceType.King && tempCopy.grid[kingIndex].occupyingPiece.White == whitesTurn)
                    {
                        for (int i = 0; i < 64; i++)
                        {
                            if (CheckMove(i, kingIndex, tempCopy, whitesTurn))
                            {
                                return false;
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
            if (board.grid[secondInput].occupyingPiece == null) //maybe switches here?
            {
                if (board.grid[firstInput].occupyingPiece.White)
                {
                    if (secondInput + 8 == firstInput || (secondInput + 16 == firstInput && !board.grid[firstInput].occupyingPiece.HasMoved && board.grid[secondInput].occupyingPiece == null))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (secondInput == firstInput + 8 || (secondInput == firstInput + 16 && !board.grid[firstInput].occupyingPiece.HasMoved && board.grid[firstInput+8].occupyingPiece == null))
                    {
                        return true;
                    }
                    return false;
                }
            }
            else
            {
                if (board.grid[firstInput].occupyingPiece.White == board.grid[secondInput].occupyingPiece.White)
                {
                    return false;
                }
                else
                {
                    if (board.grid[firstInput].occupyingPiece.White)
                    {
                        if ((firstInput == secondInput + 7 && (firstInput + 1) % 8 != 0) || (firstInput == secondInput + 9 && firstInput % 8 != 0))
                        {
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        if ((secondInput == firstInput + 7 && (firstInput + 1) % 8 != 0) || (secondInput == firstInput + 9 && firstInput % 8 != 0))
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
            //towering probably here

            if (secondInput/8 == firstInput/8) //same row
            {
                int horrizontalTower = 1;
                if (secondInput%8 < firstInput%8)
                {
                    while (firstInput-horrizontalTower != secondInput)
                    {
                        if (board.grid[firstInput-horrizontalTower] != null)
                        {
                            return false;
                        }
                        horrizontalTower++;
                    }
                }
                else
                {
                    while (firstInput+horrizontalTower != secondInput)
                    {
                        if (board.grid[firstInput+horrizontalTower] != null)
                        {
                            return false;
                        }
                        horrizontalTower++;
                    }
                }
            }
            else
            {
                if (secondInput%8 == firstInput%8)
                {
                    int verticalTower = 1;
                    if (secondInput < firstInput)
                    {
                        while (firstInput - (verticalTower * 8) != secondInput)
                        {
                            if (board.grid[firstInput - (verticalTower * 8)].occupyingPiece != null)
                            {
                                return false;
                            }
                            verticalTower++;
                        }
                    }
                    else
                    {
                        while (firstInput + (verticalTower * 8) != secondInput)
                        {
                            if (board.grid[firstInput + (verticalTower * 8)].occupyingPiece != null)
                            {
                                return false;
                            }
                            verticalTower++;
                        }
                    }
                }
            }
            if (board.grid[secondInput].occupyingPiece == null)
            {
                return true;
            }
            if (board.grid[firstInput].occupyingPiece.White != board.grid[secondInput].occupyingPiece.White)
            {
                return true;
            }
            return false;

        }
        private static bool CheckBishop(int firstInput, int secondInput, Board board)
        {
            if (board.grid[firstInput].occupyingPiece.White == board.grid[secondInput].occupyingPiece.White)
            {
                return false;
            }
            if (secondInput < firstInput)
            {
                int rowsToMove = firstInput / 8 - secondInput / 8;
                if (firstInput%8 == (secondInput%8)+rowsToMove || (firstInput%8)+rowsToMove == (secondInput%8)) 
                {
                    if (secondInput%8 < firstInput%8)
                    {
                        for (int i = 1; i < rowsToMove; i++)
                        {
                            if (board.grid[firstInput-7*i].occupyingPiece != null)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < rowsToMove; i++)
                        {
                            if (board.grid[firstInput - 9 * i].occupyingPiece != null)
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
                if (firstInput%8 == (secondInput%8)+rowsToMove ||(firstInput%8)+rowsToMove == (secondInput%8))
                {
                    if (secondInput%8 < firstInput%8)
                    {
                        for (int i = 1; i < rowsToMove; i++)
                        {
                            if (board.grid[firstInput+7*i].occupyingPiece != null)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < rowsToMove; i++)
                        {
                            if (board.grid[firstInput + 9 * i].occupyingPiece != null)
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
            if (board.grid[firstInput].occupyingPiece.White == board.grid[secondInput].occupyingPiece.White)
            {
                return false;
            }
            if (((secondInput % 8 == (firstInput % 8) + 1 || (secondInput % 8) + 1 == firstInput % 8) && ((secondInput / 8) + 2 == firstInput / 8) || (firstInput / 8) + 2 == secondInput / 8)
                        || ((secondInput % 8 == (firstInput % 8) + 2 || (secondInput % 8) + 2 == firstInput % 8) && ((secondInput / 8) + 1 == firstInput / 8) || (firstInput / 8) + 1 == secondInput / 8))
            {
                return true;
            }
            return false;
        }
        private static bool CheckKing(int firstInput, int secondInput, Board board)
        {
            if (board.grid[firstInput].occupyingPiece.White == board.grid[secondInput].occupyingPiece.White)
            {
                return false;
            }
            if ((secondInput == firstInput + 1 && firstInput % 8 < 7) || (secondInput + 1 == firstInput && firstInput % 8 > 0) || secondInput + 8 == firstInput || secondInput == firstInput + 8
                            || (secondInput == firstInput + 9 && firstInput % 8 < 7) || (secondInput + 9 == firstInput && firstInput % 8 > 0) || (secondInput == firstInput + 7 && firstInput % 8 > 0)
                            || (secondInput + 7 == firstInput && firstInput % 8 < 7))
            {
                return true;
            }
            return false;
        }
        private static bool CheckQueen(int firstInput, int secondInput, Board board)
        {
            if (CheckKing(firstInput, secondInput, board) || CheckTower(firstInput, secondInput, board) || CheckBishop(firstInput, secondInput, board))
            {
                return true;
            }
            return false;
        }
    }
}
