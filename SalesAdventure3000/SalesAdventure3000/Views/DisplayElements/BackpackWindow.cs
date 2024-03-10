using Engine.Models;
using System;
using System.Collections.Generic;
using static SalesAdventure3000_UI.Views.AdventureView;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class BackpackWindow
    {
        public static void Draw(Actions currentAction, List<Item> backpack, int backpackIndex)
        {
            Console.SetCursorPosition(0, 28);
            Console.ForegroundColor = currentAction == Actions.OpenBackpack ? ConsoleColor.Cyan : ConsoleColor.Gray;
            Console.WriteLine("╔═BACKPACK═[B]".PadRight(GameDimensions.Width * 2 - 1, '═') + "╗");

            for (int i = 0; i < 8; i++)
            {
                if (i < backpack.Count)
                {
                    string[] selection = i == backpackIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
                    if (i % 2 == 0)
                        Console.Write($"║ {selection[0]}{backpack[i].GetName()}{selection[1]}".PadRight(GameDimensions.Width - 1, ' '));
                    else
                        Console.Write($"{selection[0]}{backpack[i].GetName()}{selection[1]}".PadRight(GameDimensions.Width, ' ') + "║\n");
                    if (i % 2 == 0 && i == backpack.Count - 1)
                        Console.Write("".PadRight(GameDimensions.Width, ' ') + "║\n");
                }
                else
                    if (i % 2 == 0)
                        Console.Write($"║".PadRight(GameDimensions.Width * 2 - 1) + "║\n");
            }
            Console.WriteLine("╚".PadRight(GameDimensions.Width * 2 - 1, '═') + "╝");
            Console.ResetColor();
        }
    }
}
