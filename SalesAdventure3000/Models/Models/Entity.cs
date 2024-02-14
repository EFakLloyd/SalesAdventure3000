using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAdventure3000
{
    public class Entity
    {
        public int Id { get; set; }
        protected string Name { get; set; }
        public int[,] Coordinates { get; set; }
        protected char Character { get; set; }
        public ConsoleColor FGColor { get; set; }
        public ConsoleColor BGColor { get; set; }
        public int Idseed { get; }

        protected List<Item> backpack = new List<Item>();
        protected static int IdSeed = 4000;

        public int X { get; set; }
        public int Y { get; set; }
        

        public Entity(int Id, int y, int x, string name, char character, ConsoleColor bGColor, List<Item> backpack)
        {
            Id = IdSeed++;
            Name = name;
            Character = character;
            BGColor = bGColor;
            
            this.backpack = backpack;
            X = x;
            Y = y;
            Coordinates = new int[y, x];

        }

        public void MoveRight()
        {
             X++;
        }
        public void MoveLeft()
        {
            X--;
        }
        public void MoveUp()
        {
            Y++;
        }
        public void MoveDown()
        {
            Y--;
        }

        
    }
   
}
