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
            Coolness,
            AvatarId
        }
        public enum Adjustment      //Designates bonus or malus modifiers.
        {
            Up,
            Down
        }

        public string Name { get; protected set; }
        protected string Appearance;                    //The "texture" of the entity when drawn on the world map.
        protected ConsoleColor FGColor;                 //Assiged color.
        public int Id { get; protected set; }
        public Position Coordinates { get; protected set; }

        public Entity() { }
        public Entity(string name, string appearance, ConsoleColor fGColor, int id)
        {
            this.Name = name;
            this.Appearance = appearance;
            this.FGColor = fGColor;
            this.Id = id;
        }
        public virtual (ConsoleColor fgColor, string appearance) GetVisuals() => (FGColor, Appearance);     //Returns needed information to draw the entity on map.
        public void SetCoordinates(Position coordinates) => Coordinates = coordinates;
    }
}
