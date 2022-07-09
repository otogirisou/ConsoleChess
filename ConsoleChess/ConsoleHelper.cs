using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleChess
{
    static class ConsoleHelper
    {
        public static Game InitialMenu()
        {
            string userInput = "";
            do
            {
                Console.WriteLine("Enter 'new' to start a new game");
                Console.WriteLine("Enter 'load' to load previous game. Enter 'save' while playing to save");
                userInput = Console.ReadLine().Trim().ToLower();
            } while (userInput != "new" && userInput != "load");
            Console.Clear();
            Game game;
            if (userInput == "new")
            {
                game = new Game(new List<Piece>(), new List<Piece>());
                Board currentBoard = new Board();
                currentBoard.InitialSetup();
                game.CurrentBoard = currentBoard;
                game.WhitesTurn = true;
            }
            else
            {
                game = SaveLoad.Load();

            }
            ConsoleHelper.PrintBoard(game.CurrentBoard, game.DeadWhitePieces, game.DeadBlackPieces, -1, (bool)game.WhitesTurn, new List<int>());
            return game;
        }
        public static int GetIndexFromInput()
        {
            int firstPart;
            int secondPart;
            string userInput = Console.ReadLine().Trim().ToLower();
            if (userInput == "save")
            {
                return -2;
            }
            if (userInput.Length != 2)
            {
                return -1; //exceptions?
            }
            else
            {
                firstPart = ParseFirstChar(userInput[0]);
                if (firstPart == -1)
                {
                    return -1;
                }
                secondPart = ParseSecondChar(userInput[1]);
                if (secondPart == -1)
                {
                    return -1;
                }
                return firstPart + secondPart;

            }
        }

        private static int ParseFirstChar(char first)
        {
            switch (first)
            {
                case 'a':
                    return 0;
                case 'b':
                    return 1;
                case 'c':
                    return 2;
                case 'd':
                    return 3;
                case 'e':
                    return 4;
                case 'f':
                    return 5;
                case 'g':
                    return 6;
                case 'h':
                    return 7;
                default:
                    return -1;
            }
        }

        private static int ParseSecondChar(char second)
        {
            if (int.TryParse(second.ToString(), out var secondPart))
            {
                switch (secondPart)
                {
                    case 1:
                        return 0;
                    case 2:
                        return 8;
                    case 3:
                        return 16;
                    case 4:
                        return 24;
                    case 5:
                        return 32;
                    case 6:
                        return 40;
                    case 7:
                        return 48;
                    case 8:
                        return 56;
                    default:
                        return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public static void PrintBoard(Board board, List<Piece> deadWhitePieces, List<Piece> deadBlackPieces, int selectedPiece, bool whitesTurn, List<int> possibleMoves)
        {
            Console.Clear();
            for (int i = 0; i < 64; i++)
            {
                if (i % 8 == 0)
                {
                    Console.Write("{0} - ", (i / 8) + 1);
                }
                bool selected = false;
                if (i == selectedPiece || possibleMoves.Contains(i))
                {
                    selected = true;
                }
                PrintSpace(board.grid[i], selected);

                if ((i + 1) % 8 == 0)
                {
                    if (i == 7)
                    {
                        Console.Write("  Dead blue pieces: ");
                        PrintDeadPieces(deadWhitePieces);
                    }
                    else if (i == 15)
                    {
                        Console.Write("  Dead red pieces:  ");
                        PrintDeadPieces(deadBlackPieces);
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("     A B C D E F G H");
            PrintTurnDisplay(whitesTurn);
        }

        private static void PrintTurnDisplay(bool whitesTurn)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            if (whitesTurn)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("       Blues's turn ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("       Reds's turn  ");
            }
            Console.ResetColor();
        }

        private static void PrintSpace(Space space, bool selected)
        {
            if (selected)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if ((bool)space.WhiteSpace)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }
            char letter;
            if (space.OccupyingPiece != null)
            {
                letter = space.OccupyingPiece.Letter;
                if (space.OccupyingPiece.White)
                {
                    Console.ForegroundColor = ConsoleColor.Blue; // white is blue
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // black is red
                }
            }
            else
            {
                letter = ' ';
            }
            Console.Write(" {0}", letter);
            Console.ResetColor();
        }

        private static void PrintDeadPieces(List<Piece> deadPieces)
        {
            if (deadPieces.Count != 0)
            {
                if (deadPieces.First().White)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                foreach (Piece piece in deadPieces)
                {
                    Console.Write(" {0}", piece.Letter);
                }
            }
            Console.ResetColor();
        }

        public static void SaveGameAndExit(Game game)
        {
            SaveLoad.Save(game);
            Console.WriteLine("Game saved!");
            Console.WriteLine("Window closing in:");
            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine(i);
                Thread.Sleep(150);
            }
        }
    }
}
