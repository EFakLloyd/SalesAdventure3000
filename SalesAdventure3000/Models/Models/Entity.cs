using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public abstract class Entity
    {
        public string Name { get; set; }
        public string Appearance { get; set; }  //The "texture" of the entity when drawn on the world map.
        public ConsoleColor FGColor { get; set; }   //Assiged color.
        public Entity(string name, string appearance, ConsoleColor fGColor)
        {
            Name = name;
            Appearance = appearance;
            FGColor = fGColor;    
        }   
    }
}
