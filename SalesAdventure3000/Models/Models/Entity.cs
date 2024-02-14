using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Entity
    {
        public readonly int ID;
        protected string Name { get; set; }
        public int[] Coordinates { get; set; }
        protected char Character { get; set; }

        public Entity(int ID,string Name, char Character)
        {
            this.ID = ID;
            this.Name = Name;
            this.Coordinates = getCoordinates();
            this.Character = Character;
        }

        private int[]? getCoordinates()
        {
            throw new NotImplementedException();
        }
    }
   
}
