using Battleships.Core;
using Battleships.Core.Grid;
using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var gridManager = new GridManager();
            var g = new Game(gridManager);
                g.Init();
            Console.WriteLine("Game starts. You can see ships if you type 'cheats' into target.");
            bool isCheatEnabled = false;

            do
            {
                Console.WriteLine(g.PrintStatus(isCheatEnabled));
                Console.Write("Your target: ");
                string target = Console.ReadLine().ToUpper();
                if (target == "CHEATS")
                {
                    isCheatEnabled = true;
                    Console.WriteLine("Cheat mode is enabled! You can see ships now (D - destroyer, B - battleship)");
                }
                else
                {
                    var response = g.Shoot(target);
                    if (response.IsSuccess)
                        Console.WriteLine(response.StatusDescription);
                    else
                        Console.WriteLine("Error: " + response.Error);
                }
            } while (!g.IsGameFinished());
            
            Console.WriteLine(g.PrintStatus(true));
            Console.Write("Game is finished! All ships are sunk!!!");
            Console.ReadKey();
        }
    }
}
