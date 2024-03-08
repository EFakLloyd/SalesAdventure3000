using System;
using static SalesAdventure3000_UI.Views.AdventureView;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class BattleMenuWindow
    {
        public static void Draw(Actions currentAction, int menuIndex)
        {
            string[] battleOptions = new string[] { "Attack", "Reckless Attack", "Use Item", "Flee" };

            Console.SetCursorPosition(0, 24);
            Console.ForegroundColor = currentAction == Actions.ContinueFight ? ConsoleColor.Cyan : ConsoleColor.Gray;
            Console.WriteLine("╔".PadRight(GameDimensions.Width * 2 - 1, '═') + "╗");
            for (int i = 0; i < battleOptions.Length; i++)
            {
                string[] selection = i == menuIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
                if (i % 2 == 0)
                    Console.Write($"║ {selection[0]}{battleOptions[i]}{selection[1]}".PadRight(GameDimensions.Width - 1, ' '));
                else
                    Console.Write($"{selection[0]}{battleOptions[i]}{selection[1]}".PadRight(GameDimensions.Width, ' ') + "║\n");
                if (i % 2 == 0 && i == battleOptions.Length - 1)
                    Console.Write("".PadRight(GameDimensions.Width, ' ') + "║\n");
            }
            Console.WriteLine("╚".PadRight(GameDimensions.Width * 2 - 1, '═') + "╝");
            Console.ResetColor();
        }
    }
}
