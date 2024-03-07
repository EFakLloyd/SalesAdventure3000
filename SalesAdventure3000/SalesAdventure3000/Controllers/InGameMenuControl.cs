using System;

namespace SalesAdventure3000_UI.Controllers
{
    public class InGameMenuControl
    {
        public static (int selectedIndex, bool confirmedChoice, bool stayInLoop) GetInput(int index, int upperLimit)
        {
            bool confirmedChoice = false;
            bool stayInLoop = true;
            string input = Console.ReadKey().Key.ToString();
            switch (input)
            {
                case "LeftArrow":
                    if (index > 0)
                        index--;
                    break;
                case "RightArrow":
                    if (index + 1 < upperLimit)
                        index++;
                    break;
                case "DownArrow":
                    if (index + 2 < upperLimit)
                        index += 2;
                    break;
                case "UpArrow":
                    if (index - 2 > -1)
                        index -= 2;
                    break;
                case "Enter":
                    confirmedChoice = true;
                    break;
                case "Escape":
                    stayInLoop = false;
                    break;
            }
            return (index, confirmedChoice, stayInLoop);
        }
    }
}
