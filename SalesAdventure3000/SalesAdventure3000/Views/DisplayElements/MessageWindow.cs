using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class MessageWindow
    {
        public static void Draw(List<string> messages)
        {
            int width = 42;

            Console.SetCursorPosition(0, 19);
            Console.WriteLine("╔═MESSAGES".PadRight(width * 2 - 1, '═') + "╗");
            for (int i = 3; i > 0; i--)
            {
                if (i <= messages.Count())
                {
                    Console.Write($"║ ");
                    Console.ForegroundColor = i == 1 ? ConsoleColor.Cyan : ConsoleColor.Gray;
                    Console.Write($"{messages[messages.Count() - i]}".PadRight(width * 2 - 3, ' '));
                    Console.ResetColor();
                    Console.Write("║\n");
                }
                else
                    Console.Write($"║".PadRight(width*2 - 1) + "║\n");
            }
            Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
        }
    }
}
