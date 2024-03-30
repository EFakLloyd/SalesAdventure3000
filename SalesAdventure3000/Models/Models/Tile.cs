namespace Engine.Models
{
    public class Tile
    {
        private ConsoleColor fgColor;
        private ConsoleColor bgColor;
        private string texture;                         //Texture of the tile when drawn on the world map.
        public bool Passable { get; private set; }      //Can entities be on or enter the tile?
        public Entity? Occupant { get; private set; }   //Potential entity on the tile.
        public Tile(string type)    //Based what is read from the map string, assign the tile different properties.
        {
            Random textureFlip = new Random();  //Used to randomize the texture on tiles.

            if (type == ".")        //Creates grasslands.
            {
                Passable = true;
                fgColor = ConsoleColor.Green;
                bgColor = ConsoleColor.DarkGreen;
                texture = textureFlip.Next(1, 4) == 1 ? " ." : textureFlip.Next(1, 3) == 1 ? ". " : "  ";
            }
            else if (type == "~")   //Creates water.
            {
                Passable = false;
                fgColor = ConsoleColor.Blue;
                bgColor = ConsoleColor.DarkBlue;
                texture = textureFlip.Next(1, 4) == 1 ? "~~" : textureFlip.Next(1, 3) == 1 ? "~ " : " ~";
            }
            else                    //Creates mountains.
            {
                Passable = false;
                fgColor = ConsoleColor.Gray;
                bgColor = ConsoleColor.DarkGray;
                texture = "^^";
            }
        }
        public void DrawTile()                          //A tile writes itself to the screen.
        {
            Console.BackgroundColor = this.bgColor;
            if (Occupant != null)                       //If there is an occupant entity we draw that instead.
            {
                var occupantVisuals = Occupant.GetVisuals();
                Console.ForegroundColor = occupantVisuals.fgColor;
                Console.Write(occupantVisuals.appearance.PadRight(2, ' '));
            }
            else                                        //Writes the tile texture with appropiate colours.
            {
                Console.ForegroundColor = this.fgColor;
                Console.Write(this.texture);
            }
        }
        public void NewOccupant(Entity entity) => Occupant = entity;
        public void ClearOccupant() => Occupant = null;
    }
}
