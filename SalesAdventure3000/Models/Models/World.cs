﻿using System;
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
        public Tile[,] Map { get; set; }    //Multidimensional array to store the map tiles and create a coordinate system. 
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Entity> Entities { get; set; }  // Entities in the world are stored here.

        public World() 
        { 
            this.Width = 42;
            this.Height = 15;
            this.Entities = CreateEntities();
            this.Map = CreateAndPopulateWorld(Entities);
        }
        private List<Entity> CreateEntities()  //Creates 20 entities. 1/4  Consumables, 1/4 Equipment, 2/4 Monsters
        {
            Random rnd = new Random();
            List<Entity> entities = new List<Entity>();

            for (int i = 0; i < 20; i++)    
            {
                int entityType =  rnd.Next(1,5);
                if (entityType == 1)
                    entities.Add(ConsumableFactory.CreateConsumable(rnd.Next(3000, 3004)));
                if (entityType == 2)
                    entities.Add(EquipmentFactory.CreateEquipment(rnd.Next(2000, 2007)));
                if (entityType >= 3)
                    entities.Add(MonsterFactory.CreateMonster(rnd.Next(1000, 1004)));
            }
            return entities;
        }
        private Tile[,] CreateAndPopulateWorld(List<Entity> entities)   //Assigns Tile instances appropraite props based on the map string. Places the entities in the world at random.
        {
            Random rnd = new Random();
            string mapSketch = "XXXXXXXX~~~~~~~~~~~~~XXXXXXXXXXXXXX~~~~~~~\r\nXXXXXXXXX~~~~~~~~~..XXX.XXXXXXXXXXXXX~~~~~\r\nXXX..XXXXX~~~~~~~..XXX.........XXXXXXXX..~\r\nXX........~~~~~...XXX.............XXXXXX..\r\nXXX.........~~..XXX.............XXXX..XXX.\r\nX...........~..................XX.........\r\n..........~~.........................~....\r\n.........~........................X.~~~...\r\n......~~.........XXX.............XX..~~~..\r\n.....~~........XXX...~~~~~.......XX.......\r\n~~~.~~...........~~~~~~~~~~~~......XX.....\r\n~~~~~........~~~~~~~~~~~~~~~~~~....XXXX..X\r\n~~~~~.........~~~~~..~~~~~~~~~~......XXXXX\r\n~~~~~~~...............~~~~~~~~~~~......XXX\r\n~~~~~~~~~..~~~.........~~~~~~~~~~~~~~...XX".Replace("\r", "").Replace("\n", "");
            char[] mapArray = mapSketch.ToCharArray();

            Tile[,] map = new Tile[Height, Width];

            int i = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    map[y, x] = new Tile(mapArray[i].ToString());   //Sends a char that decides what the Tile constructor will do.
                    i++;
                }
            }
            
            foreach (Entity entity in entities)
            {
                while (true)
                {
                    int x = rnd.Next(0, 42);
                    int y = rnd.Next(0, 15);
                    if (map[y, x].Occupant == null && map[y, x].Passable)   //Makes sure that the Tile is passable and also not already taken.
                    {
                        map[y, x].Occupant = entity;
                        break;
                    }
                }
            }
            return map;
        }
    }
}
