using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : Creature
    {
        public int[] Coordinate { get; set; }
        public Player(string name, int[] coordinate) : base(name, "@", ConsoleColor.DarkMagenta, 15, 15, 5)
        {
            this.Name = name;
            //this.Appearance = "@";
            //this.FGColor = ConsoleColor.DarkMagenta;
            //this.Strength = 15;
            //this.Vitality = 15;
            //this.Coolness = 5;
            this.Coordinate = coordinate;
            //this.Coordinate = new int[] {Coordinate[0, 0], Coordinate[0, 1]};
        }
        //public Player(int Id, string Name, int[] Coordinates, char Character, ConsoleColor bGColor, List<Item> backpack) :
        //    base(Id, Name, Coordinates, Character, bGColor, backpack)
        //{
        //}

    }
}
