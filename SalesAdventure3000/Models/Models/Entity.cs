﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Entity
    {
        public int Id { get; set; }
        protected string Name { get; set; }
        //public int[,] Coordinates { get; set; }
        public char Character { get; set; }
        public ConsoleColor FGColor { get; set; }
        //public ConsoleColor BGColor { get; set; }
        //public int Idseed { get; }

        //protected List<Item> backpack = new List<Item>();
        protected static int IdSeed = 4000;

        //public int X { get; set; }
        //public int Y { get; set; }

        //public Entity(int Id, int y, int x, string name, char character, ConsoleColor bGColor, List<Item> backpack)
        public Entity(string name, char character, ConsoleColor fGColor)
        {
            Id = IdSeed++;
            Name = name;
            Character = character;
            FGColor = fGColor;
            
            //this.backpack = backpack;
            //X = x;
            //Y = y;
            //Coordinates = new int[y, x];

        }

        

        
    }
   
}
