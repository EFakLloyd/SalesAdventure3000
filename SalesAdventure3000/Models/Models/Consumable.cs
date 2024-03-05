using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Consumable : Item
    {
        public int? Duration { get; set; }  //Used for items that give a temporary boost. Ticks down via Coundown()
        public bool TimerIsOn { get; set; } //Used for checking which items are on a countdown.
        public int Uses { get; set; }   //Multiple use items. 
        public Consumable() { }
        public Consumable(string name, string appearance, ConsoleColor fgColor, Stat stat, int modifier, int? duration, int uses, string useMessage, int id) : base(name, appearance, fgColor, stat, modifier, useMessage, id)
        {
            this.Duration = duration;
            this.TimerIsOn = false;
            this.Uses = uses;
        }
        public override string GetName()    //Returns name, formatting for multiple uses.
        {
            return Name + (Uses > 1 ? " x" + Uses : "");    
        }
        public void Countdown(Player player)    //Is called to reduce duration, based on TimerIsOn. Upon duration end the boost to the player is withdrawn.
        {
            if (Duration != null)   //Safeguards that only items with an actual Duration is handled.
            {
                if (Duration <= 0)
                {
                    player.AdjustStat(AffectedStat, Modifier * - 1); //"* - 1" inverts modifier.
                    TimerIsOn = false;  //No more need to check item.
                }
                Duration--;
            }
        }
    }
}
