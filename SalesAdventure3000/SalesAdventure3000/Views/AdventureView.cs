using Engine.Models;
using SalesAdventure3000_UI.Controllers;
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
        public enum Actions
        {
            StayOnMap,
            OpenEquipment,
            OpenBackpack,
            GoToMenu
        }
        public static Actions Control(Session currentSession)
        {
            Actions currentAction = Actions.StayOnMap;

            while (true)
            {
                if (currentAction == Actions.StayOnMap)
                {
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

            Actions currentAction = Actions.StayOnMap;

            while (true)
            {
                Console.Clear();
                DrawPlayerStats();
                DrawWorld();
                DrawInfoWindow();
                DrawEquipment();
                DrawBackpack();

                if (currentAction == Actions.StayOnMap)
                {
                    MapControl.Control(currentSession);
                }             
                                 
                if (currentAction == Actions.OpenBackpack) { 
                    PlayerInventoryControl.GetInput(1, currentSession.CurrentPlayer.Backpack.Count);
                }
                if (currentAction == Actions.OpenEquipment)
                {
                    InventoryControl.GetInput(1,currentSession.CurrentPlayer.EquippedItems.Count);
                }
                if (currentAction == Actions.GoToMenu)
                {
                    return View.Exit;
                }
                   

                
                
            }

            void DrawPlayerStats()
            {
                Console.ResetColor();
                Console.WriteLine($"╔═{currentSession.CurrentPlayer.Name}".PadRight(width * 2 - 1, '═') + "╗");
                Console.WriteLine(
                    $"║ Strength: {currentSession.CurrentPlayer.Strength}".PadRight(width - 1, ' ') + $"Vitality: {currentSession.CurrentPlayer.Vitality}".PadRight(width, ' ') + "║\n" +
                    $"║ Coolness: {currentSession.CurrentPlayer.Coolness}".PadRight(width - 1, ' ') + $"Armour: {currentSession.CurrentPlayer.Armour}".PadRight(width, ' ') + "║\n" +
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
}
