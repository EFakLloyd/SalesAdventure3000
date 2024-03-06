using Engine.Models;
using System;
using System.Collections.Generic;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    public static class StatsWindow
    {
        public static void Draw(Dictionary<Entity.Stat, string> playerStats)
        {
            int width = 42;
            Console.SetCursorPosition(0, 0);

            Console.Write($"╔═{playerStats[Entity.Stat.Name]}".PadRight(width * 2 - 1, '═') + "╗\n" +
                $"║ Strength: {playerStats[Entity.Stat.Strength]}".PadRight(width - 1, ' ') + $"Vitality: {playerStats[Entity.Stat.Vitality]}/{playerStats[Entity.Stat.MaxVitality]}".PadRight(width, ' ') + "║\n" +
                $"║ Coolness: {playerStats[Entity.Stat.Coolness]}".PadRight(width - 1, ' ') + $"Armour: {playerStats[Entity.Stat.Armour]}".PadRight(width, ' ') + "║\n" +
                "╚".PadRight(width * 2 - 1, '═') + "╝\n");
        }
    }
}
