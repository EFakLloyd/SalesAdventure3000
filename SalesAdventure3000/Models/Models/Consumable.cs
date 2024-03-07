namespace Engine.Models
{
    public class Consumable : Item
    {
        private int? Duration;  //Used for items that give a temporary boost. Ticks down via Coundown()
        public bool TimerIsOn { get; private set; } //Used for checking which items are on a countdown.
        public int Uses { get; private set; }   //Multiple use items. 
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
            if (Duration <= 0)
            {
                player.AdjustStat(AffectedStat, Modifier, Adjustment.Down); //inverts modifier.
                TimerIsOn = false;  //No more need to check item.
            }
            Duration--;
        }
        public void Activate()
        {
            if (Duration != null)
                TimerIsOn = true;
            Uses--;
        }
        public (int? duration, bool timerIsOn, int uses) GetStatsOnSave()
        {
            return (Duration, TimerIsOn, Uses);
        }
        public void SetStatsOnLoad(int? duration, bool timerIsOn, int uses)
        {
            Duration = duration;
            TimerIsOn = timerIsOn;
            Uses = uses;
        }
    }
}
