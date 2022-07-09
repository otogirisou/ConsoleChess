using System;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = GetGame();
            game.StartGame();
        }

        private static Game GetGame() //to add modularity
        {
            return ConsoleHelper.InitialMenu();
        }
    }
}
