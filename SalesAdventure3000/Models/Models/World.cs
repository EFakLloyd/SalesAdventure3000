using System.Text.RegularExpressions;

namespace Engine.Models
{
    public class World
    {
        public Tile[,] Map { get; private set; }    //Multidimensional array to store the map tiles and create a coordinate system. 
        public List<Entity> WorldEntities { get; private set; }  // Entities in the world are stored here.

        public World(Position gameDimensions)
        {
            this.WorldEntities = new List<Entity>();
            this.Map = createMap(gameDimensions);
        }
        private Tile[,] createMap(Position dimensions)   //Assigns Tile instances appropriate props based on the map string. Places the entities in the world at random.
        {
            Tile[,] map = new Tile[dimensions.Y, dimensions.X];
            string mapSketch = File.ReadAllText("Mapfiles/world1.txt");
            mapSketch = Regex.Replace(mapSketch, "[^X~.]", "");
            char[] mapArray = mapSketch.ToCharArray();
            int i = 0;

            for (int y = 0; y < dimensions.Y; y++)
            {
                for (int x = 0; x < dimensions.X; x++)
                {
                    map[y, x] = new Tile(mapArray[i].ToString());   //Sends a char that decides what the Tile constructor will do.
                    i++;
                }
            }
            return map;
        }
        public void InsertNewEntities(Position dimensions)  //Creates 20 entities. 1/4  Consumables, 1/4 Equipment, 2/4 Monsters
        {
            Random rnd = new Random();
            WorldEntities.Clear();

            for (int i = 0; i < 20; i++)
            {
                int entityType = rnd.Next(1, 5);
                if (entityType == 1)
                    WorldEntities.Add(ConsumableFactory.CreateConsumable(rnd.Next(3000, 3004)));
                if (entityType == 2)
                    WorldEntities.Add(EquipmentFactory.CreateEquipment(rnd.Next(2000, 2007)));
                if (entityType >= 3)
                    WorldEntities.Add(MonsterFactory.CreateMonster(rnd.Next(1000, 1004)));
            }
            foreach (Entity entity in WorldEntities)
            {
                while (true)
                {
                    int x = rnd.Next(0, dimensions.X);
                    int y = rnd.Next(0, dimensions.Y);
                    if (Map[y, x].Occupant == null && Map[y, x].Passable)   //Makes sure that the Tile is passable and also not already taken.
                    {
                        entity.SetCoordinates(new Position(y, x));
                        Map[y, x].NewOccupant(entity);
                        break;
                    }
                }
            }
        }
        public void InsertLoadedEntities(List<Entity> loadedEntities)       //Takes loaded entities and doles out to map at their respective coordinates.
        {
            WorldEntities = loadedEntities;
            foreach (Entity entity in WorldEntities)
                Map[entity.Coordinates.Y, entity.Coordinates.X].NewOccupant(entity);
        }
        public void RemoveEntity(Entity entity) => WorldEntities.Remove(entity);
    }
}
