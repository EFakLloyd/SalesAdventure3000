using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class World
    {
        public Tile[,] Map { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Entity> Entities { get; set; }

        public World() 
        { 
            this.Width = 42;
            this.Height = 15;
            this.Entities = CreateEntities();
            this.Map = CreateWorld(Entities);
        }
        private List<Entity> CreateEntities()
        {
            Random rnd = new Random();
            List<Entity> entities = new List<Entity>();

            for (int i = 0; i < 20; i++)
            {
                Entity entity = rnd.Next(1, 3) == 2 ? new Creature("Orm", 'S', ConsoleColor.Cyan) : new Item("Kudde", '#', ConsoleColor.White);
                entities.Add(entity);
            }
            return entities;
        }
        private Tile[,] CreateWorld(List<Entity> entities)
        {
            Random rnd = new Random();
            string mapSketch = "XXXXXXXX~~~~~~~~~~~~~XXXXXXXXXXXXXX~~~~~~~\r\nXXXXXXXXX~~~~~~~~~..XXX.XXXXXXXXXXXXX~~~~~\r\nXXX..XXXXX~~~~~~~..XXX.........XXXXXXXX..~\r\nXX........~~~~~...XXX.............XXXXXX..\r\nXXX.........~~..XXX.............XXXX..XXX.\r\nX...........~..................XX.........\r\n..........~~.........................~....\r\n.........~........................X.~~~...\r\n......~~.........XXX.............XX..~~~..\r\n.....~~........XXX...~~~~~.......XX.......\r\n~~~.~~...........~~~~~~~~~~~~......XX.....\r\n~~~~~........~~~~~~~~~~~~~~~~~~....XXXX..X\r\n~~~~~.........~~~~~..~~~~~~~~~~......XXXXX\r\n~~~~~~~...............~~~~~~~~~~~......XXX\r\n~~~~~~~~~..~~~.........~~~~~~~~~~~~~~...XX".Replace("\r", "").Replace("\n", ""); ;
            //string mapSketch2 = "XXXXXXXX~~~~~~~~~~~~~XXXXX\r\nXXXXXXXXX~~~~~~~~~..XXX.XX\r\nXXX..XXXXX~~~~~~~..XXX...X\r\nXX........~~~~~...XXX.....\r\nXXX.........~~..XXX.......\r\nX.........................\r\n..........~~..............\r\n.........~...............~\r\n......~~.........XXX....~~\r\n.....~~........XXX...~~~~~\r\n~~~.~~...........~~~~~~~~~\r\n~~~~~........~~~~~~~~~~~~~\r\n~~~~~.........~~~~~..~~~~~\r\n~~~~~~~...............~~~~\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~".Replace("\r", "").Replace("\n", "");
            char[] mapArray = mapSketch.ToCharArray();

            Tile[,] map = new Tile[Height, Width];

            int i = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    map[y, x] = new Tile(y, x, null, mapArray[i].ToString());
                    i++;
                }
            }
            
            foreach (Entity entity in entities)
            {
                while (true)
                {
                    int x = rnd.Next(0, 42);
                    int y = rnd.Next(0, 15);
                    if (map[y, x].Occupant == null && map[y, x].Passable)
                    {
                        map[y, x].Occupant = entity.Id;
                        break;
                    }
                }
            }
            return map;
        }
    }
}
