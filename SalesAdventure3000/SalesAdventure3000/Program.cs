using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Engine.Models;

namespace SalesAdventure3000
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = 42;
            int height = 15;

            Console.WriteLine("");

            Session currentSession = new Session();

            DrawWorld(width, height, currentSession);



            //World currentMap = new World();
            //currentMap.CreateWorld();
            //DrawWorld(width, height, currentMap);


            //Tile[,] map = new Tile[height, width];

            //map = CreateWorld(width, height);
            //DrawWorld(width, height, map);
            DrawInfoWindow(width);
            DrawEquipment(width);
            DrawBackpack(width);

            //static Tile[,] CreateWorld(int width, int height)
            //{
            //    string mapSketch = "XXXXXXXX~~~~~~~~~~~~~XXXXXXXXXXXXXX~~~~~~~\r\nXXXXXXXXX~~~~~~~~~..XXX.XXXXXXXXXXXXX~~~~~\r\nXXX..XXXXX~~~~~~~..XXX.........XXXXXXXX..~\r\nXX........~~~~~...XXX.............XXXXXX..\r\nXXX.........~~..XXX.............XXXX..XXX.\r\nX...........~..................XX.........\r\n..........~~.........................~....\r\n.........~........................X.~~~...\r\n......~~.........XXX.............XX..~~~..\r\n.....~~........XXX...~~~~~.......XX.......\r\n~~~.~~...........~~~~~~~~~~~~......XX.....\r\n~~~~~........~~~~~~~~~~~~~~~~~~....XXXX..X\r\n~~~~~.........~~~~~..~~~~~~~~~~......XXXXX\r\n~~~~~~~...............~~~~~~~~~~~......XXX\r\n~~~~~~~~~..~~~.........~~~~~~~~~~~~~~...XX".Replace("\r", "").Replace("\n", ""); ;
            //    //string mapSketch2 = "XXXXXXXX~~~~~~~~~~~~~XXXXX\r\nXXXXXXXXX~~~~~~~~~..XXX.XX\r\nXXX..XXXXX~~~~~~~..XXX...X\r\nXX........~~~~~...XXX.....\r\nXXX.........~~..XXX.......\r\nX.........................\r\n..........~~..............\r\n.........~...............~\r\n......~~.........XXX....~~\r\n.....~~........XXX...~~~~~\r\n~~~.~~...........~~~~~~~~~\r\n~~~~~........~~~~~~~~~~~~~\r\n~~~~~.........~~~~~..~~~~~\r\n~~~~~~~...............~~~~\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~".Replace("\r", "").Replace("\n", "");
            //    char[] mapArray = mapSketch.ToCharArray();

            //    Tile[,] map = new Tile[height,width];

            //    int i = 0;

            //    for (int y = 0; y < height; y++)
            //    {
            //        for(int x = 0; x < width; x++)
            //        {
            //            map[y, x] = new Tile(y, x, null, mapArray[i].ToString());
            //            i++;
            //        }
            //    }
            //    return map;
            //}




            //static void DrawWorld(int width, int height, Tile[,] map)
            //{

            //    for (int y = 0; y < height; y++)
            //    {
            //        for (int x = 0; x < width; x++)
            //        {
            //            map[y, x].DrawTile();
            //        }
            //        DrawEdge();
            //    }

            //    static void DrawEdge() 
            //    {
            //        Console.ForegroundColor = Console.BackgroundColor = ConsoleColor.Black;
            //        Console.WriteLine(".");
            //    }
            //}

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

            static void DrawEdge()
            {
                Console.ForegroundColor = Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(".");
            }
        }
    }
}