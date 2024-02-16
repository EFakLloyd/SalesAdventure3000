using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Session
    {
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }

        public Session()
        {
        }

        public void CreatePlayer(string name)
        {
            //this.CurrentPlayer = new Player();
        }
        public void CreateNewWorld()
        {
            this.CurrentWorld = new World();
        }

        public void LoadSession()
        {
            //Load session
        }

        public void SaveSession()
        {
            //Save session
        }
    }
}
