using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : Creature
    {
        public int[,] Coordinate { get; set; }
        public Player(string Name, char Character, ConsoleColor FGColor, int[,] Coordinate) : base(Name, Character, FGColor)
        {
            this.Character = '@';
            this.FGColor = ConsoleColor.DarkMagenta;
            this.Coordinate = Coordinate;
        }
        //public Player(int Id, string Name, int[] Coordinates, char Character, ConsoleColor bGColor, List<Item> backpack) :
        //    base(Id, Name, Coordinates, Character, bGColor, backpack)
        //{
        //}

    }
}
