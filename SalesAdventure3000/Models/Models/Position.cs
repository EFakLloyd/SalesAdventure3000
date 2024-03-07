namespace Engine.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int y, int x) { X = x; Y = y; }
    }
}
