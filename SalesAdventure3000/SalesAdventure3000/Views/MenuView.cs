using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using System;
using System.Collections.Generic;
using System.Threading;
using static SalesAdventure3000_UI.Views.ViewType;

namespace SalesAdventure3000_UI.Views
{
    public class MenuView
    {
        public static int SelectedCommand { get; set; }

        public static View Display(Session currentSession)
        {
            View returnView = View.Start;
            List<string> options = new List<string>(new string[] { "New game", "Load game", "Exit" });
            if (currentSession.CurrentPlayer != null)
            {
                options.Insert(0, "Continue");
                options.Insert(1, "Save Game");
            }

            bool menuLoop = true;

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
                                        "".PadRight(42 * 2, '-') + "\n\n");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    for (int i = 0; i < options.Count; i++)
                    {
                        string[] selection = i == SelectedCommand ? new string[] { "[", "]" } : new string[] { " ", " " };
                        Console.Write($"\t{selection[0]}{options[i]}{selection[1]}\n\n");
                    }

                    var input = MenuControl.GetInput(SelectedCommand, options.Count);
                    if (input.enter)
                        break;
                    SelectedCommand = input.val;
                }
                Console.Clear();
                switch (SelectedCommand + (currentSession.CurrentPlayer != null ? -2 : 0))
                {
                    case -2:
                        menuLoop = false;
                        returnView = View.Adventure;
                        break;
                    case -1:
                        Console.Write("\n\tGame Saved.");
                        currentSession.SaveSession();
                        returnView = View.Start;
                        menuLoop = true;
                        break;
                    case 0:
                        Console.Write("\n\tEnter your name, brave adventurer: ");
                        string adventurerName = Console.ReadLine();
                        int adventurerAvatar = chooseAvatar();
                        Console.WriteLine($"\n\tReady yourself, brave {adventurerName}!");
                        currentSession.StartNewSession(adventurerName, adventurerAvatar);           
                        menuLoop = false;
                        returnView = View.Adventure;
                        break;
                    case 1:
                        Console.Write("\n\tLoading game. Ready yourself.");
                        currentSession.LoadSession();
                        menuLoop = false;
                        returnView = View.Adventure;
                        break;
                    case 2:
                        Console.Write("\n\tExiting game.");
                        menuLoop = false;
                        returnView = View.Exit;
                        break;
                }
                Thread.Sleep(1400);
                Console.Clear();

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
            }
            return returnView;
        }
    }
}
