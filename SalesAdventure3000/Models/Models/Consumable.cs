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
        //public bool Countdown { get; set; } //Items where Countdown is true gets checked each game loop
        public bool Timer { get; set; }
        public int Uses { get; set; }
        public Consumable(string name, string appearance, ConsoleColor fgColor, Stat stat, int modifier, int? duration, int uses, string useMessage) : base(name, appearance, fgColor, stat, modifier, useMessage)
        {
            this.Duration = duration;
            this.Timer = false;
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
                    Timer = false;
                }
                Duration--;
            }
        }
        //public void UsedItem(Player player, int index)      //Antagligen KEFF kod som borde skrivas om.
        //{
        //    //int upOrDown = Countdown ? 1 : -1;                               //flips the effect of a consumable, once it's Duration (if it has any) ends
        //    //player.AdjustPlayerStat(AffectedStat, Modifier*upOrDown);   //UpOrDown determines if its a bonus or malus
        //    //if (Duration > 0 && Duration != null)       //Only if the consu. has a duration value and is not null should it flip Countdown.
        //    //    Countdown = true;
        //    //else
        //    //    Countdown = false;
        //    //Uses--;                 //
        //    //if (Uses < 1)
        //    //    player.RemoveFromBackpack(index);
        //}
        //public bool IsDurationOver()
        //{
        //    if (Duration == 0)
        //        return true;
        //    Duration--;
        //    return false;
        //}
    }
}
