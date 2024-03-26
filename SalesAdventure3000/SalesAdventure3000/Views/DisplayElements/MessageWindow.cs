using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class MessageWindow
    {
        public static void Draw(List<string> messages)  //Displays the latest three strings in GameMessages.
        {
            Console.SetCursorPosition(0, 19);
            Console.WriteLine("╔═MESSAGES".PadRight(GameDimensions.Width * 2 - 1, '═') + "╗");
            for (int i = 3; i > 0; i--)
            {
                if (i <= messages.Count())
                {
                    Console.Write($"║ ");
                    Console.ForegroundColor = i == 1 ? ConsoleColor.Cyan : ConsoleColor.Gray;
                    Console.Write($"{messages[messages.Count() - i]}".PadRight(GameDimensions.Width * 2 - 3, ' '));
                    Console.ResetColor();
                    Console.Write("║\n");
                }
                else
                    Console.Write($"║".PadRight(GameDimensions.Width * 2 - 1) + "║\n");
            }
            Console.WriteLine("╚".PadRight(GameDimensions.Width * 2 - 1, '═') + "╝");
        }
    }
}
