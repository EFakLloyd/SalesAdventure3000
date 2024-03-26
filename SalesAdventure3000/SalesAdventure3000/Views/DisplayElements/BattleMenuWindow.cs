using System;
using static SalesAdventure3000_UI.Views.AdventureView;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class BattleMenuWindow
    {
        public static void Draw(ViewEnums.Actions currentAction, int menuIndex)
        {
            string[] battleOptions = new string[] { "Attack", "Reckless Attack", "Use Item", "Flee" };  //Player options in battle.

            Console.SetCursorPosition(0, 24);
            Console.ForegroundColor = currentAction == ViewEnums.Actions.Fight ? ConsoleColor.Cyan : ConsoleColor.Gray; //Checks if the window is active.
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
