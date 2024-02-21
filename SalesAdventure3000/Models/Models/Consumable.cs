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
        public bool Countdown { get; set; }
        public bool Used { get; set; }
        public int Uses { get; set; }
        public Consumable(string name, string appearance, ConsoleColor fgColor, Stat stat, int modifier, int? duration, int uses, string useMessage) : base(name, appearance, fgColor, stat, modifier, useMessage)
        {
            this.Duration = duration;
            this.Countdown = false;
            this.Used = false;
            this.Uses = uses;
        }
        public string GetNameForBackpack()
        {
            return Name + (Uses > 1 ? " x" + Uses : "");
        }
        public Player UsedItem(Player player)
        {
            Countdown = false;
            int mod = Used ? 1 : -1;
            switch (AffectedStat)
            {
                case Stat.Strength:
                    player.Strength += Modifier * mod;
                    break;
                case Stat.Vitality:
                    if (player.Vitality + Modifier * mod > player.MaxVitality)
                        player.Vitality = player.MaxVitality;
                    else
                        player.Vitality += Modifier * mod;
                    break;
                case Stat.MaxVitality:
                    player.MaxVitality += Modifier * mod;
                    break;
                case Stat.Armour:
                    player.Armour += Modifier * mod;
                    break;
                case Stat.Coolness:
                    player.Coolness += Modifier * mod;
                    break;
                default:
                    break;
            }
            if (Duration > 0 && Duration != null)
                Countdown = true;
            Used = false;
            return player;
        }
        public bool IsDurationOver()
        {
            if (Duration == 0)
            {
                return true;
            }
            Duration--;
            return false;
        }
    }
}
