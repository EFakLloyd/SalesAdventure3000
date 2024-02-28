using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Engine.Models.Item;

namespace Engine.Models
{
    public class Equipment : Item
    {
        public enum Slot  //A piece of equipment can only be worn on a specific slot. Used in AdventureBiew and Player classes.
        {
            Head,
            Torso,
            Weapon,
            Bling
        }
        public Slot Type { get; set; }    
        public Equipment() { }
        public Equipment(string name, string appearance, ConsoleColor fgColor, Slot type, Stat stat, int modifier, string useMessage, int id) : base(name, appearance, fgColor, stat, modifier, useMessage, id)
        {
            this.Type = type;
        }
    }
}
