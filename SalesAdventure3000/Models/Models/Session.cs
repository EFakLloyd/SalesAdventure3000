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
        public List<string[]> Avatars { get; set; }
        public Session()
        {
            this.GameMessages = new List<string>();
            this.Avatars = LoadAvatars();
        }
        public void StartNewSession(string name, int avatar)
        {
            CurrentPlayer = new Player(name, new int[] { 7, 22 }, avatar);  //Standard player starting coordinates.
            CurrentWorld = new World();
            CurrentWorld.CreateMap();
            CurrentWorld.Map[7, 22].Occupant=CurrentPlayer;
            CurrentWorld.CreateEntities();
            CurrentWorld.PopulateWorld(CurrentWorld.WorldEntities);
        }
        public void LoadSession()
        {
            CurrentPlayer = new Player();
            CurrentPlayer = JsonPackaging.LoadPlayer();
            CurrentWorld = new World();
            CurrentWorld.CreateMap();
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
        public void PickupItem(Item item)
        {

        }
        public void UseItem(Item item)
        {

        }
        private List<string[]> LoadAvatars()
        {
            List<string[]> avatars = new List<string[]>();

            using (StreamReader inputFile = new StreamReader("Images/avatars.txt"))
            {
                List<string> currentAvatar = new List<string>();
                string line;
                int lineCount = 0;

                while ((line = inputFile.ReadLine()) != null)
                {
                    currentAvatar.Add(line);
                    lineCount++;

                    if (lineCount % 10 == 0)
                    {
                        avatars.Add(currentAvatar.ToArray());
                        currentAvatar.Clear();
                    }
                }
            }
            return avatars;
        }
    }
}
