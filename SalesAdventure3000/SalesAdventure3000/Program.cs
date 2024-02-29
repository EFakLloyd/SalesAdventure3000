using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using SalesAdventure3000_UI.Views;
using static SalesAdventure3000_UI.Views.ViewType;

namespace SalesAdventure3000
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            View currentView = View.Start;
            
            Session currentSession = new Session();

            while (true)
            {
                if (currentView == View.Start)
                    currentView = MenuView.Display(currentSession);
                BattleView.Display(currentSession);
                AdventureView2.Display(currentSession);



                if (currentView == View.Adventure)                
                    currentView = AdventureView.Display(currentSession);
            }
            //Sluta här typ

            //static void DrawPlayerStats(int width, Session currentSession)
            //{
            //    Console.ResetColor();
            //    Console.WriteLine($"╔═{currentSession.CurrentPlayer.Name}".PadRight(width * 2 - 1, '═') + "╗");
            //    Console.WriteLine(
            //        $"║ Strength: {currentSession.CurrentPlayer.Strength}".PadRight(width - 1, ' ') + $"Vitality: {currentSession.CurrentPlayer.Vitality}".PadRight(width, ' ') + "║\n" +
            //        $"║ Coolness: {currentSession.CurrentPlayer.Coolness}".PadRight(width - 1, ' ') + $"Armour: {currentSession.CurrentPlayer.Armour}".PadRight(width, ' ') + "║\n" +
            //        "╚".PadRight(width * 2 - 1, '═') + "╝");
            //}

            //static void DrawInfoWindow(int width)
            //{
            //    //if (currentSession.frame > lastMessageFrame + 4)
            //    //{
            //    //    //Kör inte funktionen om spelaren tagit flera steg utan att något hänt
            //    //}
            //    List<string> gameMessages = new List<string>();
            //    string[] messages = { "You healed 5 hp", "You picked up {playerbackpack[pbp.Count-1].name}", "The dragon breathed fire at you for 15 damage" };
            //    gameMessages.AddRange(messages);

            //    Console.ResetColor();
            //    Console.WriteLine("╔".PadRight(width * 2 - 1, '═') + "╗");
            //    for (int i = 3; i > 0; i--)
            //    {
            //        Console.Write($"║ {gameMessages[gameMessages.Count-i]}".PadRight(width*2 - 1, ' ') + "║\n");
            //    }
            //    Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            //}

            //static void DrawEquipment(int width) 
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
            //            Console.Write($"{frontSelect}{equippedItems[i]}{backSelect} {statBonus}".PadRight(width, ' ') +"║\n");
            //        if (i == equippedItems.Count-1)
            //            Console.Write("".PadRight(width, ' ') + "║\n");
            //    }
            //    Console.WriteLine("╚".PadRight(width*2-1, '═') + "╝");
            //}

            //    static void DrawBackpack(int width)
            //    {
            //        List<string> equippedItems = new List<string>();
            //        string[] items = { "Banana", "Chain Mail", "Dane Axe", "Sunglasses", "Trusty Boots" };
            //        equippedItems.AddRange(items);

            //        //string frontSelect = equippedItems[i].Id == selectedItem ? " " : "[";
            //        //string backSelect = equippedItems[i].Id == selectedItem ? " " : "[";

            //        string frontSelect = ".";
            //        string backSelect = ".";

            //        Console.ResetColor();
            //        Console.WriteLine("╔═Backpack═[B]".PadRight(width * 2 - 1, '═') + "╗");
            //        for (int i = 0; i < equippedItems.Count; i++)
            //        {
            //            if (i % 2 == 0)
            //                Console.Write($"║ {frontSelect}{equippedItems[i]}{backSelect}".PadRight(width - 1, ' '));
            //            else
            //                Console.Write($"{frontSelect}{equippedItems[i]}{backSelect}".PadRight(width, ' ') + "║\n");
            //            if (i == equippedItems.Count - 1)
            //                Console.Write("".PadRight(width, ' ') + "║\n");
            //        }
            //        Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            //    }
            //}
            //public static void DrawWorld(int width, int height, Session currentSession)
            //{

            //    for (int y = 0; y < height; y++)
            //    {
            //        for (int x = 0; x < width; x++)
            //        {
            //            //DrawTile(currentSession.CurrentWorld.Map[y, x]) // Om vi lägger alla skriva ut-funktioner i separat klass.
            //            currentSession.CurrentWorld.Map[y, x].DrawTile(currentSession.CurrentWorld.Entities);
            //        }
            //        DrawEdge();
            //    }

            //void DrawEdge()
            //{
            //    Console.ForegroundColor = Console.BackgroundColor = ConsoleColor.Black;
            //    Console.WriteLine(".");
            //}
        }
    }
}