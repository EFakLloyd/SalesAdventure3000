﻿using Engine.Models;
using System;
using System.Collections.Generic;
using static SalesAdventure3000_UI.Views.AdventureView;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class EquipmentWindow
    {
        public static void Draw(ViewEnums.Actions currentAction, Dictionary<Equipment.Slot, Equipment?> playerEquipment, int equipmentIndex)
        {
            List<Equipment?> equipment = new List<Equipment>(); //List for navigation by index.
            List<string> slots = new List<string>();            //List for display in equipment window.

            foreach (KeyValuePair<Equipment.Slot, Equipment?> post in playerEquipment)
            {
                if (post.Value == null)
                    slots.Add(post.Key + ":");
                else
                    slots.Add(post.Key + $": {post.Value.GetName()} +{post.Value.Modifier} {post.Value.AffectedStat}");
                equipment.Add(post.Value);
            }

            Console.SetCursorPosition(0, 24);
            Console.ForegroundColor = currentAction == ViewEnums.Actions.OpenEquipment ? ConsoleColor.Cyan : ConsoleColor.Gray;
            Console.WriteLine("╔═EQUIPMENT═[E]".PadRight(GameDimensions.Width * 2 - 1, '═') + "╗");
            for (int i = 0; i < slots.Count; i++)
            {
                string[] selection = i == equipmentIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
                if (i % 2 == 0)
                    Console.Write($"║ {selection[0]}{slots[i]}{selection[1]}".PadRight(GameDimensions.Width - 1, ' '));
                else
                    Console.Write($"{selection[0]}{slots[i]}{selection[1]}".PadRight(GameDimensions.Width, ' ') + "║\n");
                if (i % 2 == 0 && i == slots.Count - 1)
                    Console.Write("".PadRight(GameDimensions.Width, ' ') + "║\n");
            }
            Console.WriteLine("╚".PadRight(GameDimensions.Width * 2 - 1, '═') + "╝");
            Console.ResetColor();
        }
    }
}
