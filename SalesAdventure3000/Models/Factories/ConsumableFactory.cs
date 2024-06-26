﻿using Engine.Models;

namespace Engine
{
    internal static class ConsumableFactory
    {
        public static Consumable CreateConsumable(int id)
        {
            switch (id) //Select among prepared entities. May be done at random or via feeding a specific id.
            {
                case 3000:
                    Consumable bread = new Consumable("Bread", "B", ConsoleColor.Yellow, Item.Stat.Vitality, 3, null, 3, "You munch down on the bread. It gives you ", 3000);
                    return bread;
                case 3001:
                    Consumable healthPotion = new Consumable("Health Potion", "H", ConsoleColor.Yellow, Item.Stat.Vitality, 10, null, 1, "The health potion restores you for ", 3001);
                    return healthPotion;
                case 3002:
                    Consumable coolPotion = new Consumable("Potion of Cool", "C", ConsoleColor.Yellow, Item.Stat.Coolness, 15, 10, 1, "Such style! The potion temporarily grants you ", 3002);
                    return coolPotion;
                case 3003:
                    Consumable rageShroom = new Consumable("Rage Shroom", "R", ConsoleColor.Yellow, Item.Stat.Strength, 10, 10, 1, "You get angry! The potion temporarily grants you ", 3003);
                    return rageShroom;
                default:            //In case id is incorrect.
                    return null;
            }
        }
    }
}
