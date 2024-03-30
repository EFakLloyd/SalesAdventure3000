using System;

namespace SalesAdventure3000_UI.Controllers
{
    public static class MenuControl
    {
        public static (int command, bool confirmed) GetInput(int selectedCommand, int upperLimit)   //Simple navigation of start menu. Is called repeatedly while confirmedChoice is false.
        {
            Console.SetCursorPosition(GameDimensions.Width * 2-1, 0);
            bool confirmedChoice = false;
            string input = Console.ReadKey().Key.ToString();
            switch (input)
            {
                case "UpArrow":
                    if (selectedCommand > 0)
                        selectedCommand--;
                    break;
                case "DownArrow":
                    if (selectedCommand < upperLimit - 1)
                        selectedCommand++;
                    break;
                case "Enter":
                    confirmedChoice = true;
                    break;
            }
            return (selectedCommand, confirmedChoice);
        }
    }
}
