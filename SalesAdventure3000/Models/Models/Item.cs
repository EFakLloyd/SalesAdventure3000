using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Item : Entity
    {
        public Stat AffectedStat { get; private set; }  //The stat to be altered.
        public int Modifier { get; private set; }   //By how much?
        private string UseMessage;
        public Item() { }
        public Item(string name, string appearance, ConsoleColor fgColor, Stat stat, int modifier, string useMessage, int id) : base(name, appearance, fgColor, id)
        {
            this.AffectedStat = stat;
            this.Modifier = modifier;
            this.UseMessage = useMessage;
        }
        public string MessageUponUse()  //String that goes in GameMessages.
        {
            return UseMessage + Modifier + " " + AffectedStat + "."; 
        }
        public string MessageUponPickUp() //String that goes in GameMessages.
        {
            return "You picked up: " + Name;
        }
        public virtual string GetName()
        {
            return Name;
        }
    }
}
