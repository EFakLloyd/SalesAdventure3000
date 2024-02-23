using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Session
    {
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<string> GameMessages { get; set; }

        public Session()
        {
        }

        public void CreatePlayer(string name)
        {
            CurrentPlayer = new Player(name, new int[] { 7, 22 });
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
