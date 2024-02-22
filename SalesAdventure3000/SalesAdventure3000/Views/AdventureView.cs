using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static SalesAdventure3000_UI.Views.ViewType;

namespace SalesAdventure3000_UI.Views
{
    public class AdventureView
    {
        
        public static Actions Control(Session currentSession) {

            Actions currentAction = Actions.StayOnMap;

            while (true)
            {
                if (currentAction == Actions.StayOnMap) { 
                    currentAction = (Actions)Display(currentSession);
                    
                }
                if (currentAction == Actions.OpenEquipment)
                currentAction = (Actions)Display(currentSession);
                if (currentAction == Actions.OpenBackpack)
                    currentAction = (Actions)Display(currentSession);
                if (currentAction == Actions.GoToMenu)
                {
                return currentAction;
            }
                    

                return currentAction;
        }


    }


        public static View Display(Session currentSession)
        {
            int width = 42;
            int height = 15;
            while (true)
            {
                DrawPlayerStats();
                DrawWorld();
                DrawInfoWindow();
                DrawEquipment();
                DrawBackpack();

            }

             void DrawPlayerStats()
            {
                Console.ResetColor();
                Console.WriteLine("╔".PadRight(width * 2 - 1, '═') + "╗");
                Console.WriteLine(
                    $"║ Name: {currentSession.CurrentPlayer.Name}".PadRight(width - 1, ' ') + $"Coolness: {currentSession.CurrentPlayer.Coolness.ToString()}".PadRight(width, ' ') + "║\n" +
                    $"║ Strength: {currentSession.CurrentPlayer.Strength}".PadRight(width - 1, ' ') + $"Vitality: {currentSession.CurrentPlayer.Vitality.ToString()}".PadRight(width, ' ') + "║\n" +
                    "╚".PadRight(width * 2 - 1, '═') + "╝");
            }

            void DrawInfoWindow()
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
                for (int i = 3; i > 0; i--)
                {
                    Console.Write($"║ {gameMessages[gameMessages.Count - i]}".PadRight(width * 2 - 1, ' ') + "║\n");
                }
                Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            }

             void DrawEquipment()
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
                        Console.Write($"{frontSelect}{equippedItems[i]}{backSelect} {statBonus}".PadRight(width, ' ') + "║\n");
                    if (i == equippedItems.Count - 1)
                        Console.Write("".PadRight(width, ' ') + "║\n");
                }
                Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            }

             void DrawBackpack()
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
        

         void DrawWorld()
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
                // Skriva själva metoderna här(inte kalla på dessa här)
                //DrawPlayerStats(width, currentSession);

                //DrawWorld(width, height, currentSession);
                //DrawEquipment(width);
                //DrawBackpack(width);
                //return View.Start;

            }
            return View.Adventure;
        
        }
    

    public enum Actions
        {
            StayOnMap,
            OpenEquipment,
            OpenBackpack,
            GoToMenu
        }
    }
}
