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
            CurrentWorld = new World();
        }
    }
}
