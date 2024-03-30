using Engine.Models;

namespace Engine
{
    internal static class EquipmentFactory
    {
        public static Equipment CreateEquipment(int id)
        {
            switch (id) //Select among prepared entities. May be done at random or via feeding a specific id.
            {
                case 2000:
                    Equipment hornedHelmet = new Equipment("Horned Helmet", "V", ConsoleColor.White, Equipment.Slot.Head, Item.Stat.Armour, 5, "The helmet frames your face menacingly. It provides ", 2000);
                    return hornedHelmet;
                case 2001:
                    Equipment chainMail = new Equipment("Chain Mail", "%", ConsoleColor.White, Equipment.Slot.Torso, Item.Stat.Armour, 7, "The mail makes some noise when you move about. It provides ", 2001);
                    return chainMail;
                case 2002:
                    Equipment greatAxe = new Equipment("Great Axe", "P", ConsoleColor.White, Equipment.Slot.Weapon, Item.Stat.Strength, 10, "A monstrous axe! It grants you ", 2002);
                    return greatAxe;
                case 2003:
                    Equipment shortSword = new Equipment("Short Sword", "I", ConsoleColor.White, Equipment.Slot.Weapon, Item.Stat.Armour, 5, "A toothpick of a sword, really. It gives you ", 2003);
                    return shortSword;
                case 2004:
                    Equipment sunglasses = new Equipment("Sunglasses", "8", ConsoleColor.White, Equipment.Slot.Bling, Item.Stat.Coolness, 12, "Superstylish glasses! They give you ", 2004);
                    return sunglasses;
                case 2005:
                    Equipment powerMedallion = new Equipment("Power Medallion", "Q", ConsoleColor.White, Equipment.Slot.Bling, Item.Stat.Strength, 6, "The medallion boosts your martial provess. It gives you ", 2005);
                    return powerMedallion;
                case 2006:
                    Equipment backwardsCap = new Equipment("Backwards Cap", "d", ConsoleColor.White, Equipment.Slot.Head, Item.Stat.Coolness, 6, "Sick cap dawg... It provides ", 2006);
                    return backwardsCap;
                default:            //In case id is incorrect.
                    return null;
            }
        }
    }
}
