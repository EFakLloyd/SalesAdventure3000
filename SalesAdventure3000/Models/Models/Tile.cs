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
        public static int Id { get; set; }
        public int[,] Coordinates { get; set; }
        public ConsoleColor FGColor { get; set; }
        public ConsoleColor BGColor { get; set; }
        public string Texture { get; set; }
        public bool Passable { get; set; }
        public int? Occupant { get; set; }

        private static int IdSeed = 2000;

        public Tile(int x, int y, int? occupant, string type)
        {
            Random textureFlip = new Random();

            Coordinates = new int[y,x];
            Id = IdSeed;
            IdSeed++;

            if (type == ".")
            {
                Passable = true;
                FGColor = ConsoleColor.Green;
                BGColor = ConsoleColor.DarkGreen;
                Texture = textureFlip.Next(1, 3) == 1 ? " ." : "  ";
            }
            else if (type == "~")
            {
                Passable = false;
                FGColor = ConsoleColor.Blue;
                BGColor = ConsoleColor.DarkBlue;
                Texture = textureFlip.Next(1, 4) == 1 ? "~~" : " ~";
            }
            else
            {
                Passable = false;
                FGColor = ConsoleColor.Gray;
                BGColor = ConsoleColor.DarkGray;
                Texture = "▲▲";
            }

            Occupant = Passable ? occupant : null;
        }

        public void DrawTile ()
        {
            
            if (Occupant != null)
            {
                //Call on occupant via ID and use its draw function.
                Console.Write("&&");
            }
            else
            {
                Console.ForegroundColor = this.FGColor;
                Console.BackgroundColor = this.BGColor;
                Console.Write(this.Texture);
            }
        }
    }
}
