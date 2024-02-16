using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Engine.Models;
using SalesAdventure3000_UI.Views;
using static SalesAdventure3000_UI.Views.ViewType;

namespace SalesAdventure3000
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = 42;
            int height = 15;

            View currentScreen = View.Start;
            
            Session currentSession = new Session();

            //Startscreen startscreen = new Startscreen();

            //startscreen.PrintAndGetInput(currentSession);

            while (true)
            {
                if (currentScreen == View.Start)
                    currentScreen = MenuView.Display(currentSession);
                if (currentScreen == View.Adventure)
                    break;
                    //currentScreen = AdventureView.Display(currentSession);
            }

            //currentSession.CreateNewWorld();

            DrawWorld(width, height, currentSession);

            DrawInfoWindow(width);
            DrawEquipment(width);
            DrawBackpack(width);

            static void DrawInfoWindow(int width)
            {
                //if (currentSession.frame > lastMessageFrame + 4)
                //{
                //    //Kör inte funktionen om spelaren tagit flera steg utan att något hänt
                //}
                List<string> gameMessages = new List<string>();
                string[] messages = { "You healed 5 hp", "You picked up {playerbackpack[pbp.Count-1].name}", "The dragon breathed fire at you for 15 damage" };
                gameMessages.AddRange(messages);

                Console.ResetColor();
                Console.WriteLine("╔".PadRight(width * 2 - 1, '═') + "╗");
                for (int i = 2; i > 0; i--)
                {
                    Console.Write($"║ {gameMessages[gameMessages.Count-i]}".PadRight(width*2 - 1, ' ') + "║\n");
                }
                Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            }

            static void DrawEquipment(int width) 
            {
                List<string> equippedItems = new List<string>();
                string[] items = { "Banana", "Chain Mail", "Dane Axe", "Sunglasses", "Trusty Boots" };
                equippedItems.AddRange(items);
                string frontSelect = ".";
                string backSelect = ".";
                string statBonus = "+5 Stat";

                //string frontSelect = equippedItems[i].Id == selectedItem ? " " : "[";
                //string backSelect = equippedItems[i].Id == selectedItem ? " " : "[";

                Console.ResetColor();
                Console.WriteLine("╔═EQUIPMENT═[E]".PadRight(width * 2 - 1, '═') + "╗");
                for (int i = 0; i < equippedItems.Count; i++)
                {
                    if (i % 2 == 0)
                        Console.Write($"║ {frontSelect}{equippedItems[i]}{backSelect} {statBonus}".PadRight(width - 1, ' '));
                    else
                        Console.Write($"{frontSelect}{equippedItems[i]}{backSelect} {statBonus}".PadRight(width, ' ') +"║\n");
                    if (i == equippedItems.Count-1)
                        Console.Write("".PadRight(width, ' ') + "║\n");
                }
                Console.WriteLine("╚".PadRight(width*2-1, '═') + "╝");
            }

            static void DrawBackpack(int width)
            {
                List<string> equippedItems = new List<string>();
                string[] items = { "Banana", "Chain Mail", "Dane Axe", "Sunglasses", "Trusty Boots" };
                equippedItems.AddRange(items);

                //string frontSelect = equippedItems[i].Id == selectedItem ? " " : "[";
                //string backSelect = equippedItems[i].Id == selectedItem ? " " : "[";

                string frontSelect = ".";
                string backSelect = ".";

                Console.ResetColor();
                Console.WriteLine("╔═Backpack═[B]".PadRight(width * 2 - 1, '═') + "╗");
                for (int i = 0; i < equippedItems.Count; i++)
                {
                    if (i % 2 == 0)
                        Console.Write($"║ {frontSelect}{equippedItems[i]}{backSelect}".PadRight(width - 1, ' '));
                    else
                        Console.Write($"{frontSelect}{equippedItems[i]}{backSelect}".PadRight(width, ' ') + "║\n");
                    if (i == equippedItems.Count - 1)
                        Console.Write("".PadRight(width, ' ') + "║\n");
                }
                Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            }
        }
        public static void DrawWorld(int width, int height, Session currentSession)
        {

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //DrawTile(currentSession.CurrentWorld.Map[y, x]) // Om vi lägger alla skriva ut-funktioner i separat klass.
                    currentSession.CurrentWorld.Map[y, x].DrawTile(currentSession.CurrentWorld.Entities);
                }
                DrawEdge();
            }

            void DrawEdge()
            {
                Console.ForegroundColor = Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(".");
            }
        }
    }
}