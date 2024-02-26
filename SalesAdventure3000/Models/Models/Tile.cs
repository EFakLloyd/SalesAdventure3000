using Engine.Models;

namespace Engine.Models
{
    public class Tile
    {
        public ConsoleColor FGColor { get; set; }
        public ConsoleColor BGColor { get; set; }
        public string Texture { get; set; } //Texture of the tile when drawn on the world map.
        public bool Passable { get; set; }  //Can entities be on or enter the tile?
        public Entity? Occupant { get; set; }   //Potential entity on the tile.

        public Tile(string type)    //Based what is read from the map string, assign the tile different properties.
        {
            Random textureFlip = new Random();  //Used to randomize the texture on tiles.

            if (type == ".")    //Creates grasslands.
            {
                Passable = true;
                FGColor = ConsoleColor.Green;
                BGColor = ConsoleColor.DarkGreen;
                Texture = textureFlip.Next(1, 4) == 1 ? " ." : textureFlip.Next(1, 3) == 1 ? ". " : "  ";
            }
            else if (type == "~")   //Creates water.
            {
                Passable = false;
                FGColor = ConsoleColor.Blue;
                BGColor = ConsoleColor.DarkBlue;
                Texture = textureFlip.Next(1, 4) == 1 ? "~~" : textureFlip.Next(1, 3) == 1 ? "~ " : " ~";
            }
            else                    //Creates mountains.
            {
                Passable = false;
                FGColor = ConsoleColor.Gray;
                BGColor = ConsoleColor.DarkGray;
                Texture = "^^";
            }
        }

        public void DrawTile()  //A tile writes itself to the screen.
        {
            Console.BackgroundColor = this.BGColor;
            if (Occupant != null)   //If there is an occupant entity we draw that instead.
            {
                Console.ForegroundColor = Occupant.FGColor;
                Console.Write(Occupant.Appearance.PadRight(2, ' '));
            }
            else            //Writes the tile texture with appropiate colours.
            {
                Console.ForegroundColor = this.FGColor;
                Console.Write(this.Texture);
            }
        }
    }
}
