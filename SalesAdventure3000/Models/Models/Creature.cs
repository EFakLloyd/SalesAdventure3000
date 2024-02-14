using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAdventure3000
{
    

    
    public class Creature : Entity
    {
        protected int Strenght { get; set; }
        protected int Vitality { get; set; }
        protected int Patience {  get; set; }
        protected int Charisma {  get; set; }
        protected int Speed { get; set; }
        protected int Wisdom { get; set; }
        protected int Luck { get; set; }


        public Creature(int Id, string Name, int[] Coordinates, char Character, ConsoleColor bGColor, List<Item> backpack) : base(Id, Name, Character, bGColor,backpack)
        {
            this.Strenght = 9;
            this.Vitality = 7;
            this.Patience = 2;
            this.Charisma = 1;
            this.Speed = 8;
            this.Wisdom = 4;
            this.Luck = 1;
        }

      


    }
}
