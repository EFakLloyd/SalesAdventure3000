using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SalesAdventure3000_UI.Views.AdventureView;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class BattleMenuWindow
    {
        public static void DrawBattleMenu(Actions currentAction, int menuIndex)
        {
            int width = 42;
            string[] battleOptions = new string[] { "Attack", "Reckless Attack", "Use Item", "Flee" };

            Console.SetCursorPosition(0, 24);
            Console.ForegroundColor = currentAction == Actions.ContinueFight ? ConsoleColor.Cyan : ConsoleColor.Gray;
            Console.WriteLine("╔".PadRight(width * 2 - 1, '═') + "╗");
            for (int i = 0; i < battleOptions.Length; i++)
            {
                string[] selection = i == menuIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
                if (i % 2 == 0)
                    Console.Write($"║ {selection[0]}{battleOptions[i]}{selection[1]}".PadRight(width - 1, ' '));
                else
                    Console.Write($"{selection[0]}{battleOptions[i]}{selection[1]}".PadRight(width, ' ') + "║\n");
                if (i % 2 == 0 && i == battleOptions.Length - 1)
                    Console.Write("".PadRight(width, ' ') + "║\n");
            }
            Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            Console.ResetColor();
        }
    }
}
