using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Session    //Session class handles world and player creation, interaction between the player and the world based on input, saving and loading the game.
    {                       
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<string> GameMessages { get; set; }  //Holds strings that are displyed in the info box.

        public Session()
        {
            GameMessages = new List<string>();
        }

        public void CreatePlayer(string name)
        {
            CurrentPlayer = new Player(name, new int[] { 7, 22 });  //Standard player starting coordinates.
            CurrentWorld.Map[7, 22].Occupant=CurrentPlayer;
        }
        public void CreateNewWorld()
        {
            this.CurrentWorld = new World();
        }

        public void LoadSession()
        {
            //Skräddarsydd inmatning av värden från sparfil-klass till Session.


            //string fileName = "SavedSession.json";
            //string jsonString = File.ReadAllText(fileName);
            //Session currentSession = JsonSerializer.Deserialize<Session>(jsonString)!;
        }

        public void SaveSession()
        {
            //Behöver göra en skräddarsydd klass för att hålla information på ett sätt som går att serialisera till json, och bryta ut den manuellt. Inga [,]-arrayer (som finns bl.a. finns i World och Player).


            //Session testSession = new Session();
            //testSession.CreateNewWorld();
            //testSession.CreatePlayer("Berit");
            //string fileName = "SavedSession.json";
            //string jsonString = JsonSerializer.Serialize(testSession.CurrentPlayer);
            //File.Create(fileName);

            //File.WriteAllText(fileName, jsonString);

            //Console.WriteLine(File.ReadAllText(fileName));

            //Console.WriteLine(jsonString);
        }
    }
}
