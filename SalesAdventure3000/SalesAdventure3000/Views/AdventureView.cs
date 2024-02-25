using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static SalesAdventure3000_UI.Views.AdventureView;
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
        public enum State
        {
            Active,
            Inactive
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
            State backpackState = State.Inactive;
            State equipmentState = State.Inactive;

            while (true)
            {                    
                DrawPlayerStats();
                DrawWorld();
                DrawInfoWindow();
                DrawEquipment(equipmentState);
                DrawBackpack(backpackState);

                if (currentAction == Actions.StayOnMap)
                {
                    equipmentState = backpackState = State.Inactive;
                    currentAction = MapControl.Control(currentSession);
                    backpackState = currentAction == Actions.OpenBackpack ? State.Active : State.Inactive;
                    equipmentState = currentAction == Actions.OpenEquipment ? State.Active : State.Inactive;
                }             
                else if (currentAction == Actions.OpenBackpack) 
                {
                    PlayerInventoryControl.GetInput(1, currentSession.CurrentPlayer.Backpack.Count);
                }
                else if (currentAction == Actions.OpenEquipment)
                {
                    InventoryControl.GetInput(1,currentSession.CurrentPlayer.EquippedItems.Count);
                }
                else if (currentAction == Actions.GoToMenu)
                {
                    return View.Exit;
                }
                Console.Clear();
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
                Console.WriteLine("╔".PadRight(width * 2 - 1, '═') + "╗");

                for (int i = 3; i > 0; i--)
                {
                    if (i <= currentSession.GameMessages.Count())
                    {
                        Console.Write($"║ ");
                        Console.ForegroundColor = i == 1 ? ConsoleColor.Cyan : ConsoleColor.Gray;
                        Console.Write($"{currentSession.GameMessages[currentSession.GameMessages.Count() - i]}".PadRight(width * 2 - 3, ' '));
                        Console.ResetColor();
                        Console.Write("║\n");
                    }
                }
                Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            }
            void DrawEquipment(State state)
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

                Console.ForegroundColor = state == State.Active ? ConsoleColor.Cyan : ConsoleColor.Gray;
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
            void DrawBackpack(State state)
            {
                int selectedCommand = 0;

                Console.ForegroundColor = state == State.Active ? ConsoleColor.Cyan : ConsoleColor.Gray;
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
                        currentSession.CurrentWorld.Map[y, x].DrawTile();
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
