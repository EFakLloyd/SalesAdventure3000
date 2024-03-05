using Engine.Models;
using System;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    public static class StatsWindow
    {
        public static void DrawPlayerStats(Player player)
        {
            int width = 42;
            Console.SetCursorPosition(0, 0);

            Console.Write($"╔═{player.Name}".PadRight(width * 2 - 1, '═') + "╗\n" +
                $"║ Strength: {player.Strength}".PadRight(width - 1, ' ') + $"Vitality: {player.Vitality}".PadRight(width, ' ') + "║\n" +
                $"║ Coolness: {player.Coolness}".PadRight(width - 1, ' ') + $"Armour: {player.Armour}".PadRight(width, ' ') + "║\n" +
                "╚".PadRight(width * 2 - 1, '═') + "╝\n");
        }
    }
}
