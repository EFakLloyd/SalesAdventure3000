using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class World
    {
        public Tile[,] Map { get; set; }    //Multidimensional array to store the map tiles and create a coordinate system. 
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Entity> WorldEntities { get; set; }  // Entities in the world are stored here.

        public World() 
        { 
            this.Width = 42;
            this.Height = 15;
            this.WorldEntities = new List<Entity>();
            this.Map = new Tile[Height, Width];
        }
        public void CreateEntities()  //Creates 20 entities. 1/4  Consumables, 1/4 Equipment, 2/4 Monsters
        {
            Random rnd = new Random();

            for (int i = 0; i < 20; i++)    
            {
                int entityType = rnd.Next(1,5);
                if (entityType == 1)
                    WorldEntities.Add(ConsumableFactory.CreateConsumable(rnd.Next(3000, 3004)));
                if (entityType == 2)
                    WorldEntities.Add(EquipmentFactory.CreateEquipment(rnd.Next(2000, 2007)));
                if (entityType >= 3)
                    WorldEntities.Add(MonsterFactory.CreateMonster(rnd.Next(1000, 1004)));
            }
        }
        public void CreateMap()   //Assigns Tile instances appropriate props based on the map string. Places the entities in the world at random.
        {
            string mapSketch = File.ReadAllText("Mapfiles/world1.txt");
            mapSketch = Regex.Replace(mapSketch, "[^X~.]", "");
            char[] mapArray = mapSketch.ToCharArray();
            int i = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Map[y, x] = new Tile(mapArray[i].ToString());   //Sends a char that decides what the Tile constructor will do.
                    i++;
                }
            }
        }
        public void PopulateWorld(List<Entity> entities)
        {
            Random rnd = new Random();

            foreach (Entity entity in entities)
            {
                while (true)
                {
                    int x = rnd.Next(0, 42);
                    int y = rnd.Next(0, 15);
                    if (Map[y, x].Occupant == null && Map[y, x].Passable)   //Makes sure that the Tile is passable and also not already taken.
                    {
                        entity.SetCoordinates(new int[] { y, x });
                        Map[y, x].NewOccupant(entity);
                        break;
                    }
                }
            }
        }   
    }
}
