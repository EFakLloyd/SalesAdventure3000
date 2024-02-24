using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Item : Entity
    {
        public enum Stat
        {
            Strength,
            Vitality,
            MaxVitality,
            Armour,
            Coolness
        }
        public Stat AffectedStat { get; set; }
        public int Modifier { get; set; }
        public string UseMessage { get; set; }
        protected static int IdSeed = 4000;
        public Item(string name, string appearance, ConsoleColor fgColor, Stat stat, int modifier, string useMessage) : base(name, appearance, fgColor)
        {
            this.AffectedStat = stat;
            this.Modifier = modifier;
            this.UseMessage = useMessage;
        }
        public string MessageUponUse()
        {
            return UseMessage + Modifier + " " + AffectedStat + "."; 
        }
        public string MessageUponPickUp() 
        {
            return "You picked up: " + Name;
        }
        public virtual string GetName()
        {
            return Name;
        }
    }
}
