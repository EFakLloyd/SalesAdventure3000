using Engine.Models;

namespace Engine.Models
{
    public class Tile
    {
        public int Id { get; set; }
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

        public void DrawTile(List<Entity> entities)
        {
            if (Occupant != null)
            {
                Console.BackgroundColor = this.BGColor;
                Entity currentOccupant = entities.Find(a => a.Id == this.Occupant);
                Console.ForegroundColor = currentOccupant.FGColor;
                Console.Write(currentOccupant.Character.ToString()+ ' ');
                         
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
