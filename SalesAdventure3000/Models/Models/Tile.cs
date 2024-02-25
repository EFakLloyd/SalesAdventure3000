using Engine.Models;

namespace Engine.Models
{
    public class Tile
    {
        public ConsoleColor FGColor { get; set; }
        public ConsoleColor BGColor { get; set; }
        public string Texture { get; set; }
        public bool Passable { get; set; }
        public Entity? Occupant { get; set; }

        private static int IdSeed = 2000;

        public Tile(string type)
        {
            Random textureFlip = new Random();

            if (type == ".")
            {
                Passable = true;
                FGColor = ConsoleColor.Green;
                BGColor = ConsoleColor.DarkGreen;
                //Texture = textureFlip.Next(1, 3) == 1 ? " ." : "  ";
                Texture = textureFlip.Next(1, 4) == 1 ? " ." : textureFlip.Next(1, 3) == 1 ? ". " : "  ";
            }
            else if (type == "~")
            {
                Passable = false;
                FGColor = ConsoleColor.Blue;
                BGColor = ConsoleColor.DarkBlue;
                Texture = textureFlip.Next(1, 4) == 1 ? "~~" : textureFlip.Next(1, 3) == 1 ? "~ " : " ~";
            }
            else
            {
                Passable = false;
                FGColor = ConsoleColor.Gray;
                BGColor = ConsoleColor.DarkGray;
                Texture = "^^";
            }
        }

        public void DrawTile()
        {
            Console.BackgroundColor = this.BGColor;
            if (Occupant != null)
            {
                //Entity currentOccupant = entities.Find(a => a.Id == this.Occupant);
                Console.ForegroundColor = Occupant.FGColor;
                Console.Write(Occupant.Appearance.PadRight(2, ' '));
                         
            }
            else
            {
                Console.ForegroundColor = this.FGColor;
                Console.Write(this.Texture);
            }
        }
    }
}
