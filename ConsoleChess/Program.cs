using System;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = ConsoleHelper.InitialMenu();
            game.StartGame();
        }
    }
}
