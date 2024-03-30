using Engine;
using SalesAdventure3000_UI.Controllers;
using System;
using System.Collections.Generic;
using System.Threading;
using static SalesAdventure3000_UI.Views.ViewEnums;

namespace SalesAdventure3000_UI.Views
{
    public static class MenuView
    {
        private static int selectedIndex;
        private enum menuOptions
        {
            Continue,
            Save_Game,
            New_Game,
            Load_Game,
            Exit
        }

        public static View Display(Session currentSession)  //Displays start menu and handles input. Adjusts options based on whether a game is in progress.
        {
            View returnView = View.Start;   //Determines which View to transition to. 
            bool menuLoop = true;
            List<menuOptions> options = new() { menuOptions.New_Game, menuOptions.Exit };   //Options are added as they become relevant.

            if (currentSession.SaveGameExists)  
                options.Insert(1, menuOptions.Load_Game);
            if (currentSession.CurrentPlayer != null)
            {
                options.Insert(0, menuOptions.Continue);
                options.Insert(1, menuOptions.Save_Game);
            }

            while (menuLoop)
            {
                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\t    R\n" +
                                        "\t   E\n" +
                                        "\t  P    SalesAdventure 3000\n" +
                                        "\t U\n" +
                                        "\tS\n\n" +
                                        "".PadRight(GameDimensions.Width * 2, '-') + "\n\n");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    for (int i = 0; i < options.Count; i++)
                    {
                        string[] selection = i == selectedIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
                        Console.Write($"\t{selection[0]}{options[i].ToString().Replace('_', ' ')}{selection[1]}\n\n");  //Format enum to string.
                    }

                    var input = MenuControl.GetInput(selectedIndex, options.Count);
                    if (input.confirmed)
                        break;
                    selectedIndex = input.command;
                }
                Console.Clear();

                switch (options[selectedIndex])     //selected options is used for switch.
                {
                    case menuOptions.Continue:
                        menuLoop = false;
                        returnView = View.Adventure;
                        break;
                    case menuOptions.Save_Game:
                        Console.Write("\n\tGame Saved.");
                        currentSession.SaveSession();
                        returnView = View.Start;
                        menuLoop = true;
                        break;
                    case menuOptions.New_Game:
                        Console.Write("\n\tEnter your name, brave adventurer: ");
                        string adventurerName = Console.ReadLine();
                        int adventurerAvatar = chooseAvatar();      //Helper method to select avatar.
                        Console.WriteLine($"\n\tReady yourself, brave {adventurerName}!");
                        currentSession.StartNewSession(adventurerName, adventurerAvatar);
                        menuLoop = false;
                        returnView = View.Adventure;
                        break;
                    case menuOptions.Load_Game:
                        Console.Write("\n\tLoading game. Ready yourself.");
                        currentSession.LoadSession();
                        menuLoop = false;
                        returnView = View.Adventure;
                        break;
                    case menuOptions.Exit:
                        Console.Write("\n\tExiting game.");
                        menuLoop = false;
                        returnView = View.Exit;
                        break;
                }
                Thread.Sleep(1400);
                Console.Clear();
            }
            return returnView;  

            #region Helper Methods
            int chooseAvatar()
            {
                int index = 0;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"\n\n\tChoose your avatar:\n");

                    foreach (string line in currentSession.Avatars[index])
                        Console.WriteLine($"{line}".PadLeft(17 + line.Length / 2));

                    string input = Console.ReadKey().Key.ToString();
                    if (input == "Enter")
                        return index;
                    else
                        index = index == 0 ? 2 : 0;
                }
            }
            #endregion
        }
    }
}
