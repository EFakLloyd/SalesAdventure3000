﻿using SalesAdventure3000_UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAdventure3000_UI.Controllers
{
    public class PlayerInventoryControl
    {
        public static (int selectedCommand, bool confirmedChoice, bool stayInLoop) GetInput(int selectedCommand, int upperLimit)
        {
            bool confirmedChoice = false;
            bool stayInLoop = true;
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
                case "Esc":
                    confirmedChoice = true;
                    stayInLoop = false;
                    break;
            }
            return (selectedCommand, confirmedChoice, stayInLoop);
        }
    }
}
