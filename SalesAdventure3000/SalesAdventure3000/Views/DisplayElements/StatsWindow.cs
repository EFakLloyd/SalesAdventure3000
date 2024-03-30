using Engine.Models;
using System;
using System.Collections.Generic;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal static class StatsWindow
    {
        public static void Draw(Dictionary<Entity.Stat, string> playerStats)
        {
            Console.SetCursorPosition(0, 0);

            Console.Write($"╔═{playerStats[Entity.Stat.Name]}".PadRight(GameDimensions.Width * 2 - 1, '═') + "╗\n" +
                $"║ Strength: {playerStats[Entity.Stat.Strength]}".PadRight(GameDimensions.Width - 1, ' ') + $"Vitality: {playerStats[Entity.Stat.Vitality]}/{playerStats[Entity.Stat.MaxVitality]}".PadRight(GameDimensions.Width, ' ') + "║\n" +
                $"║ Coolness: {playerStats[Entity.Stat.Coolness]}".PadRight(GameDimensions.Width - 1, ' ') + $"Armour: {playerStats[Entity.Stat.Armour]}".PadRight(GameDimensions.Width, ' ') + "║\n" +
                "╚".PadRight(GameDimensions.Width * 2 - 1, '═') + "╝\n");
        }
    }
}
