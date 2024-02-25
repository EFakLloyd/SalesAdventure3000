using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Consumable : Item
    {
        public int? Duration { get; set; }
        public bool TimerIsOn { get; set; }
        public int Uses { get; set; }
        public Consumable(string name, string appearance, ConsoleColor fgColor, Stat stat, int modifier, int? duration, int uses, string useMessage) : base(name, appearance, fgColor, stat, modifier, useMessage)
        {
            this.Duration = duration;
            this.TimerIsOn = false;
            this.Uses = uses;
        }
        public override string GetName()
        {
            return Name + (Uses > 1 ? " x" + Uses : "");
        }
        public void Countdown(Player player)
        {
            if (Duration != null)
            {
                if (Duration <= 0)
                {
                    player.AdjustPlayerStat(AffectedStat, Modifier*-1);
                    TimerIsOn = false;
                }
                Duration--;
            }
        }
    }
}
