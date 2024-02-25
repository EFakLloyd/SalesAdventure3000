using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Engine
{
    public static class MonsterFactory
    {
        public static Monster CreateMonster(int id)
        {
            switch (id) //Select among prepared entities. May be done at random or via feeding a specific id.
            {
                case 1000:
                    Monster snake = new Monster("Snake", "Sn", ConsoleColor.Cyan, 5, 5, 15, "The snake slithers up your leg and bites your chin for ");
                    return snake;
                case 1001:
                    Monster orc = new Monster("Orc", "Oc", ConsoleColor.Black, 10, 10, 5, "Recklessly slashing its scimitar, the orc deals you ");
                    return orc;
                case 1002:
                    Monster tintin = new Monster("Tintin-beast", "Tt", ConsoleColor.DarkBlue, 30, 20, 99, "Tintin makes you feel insufficent with his Linux skills. You take ");
                    return tintin;
                case 1003:
                    Monster dragon = new Monster("Dragon", "Dn", ConsoleColor.DarkRed, 35, 35, 20, "The dragon breathed fire at you for ");
                    return dragon;
                default:            //In case id is incorrect.
                    return null;
            }
        }
    }
}
