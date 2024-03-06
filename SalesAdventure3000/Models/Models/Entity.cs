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
        public enum Stat    //Used by items and creatures
        {
            Name,
            Strength,
            Vitality,
            MaxVitality,
            Armour,
            Coolness
        }
        public enum Adjustment
        {
            Up,
            Down
        }

        public string Name { get; }
        protected string Appearance;  //The "texture" of the entity when drawn on the world map.
        protected ConsoleColor FGColor;  //Assiged color.
        private int id;
        public int[]? Coordinates { get; private set; }

        public Entity() { }
        public Entity(string name, string appearance, ConsoleColor fGColor, int id)
        {
            this.Name = name;
            this.Appearance = appearance;
            this.FGColor = fGColor;
            this.Coordinates = new int[2];
            this.id = id;
        }
        public string TypeOf()
        {
            return this.GetType().ToString();
        }
        public (ConsoleColor fgColor, string appearance) GetVisuals()
        {
            return (FGColor, Appearance);
        }
        public void SetCoordinates(int[] coordinates)
        {
            Coordinates = coordinates;
        }
    }
}
