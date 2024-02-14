using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    

    
    public class Creature : Entity
    {
        protected int Strenght { get; set; }
        protected int Vitality { get; set; }
        protected int Patience {  get; set; }
        protected int Charisma {  get; set; }
        protected int Speed;
        protected int Wisdom;
        protected int Luck;

        public Creature(int ID, string Name, int[] Coordinates, char Character) : base(ID, Name, Character)
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
