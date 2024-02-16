using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAdventure3000_UI.Controllers
{
    public class PlayerInventoryControl
    {
        public static (int, bool) GetInput(int selectedCommand, int upperLimit)
        {
            bool confirmedChoice = false;
            string input = Console.ReadKey().Key.ToString();
            switch (input)
            {
                case "LeftArrow":
                    if (selectedCommand > 0)
                        selectedCommand--;
                    break;
                case "RightArrow":
                    if (selectedCommand < upperLimit - 1)
                        selectedCommand++;
                    break;
                case "DownArrow":
                    if (selectedCommand + 2 < upperLimit - 1)
                        selectedCommand = selectedCommand + 2;
                    break;
                case "UpArrow":
                    if (selectedCommand - 2 > 0)
                        selectedCommand = selectedCommand - 2;
                    break;
                case "Enter":
                    confirmedChoice = true;
                    break;
            }
            return (selectedCommand, confirmedChoice);
        }
    }
}
