using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Engine.Models.Item;

namespace Engine.Models
{
    public class Session    //Session class handles world and player creation, interaction between the player and the world based on input, saving and loading the game.
    {                       
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<string> GameMessages { get; set; }  //Holds strings that are displayed in the info box.
        public Session()
        {
            GameMessages = new List<string>();
        }
        public void StartNewSession(string name)
        {
            CurrentPlayer = new Player(name, new int[] { 7, 22 });  //Standard player starting coordinates.
            CurrentWorld = new World();
            CurrentWorld.CreateWorld();
            CurrentWorld.Map[7, 22].Occupant=CurrentPlayer;
            CurrentWorld.CreateEntities();
            CurrentWorld.PopulateWorld(CurrentWorld.WorldEntities);
        }
        public void LoadSession()
        {
            CurrentPlayer = new Player();
            CurrentPlayer = JsonPackaging.LoadPlayer();
            CurrentWorld = new World();
            CurrentWorld.CreateWorld();
            CurrentWorld.Map[CurrentPlayer.Coordinates[0], CurrentPlayer.Coordinates[1]].Occupant = CurrentPlayer;
            CurrentWorld.WorldEntities = JsonPackaging.LoadEntities();
            CurrentWorld.PopulateWorld(CurrentWorld.WorldEntities);
        }
        public void SaveSession()
        {
            JsonPackaging.CreateJson(this);
        }
        public void MovePlayer()
        {

        }
        public void HandleCombat()
        {

        }
        public void PickupItem()
        {

        }
        public void UseItem()
        {

        }
    }
}
