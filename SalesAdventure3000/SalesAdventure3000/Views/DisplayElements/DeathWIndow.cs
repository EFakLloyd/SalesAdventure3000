using System;
using System.Threading;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    public static class DeathWindow
    {
        public static void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 15; i++) 
            {
                Console.Clear();
                for (int j = 0; j < i; j++) 
                {
                    Console.WriteLine();
                }
                Console.WriteLine("\t\t\tYou are dead.");
                Thread.Sleep(200);
            }
            Console.ReadKey();
        }
    }
}
