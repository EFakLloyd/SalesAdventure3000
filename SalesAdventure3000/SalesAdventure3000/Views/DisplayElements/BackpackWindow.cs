using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SalesAdventure3000_UI.Views.AdventureView;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class BackpackWindow
    {
        public static void DrawBackpack(Actions currentAction, List<Item> backpack, int backpackIndex)
        {
            int width = 42;

            Console.SetCursorPosition(0, 28);
            Console.ForegroundColor = currentAction == Actions.OpenBackpack ? ConsoleColor.Cyan : ConsoleColor.Gray;
            Console.WriteLine("╔═BACKPACK═[B]".PadRight(width * 2 - 1, '═') + "╗");
            for (int i = 0; i < backpack.Count; i++)
            {
                string[] selection = i == backpackIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
                if (i % 2 == 0)
                    Console.Write($"║ {selection[0]}{backpack[i].GetName()}{selection[1]}".PadRight(width - 1, ' '));
                else
                    Console.Write($"{selection[0]}{backpack[i].GetName()}{selection[1]}".PadRight(width, ' ') + "║\n");
                if (i % 2 == 0 && i == backpack.Count - 1)
                    Console.Write("".PadRight(width, ' ') + "║\n");
            }
            Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            Console.ResetColor();
        }
    }
}
