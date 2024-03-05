using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Engine.Models.Item;

namespace Engine.Models
{
    public class Creature : Entity
    {
        public int Strength { get; set; }
        public int Vitality { get; set; }
        public int MaxVitality { get; set; }
        public int Coolness { get; set; }
        public int AvatarId { get; set; }
        public List<Item> Backpack { get; set; }   //Monsters may also want to carry items as loot for the player.
        public int Armour { get; set; }

        public Creature() 
        { 
            Backpack = new List<Item>();
        }
        public Creature(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int coolness, int id, int avatarId ) : base(name, appearance, fgColor, id)
        {
            this.Strength = strength;
            this.Vitality = vitality;
            this.MaxVitality = vitality;
            this.Coolness = coolness;
            this.AvatarId = avatarId;
            this.Armour = 0;
        }
        public void AdjustStat(Item.Stat affectedStat, int modifier) //Adjusts one of the stats by the supplied modifer.
        {
            switch (affectedStat)
            {
                case Stat.Strength:
                    Strength += modifier;
                    break;
                case Stat.Vitality:
                    Vitality = Math.Min(Vitality + modifier, MaxVitality); //Ensures that Vitality does not go above the maximum value.
                    break;
                case Stat.MaxVitality:
                    MaxVitality += modifier;
                    break;
                case Stat.Armour:
                    Armour += modifier;
                    break;
                case Stat.Coolness:
                    Coolness += modifier;
                    break;
                default:
                    break;
            }
        }
    }
}
