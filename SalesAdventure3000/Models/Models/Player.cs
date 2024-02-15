using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : Creature
    {
        public Player(string Name, char Character, ConsoleColor FGColor) : base(Name, Character, FGColor)
        {

        }
        //public Player(int Id, string Name, int[] Coordinates, char Character, ConsoleColor bGColor, List<Item> backpack) :
        //    base(Id, Name, Coordinates, Character, bGColor, backpack)
        //{
        //}
    }
}
