using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    static class ConsoleHelper
    {
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
                if(firstPart == -1)
                {
                    return -1;
                }
                secondPart = ParseSecondChar(userInput[1]);
                if(secondPart == -1)
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
            int secondPart;
            string secondChar = second.ToString();
            if (int.TryParse(secondChar, out secondPart))
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

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < 64; i++)
            {
                if (i % 8 == 0)
                {
                    Console.Write("{0} - ", (i / 8) + 1);
                }
                if (board.grid[i].white)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                char letter;
                if (board.grid[i].occupyingPiece != null)
                {
                    letter = board.grid[i].occupyingPiece.Letter;
                    if (board.grid[i].occupyingPiece.White)
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
                if ((i+1)%8 == 0)
                {
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
            Console.WriteLine("     A B C D E F G H");
        }
    }
}
