namespace Engine.Models
{
    public class Consumable : Item
    {
        private int? duration;  //Used for items that give a temporary boost. Ticks down via Coundown(). Null for consumables without timers.
        public bool TimerIsOn { get; private set; } //Used for checking which items are on a countdown.
        public int Uses { get; private set; }   //Multiple use items. 
        public Consumable(string name, string appearance, ConsoleColor fgColor, Stat stat, int modifier, int? durationValue, int uses, string useMessage, int id) : base(name, appearance, fgColor, stat, modifier, useMessage, id)
        {
            this.duration = durationValue;
            this.TimerIsOn = false;
            this.Uses = uses;
        }
        public bool Countdown()    //Is called to reduce duration, based on TimerIsOn. Upon duration end the boost to the player is withdrawn.
        {
            if (duration <= 0)
                return false;    //No more need to check consumable. Return false bool to revert stat changes.
            duration--;
            return true;
        }
        public void Activate()      //Starts countdown and/or reduces number of uses (single use consumables will be removed after this in the session class.
        {
            if (duration != null)   //Only affects consumables with a set duration. 
                TimerIsOn = true;
            Uses--;
        }
        public void SetStatsOnLoad(int? duration, bool timerIsOn, int uses)     //Insert values loaded from save into the player's consumables.
        {
            this.duration = duration;
            TimerIsOn = timerIsOn;
            Uses = uses;
        }
        public override string GetName() => Name + (Uses > 1 ? " x" + Uses : "");     //Returns name, formatting for multiple uses.
        public (int? duration, bool timerIsOn, int uses) GetStatsOnSave() => (duration, TimerIsOn, Uses);   //Extracts the consumable's stats at the point of save.
    }
}
