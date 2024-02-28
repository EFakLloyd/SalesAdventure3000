using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Creature : Entity
    {
        public int Strength { get; set; }
        public int Vitality { get; set; }
        public int Coolness { get; set; }
        public int AvatarId { get; set; }
        public List<Item?> Backpack { get; set; }   //Monsters may also want to carry items as loot for the player.
        public Creature() 
        { 
            Backpack = new List<Item?>();
        }
        public Creature(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int coolness, int id, int avatarId ) : base(name, appearance, fgColor, id)
        {
            this.Strength = strength;
            this.Vitality = vitality;
            this.Coolness = coolness;
            this.AvatarId = avatarId;
        }
    }
}
