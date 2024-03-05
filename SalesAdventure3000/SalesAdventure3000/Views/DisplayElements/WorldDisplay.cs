using Engine.Models;
using System;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    public static class WorldDisplay
    {
        public static void DrawWorld(Tile[,] map)
        {
            int height = 15;
            int width = 42;

            Console.SetCursorPosition(0, 4);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[y, x].DrawTile();
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

        public static void DrawTiles(Tile[,] map, int[] oldYX, int[] newYX)
        {
            Console.SetCursorPosition(oldYX[1]*2, oldYX[0]+4);
            map[oldYX[0], oldYX[1]].DrawTile();
            Console.SetCursorPosition(newYX[1]*2, newYX[0]+4);
            map[newYX[0], newYX[1]].DrawTile();
            Console.ResetColor();
        }
    }
}
