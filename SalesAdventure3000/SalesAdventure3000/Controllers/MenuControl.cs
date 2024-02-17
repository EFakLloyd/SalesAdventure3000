﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAdventure3000_UI.Controllers
{
    public class MenuControl
    {
        public static (int val, bool enter) GetInput(int selectedCommand, int upperLimit)
        {
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