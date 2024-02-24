using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class EquipmentFactory
    {
        public static Equipment CreateEquipment(int id)
        {
            switch (id)
            {
                case 2000:
                    Equipment hornedHelmet = new Equipment("Horned Helmet", "V", ConsoleColor.White, Equipment.EqType.Head, Item.Stat.Armour, 5, "The helmet frames your face menacingly. It provides ");
                    return hornedHelmet;
                case 2001:
                    Equipment chainMail = new Equipment("Chain Mail", "%", ConsoleColor.White, Equipment.EqType.Torso, Item.Stat.Armour, 7, "The mail makes some noise when you move about. It provides ");
                    return chainMail;
                case 2002:
                    Equipment greatAxe = new Equipment("Great Axe", "P", ConsoleColor.White, Equipment.EqType.Weapon, Item.Stat.Strength, 10, "A monstrous axe! It grants you ");
                    return greatAxe;
                case 2003:
                    Equipment shortSword = new Equipment("Short Sword", "I", ConsoleColor.White, Equipment.EqType.Weapon, Item.Stat.Armour, 5, "A toothpick of a sword, really. It gives you ");
                    return shortSword;
                case 2004:
                    Equipment sunglasses = new Equipment("Sunglasses", "8", ConsoleColor.White, Equipment.EqType.Bling, Item.Stat.Coolness, 12, "Superstylish glasses! They give you ");
                    return sunglasses;
                case 2005:
                    Equipment powerMedallion = new Equipment("Power Medallion", "Q", ConsoleColor.White, Equipment.EqType.Bling, Item.Stat.Strength, 6, "The medallion boost your martial provice. It gives you ");
                    return powerMedallion;
                case 2006:
                    Equipment backwardsCap = new Equipment("Backwards Cap", "d", ConsoleColor.White, Equipment.EqType.Head, Item.Stat.Coolness, 6, "Sick cap dawg... It provides ");
                    return backwardsCap;
                default:
                    return null;
            }
        }
    }
}
