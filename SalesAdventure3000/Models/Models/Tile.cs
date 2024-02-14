using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Tile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int[] Coordinates { get; set; }
        public Color Color { get; set; }
        public string Texture { get; set; }
        public int? Occupant { get; set; }

        public Tile(int[] coords, int? occupant) 
        { 
            Coordinates = coords;
            Occupant = occupant;
            
            ID
        }
    }
}
