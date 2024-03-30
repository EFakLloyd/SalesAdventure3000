using Engine.Models;
using System;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class WorldDisplay
    {
        public static void DrawWorld(Tile[,] map)   //Draws the whole map.
        {
            Console.SetCursorPosition(0, 4);
            for (int y = 0; y < GameDimensions.Height; y++)
            {
                for (int x = 0; x < GameDimensions.Width; x++)
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
        public static void DrawTiles(Tile[,] map, Position oldYX, Position newYX)   //Updates only the latest affected tiles.
        {
            Console.SetCursorPosition(oldYX.X * 2, oldYX.Y + 4);
            map[oldYX.Y, oldYX.X].DrawTile();
            Console.SetCursorPosition(newYX.X * 2, newYX.Y + 4);
            map[newYX.Y, newYX.X].DrawTile();
            Console.ResetColor();
        }
    }
}
