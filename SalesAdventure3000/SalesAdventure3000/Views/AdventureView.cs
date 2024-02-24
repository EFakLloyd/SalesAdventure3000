using Engine.Models;
using System;
using System.Collections;
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
            while (true)
            {
                DrawPlayerStats();
                DrawWorld();
                DrawInfoWindow();
                DrawEquipment(true);
                DrawBackpack(false);
                // stannar koden här just nu /Jens 22/2
                Console.ReadLine();
                return View.Start;
            }

            void DrawPlayerStats()
            {
                Console.Write($"╔═{currentSession.CurrentPlayer.Name}".PadRight(width * 2 - 1, '═') + "╗\n" +
                    $"║ Strength: {currentSession.CurrentPlayer.Strength}".PadRight(width - 1, ' ') + $"Vitality: {currentSession.CurrentPlayer.Vitality}".PadRight(width, ' ') + "║\n" +
                    $"║ Coolness: {currentSession.CurrentPlayer.Coolness}".PadRight(width - 1, ' ') + $"Armour: {currentSession.CurrentPlayer.Armour}".PadRight(width, ' ') + "║\n" +
                    "╚".PadRight(width * 2 - 1, '═') + "╝\n");
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

                Console.WriteLine("╔".PadRight(width * 2 - 1, '═') + "╗");
                for (int i = 3; i > 0; i--)
                {
                    Console.Write($"║ {gameMessages[gameMessages.Count - i]}".PadRight(width * 2 - 1, ' ') + "║\n");
                }
                Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            }
            void DrawEquipment(bool active)
            {

                List<string> slots = new List<string>();
                List<Equipment?> equipment = new List<Equipment?>();
                foreach (KeyValuePair<Equipment.EqType, Equipment?> post in currentSession.CurrentPlayer.EquippedItems)
                {
                    if (post.Value == null)
                        slots.Add(post.Key + ":");
                    else
                        slots.Add(post.Key + $": {post.Value.GetName()} +{post.Value.Modifier} {post.Value.AffectedStat}");
                    equipment.Add(post.Value);
                }

                int selectedCommand = 0;

                Console.ForegroundColor = active ? ConsoleColor.Cyan : ConsoleColor.Gray;
                Console.WriteLine("╔═EQUIPMENT═[E]".PadRight(width * 2 - 1, '═') + "╗");
                for (int i = 0; i < slots.Count; i++)
                {
                    string[] selection = i == selectedCommand ? new string[] { "[", "]" } : new string[] { " ", " " };
                    if (i % 2 == 0)
                        Console.Write($"║ {selection[0]}{slots[i]}{selection[1]}".PadRight(width - 1, ' '));
                    else
                        Console.Write($"{selection[0]}{slots[i]}{selection[1]}".PadRight(width, ' ') + "║\n");
                    if (i % 2 == 0 && i == slots.Count - 1)
                        Console.Write("".PadRight(width, ' ') + "║\n");
                }
                Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
                Console.ResetColor();
            }
            //void DrawEquipment2()
            //{
            //    List<string> equippedItems = new List<string>();
            //    string[] items = { "Banana", "Chain Mail", "Dane Axe", "Sunglasses", "Trusty Boots" };
            //    equippedItems.AddRange(items);
            //    string frontSelect = ".";
            //    string backSelect = ".";
            //    string statBonus = "+5 Stat";

            //    //string frontSelect = equippedItems[i].Id == selectedItem ? " " : "[";
            //    //string backSelect = equippedItems[i].Id == selectedItem ? " " : "[";

            //    Console.ResetColor();
            //    Console.WriteLine("╔═EQUIPMENT═[E]".PadRight(width * 2 - 1, '═') + "╗");
            //    for (int i = 0; i < equippedItems.Count; i++)
            //    {
            //        if (i % 2 == 0)
            //            Console.Write($"║ {frontSelect}{equippedItems[i]}{backSelect} {statBonus}".PadRight(width - 1, ' '));
            //        else
            //            Console.Write($"{frontSelect}{equippedItems[i]}{backSelect} {statBonus}".PadRight(width, ' ') + "║\n");
            //        if (i == equippedItems.Count - 1)
            //            Console.Write("".PadRight(width, ' ') + "║\n");
            //    }
            //    Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            //}
            void DrawBackpack(bool active)
            {
                int selectedCommand = 0;

                Console.ForegroundColor = active ? ConsoleColor.Cyan : ConsoleColor.Gray;
                Console.WriteLine("╔═BACKPACK═[B]".PadRight(width * 2 - 1, '═') + "╗");
                for (int i = 0; i < currentSession.CurrentPlayer.Backpack.Count; i++)
                {
                    string[] selection = i == selectedCommand ? new string[] { "[", "]" } : new string[] { " ", " " };
                    if (i % 2 == 0)
                        Console.Write($"║ {selection[0]}{currentSession.CurrentPlayer.Backpack[i].GetName()}{selection[1]}".PadRight(width - 1, ' '));
                    else
                        Console.Write($"{selection[0]}{currentSession.CurrentPlayer.Backpack[i].GetName()}{selection[1]}".PadRight(width, ' ') + "║\n");
                    if (i % 2 == 0 && i == currentSession.CurrentPlayer.Backpack.Count - 1)
                        Console.Write("".PadRight(width, ' ') + "║\n");
                }
                Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
                Console.ResetColor();
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
                Console.ResetColor();
            }
        }
    }
}
