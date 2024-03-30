namespace Engine.Models
{
    public class Item : Entity
    {
        public Stat AffectedStat { get; private set; }  //The stat to be altered.
        public int Modifier { get; private set; }       //By how much?

        private string useMessage;
        public Item(string name, string appearance, ConsoleColor fgColor, Stat stat, int modifier, string useMessage, int id) : base(name, appearance, fgColor, id)
        {
            this.AffectedStat = stat;
            this.Modifier = modifier;
            this.useMessage = useMessage;
        }
        public string MessageUponUse() => useMessage + Modifier + " " + AffectedStat + "."; //String that goes in GameMessages.
        public string MessageUponPickUp() => "You picked up: " + Name;                      //String that goes in GameMessages.
        public virtual string GetName() => Name;
    }
}
