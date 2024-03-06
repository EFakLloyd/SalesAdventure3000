using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            GoToMenu,
            ContinueFight
        }
        private static int backpackIndex = 0;
        private static int equipmentIndex = 0;
        private static List<Equipment?> equipment = new List<Equipment>();

        //public static Actions Control(Session currentSession)
        //{
        //    Actions currentAction = Actions.StayOnMap;

        //    while (true)
        //    {
        //        if (currentAction == Actions.StayOnMap)
        //        {
        //            currentAction = (Actions)Display(currentSession);
        //        }
        //        if (currentAction == Actions.OpenEquipment)
        //            currentAction = (Actions)Display(currentSession);
        //        if (currentAction == Actions.OpenBackpack)
        //            currentAction = (Actions)Display(currentSession);
        //        if (currentAction == Actions.GoToMenu)
        //        {
        //            return currentAction;
        //        }
        //        return currentAction;
        //    }
        //}
        //public static View Display(Session currentSession)
        //{
        //    int width = 42;
        //    int height = 15;
        //    Actions currentAction = Actions.StayOnMap;

        //    while (true)
        //    {
        //        DrawPlayerStats();
        //        DrawWorld();
        //        DrawInfoWindow();
        //        DrawEquipment();
        //        DrawBackpack();

        //        if (currentAction == Actions.StayOnMap)
        //        {
        //            currentAction = MapControl.Control(currentSession);
        //        }
        //        else if (currentAction == Actions.OpenBackpack)
        //        {
        //            var input = PlayerInventoryControl.GetInput(backpackIndex, currentSession.CurrentPlayer.Backpack.Count);
        //            backpackIndex = input.selectedIndex;
        //            if (input.confirmedChoice == true)
        //                currentSession.UseItem(currentSession.CurrentPlayer.Backpack[backpackIndex]);
        //            currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;
        //        }
        //        else if (currentAction == Actions.OpenEquipment)
        //        {
        //            var input = PlayerInventoryControl.GetInput(equipmentIndex, currentSession.CurrentPlayer.EquippedItems.Count);
        //            equipmentIndex = input.selectedIndex;
        //            if (input.confirmedChoice == true)
        //                currentSession.UseItem(equipment[equipmentIndex]);
        //            currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;
        //        }
        //        else if (currentAction == Actions.GoToMenu)
        //        {
        //            return View.Exit;
        //        }
        //        Console.Clear();
        //    }

        //    void DrawPlayerStats()
        //    {
        //        Console.Write($"╔═{currentSession.CurrentPlayer.Name}".PadRight(width * 2 - 1, '═') + "╗\n" +
        //            $"║ Strength: {currentSession.CurrentPlayer.Strength}".PadRight(width - 1, ' ') + $"Vitality: {currentSession.CurrentPlayer.Vitality}".PadRight(width, ' ') + "║\n" +
        //            $"║ Coolness: {currentSession.CurrentPlayer.Coolness}".PadRight(width - 1, ' ') + $"Armour: {currentSession.CurrentPlayer.Armour}".PadRight(width, ' ') + "║\n" +
        //            "╚".PadRight(width * 2 - 1, '═') + "╝\n");
        //    }
        //    void DrawInfoWindow()
        //    {
        //        Console.WriteLine("╔═MESSAGES".PadRight(width * 2 - 1, '═') + "╗");

        //        for (int i = 3; i > 0; i--)
        //        {
        //            if (i <= currentSession.GameMessages.Count())
        //            {
        //                Console.Write($"║ ");
        //                Console.ForegroundColor = i == 1 ? ConsoleColor.Cyan : ConsoleColor.Gray;
        //                Console.Write($"{currentSession.GameMessages[currentSession.GameMessages.Count() - i]}".PadRight(width * 2 - 3, ' '));
        //                Console.ResetColor();
        //                Console.Write("║\n");
        //            }
        //            else
        //                Console.Write($"║".PadRight(width * 2 - 1) + "║\n");
        //        }
        //        Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
        //    }
        //    void DrawEquipment()
        //    {
        //        List<string> slots = new List<string>();
        //        foreach (KeyValuePair<Equipment.Slot, Equipment?> post in currentSession.CurrentPlayer.EquippedItems)
        //        {
        //            if (post.Value == null)
        //                slots.Add(post.Key + ":");
        //            else
        //                slots.Add(post.Key + $": {post.Value.GetName()} +{post.Value.Modifier} {post.Value.AffectedStat}");
        //            equipment.Add(post.Value);
        //        }

        //        Console.ForegroundColor = currentAction == Actions.OpenEquipment ? ConsoleColor.Cyan : ConsoleColor.Gray;
        //        Console.WriteLine("╔═EQUIPMENT═[E]".PadRight(width * 2 - 1, '═') + "╗");
        //        for (int i = 0; i < slots.Count; i++)
        //        {
        //            string[] selection = i == equipmentIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
        //            if (i % 2 == 0)
        //                Console.Write($"║ {selection[0]}{slots[i]}{selection[1]}".PadRight(width - 1, ' '));
        //            else
        //                Console.Write($"{selection[0]}{slots[i]}{selection[1]}".PadRight(width, ' ') + "║\n");
        //            if (i % 2 == 0 && i == slots.Count - 1)
        //                Console.Write("".PadRight(width, ' ') + "║\n");
        //        }
        //        Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
        //        Console.ResetColor();
        //    }
        //    void DrawBackpack()
        //    {
        //        Console.ForegroundColor = currentAction == Actions.OpenBackpack ? ConsoleColor.Cyan : ConsoleColor.Gray;
        //        Console.WriteLine("╔═BACKPACK═[B]".PadRight(width * 2 - 1, '═') + "╗");
        //        for (int i = 0; i < currentSession.CurrentPlayer.Backpack.Count; i++)
        //        {
        //            string[] selection = i == backpackIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
        //            if (i % 2 == 0)
        //                Console.Write($"║ {selection[0]}{currentSession.CurrentPlayer.Backpack[i].GetName()}{selection[1]}".PadRight(width - 1, ' '));
        //            else
        //                Console.Write($"{selection[0]}{currentSession.CurrentPlayer.Backpack[i].GetName()}{selection[1]}".PadRight(width, ' ') + "║\n");
        //            if (i % 2 == 0 && i == currentSession.CurrentPlayer.Backpack.Count - 1)
        //                Console.Write("".PadRight(width, ' ') + "║\n");
        //        }
        //        Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
        //        Console.ResetColor();
        //    }
        //    void DrawWorld()
        //    {
        //        for (int y = 0; y < height; y++)
        //        {
        //            for (int x = 0; x < width; x++)
        //            {
        //                currentSession.CurrentWorld.Map[y, x].DrawTile();
        //            }
        //            DrawEdge();
        //        }
        //        void DrawEdge()
        //        {
        //            Console.ForegroundColor = Console.BackgroundColor = ConsoleColor.Black;
        //            Console.WriteLine(".");
        //        }
        //        Console.ResetColor();
        //    }
        //}
    }
}
