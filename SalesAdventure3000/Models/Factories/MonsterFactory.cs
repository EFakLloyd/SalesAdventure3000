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
                    Monster snake = new Monster("Snake", "Sn", ConsoleColor.Cyan, 5, 5, 0, 15, "The snake slithers up your leg and bites your chin for ", 6, 1000);
                    return snake;
                case 1001:
                    Monster orc = new Monster("Orc", "Oc", ConsoleColor.Black, 10, 10, 2, 5, "Recklessly slashing its scimitar, the orc deals you ", 5, 1001);
                    return orc;
                case 1002:
                    Monster tintin = new Monster("Tintin-beast", "Tt", ConsoleColor.DarkBlue, 30, 20, 4, 99, "Tintin makes you feel insufficent with his Linux skills. You take ", 7, 1002);
                    return tintin;
                case 1003:
                    Monster dragon = new Monster("Dragon", "Dn", ConsoleColor.DarkRed, 35, 35, 6, 20, "The dragon breathed fire at you for ", 4, 1003);
                    return dragon;
                default:            //In case id is incorrect.
                    return null;
            }
        }
    }
}
