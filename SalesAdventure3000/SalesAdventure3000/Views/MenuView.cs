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
            List<string> commands = new List<string>(new string[] { "New game", "Load game", "Save game", "Exit" });
            if (currentSession.CurrentPlayer != null)
                commands.Insert(0, "Continue");
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
                    for (int i = 0; i < commands.Count; i++)
                    {
                        string[] selection = i == SelectedCommand ? new string[] { "[", "]" } : new string[] { " ", " " };
                        Console.Write($"\t{selection[0]}{commands[i]}{selection[1]}\n\n");
                    }

                    var input = MenuControl.GetInput(SelectedCommand, commands.Count);
                    if (input.enter)
                        break;
                    SelectedCommand = input.val + (currentSession.CurrentPlayer != null ? -1 : 0);
                }
                Console.Clear();
                switch (SelectedCommand)
                {
                    case -1:
                        menuLoop = false;
                        returnView = View.Adventure;
                        break;
                    case 0:
                        Console.Write("\n\tEnter your name, brave adventurer: ");
                        string adventurerName = Console.ReadLine();
                        Console.WriteLine($"\n\tReady yourself, brave {adventurerName}!");
                        currentSession.CreateNewWorld();
                        currentSession.CreatePlayer(adventurerName);              
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
                        Console.Write("\n\tGame Saved.");
                        currentSession.SaveSession();
                        returnView = View.Start;
                        menuLoop = true;
                        break;
                    case 3:
                        Console.Write("\n\tExiting game.");
                        menuLoop = false;
                        returnView = View.Exit;
                        break;
                }
                Thread.Sleep(1400);
                Console.Clear();
            }
            return returnView;
        }
    }
}
