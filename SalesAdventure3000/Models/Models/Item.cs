namespace Engine.Models
{
    public class Item : Entity
    {
        public Stat AffectedStat { get; private set; }  //The stat to be altered.
        public int Modifier { get; private set; }       //By how much?

        private string UseMessage;
        public Item() { }
        public Item(string name, string appearance, ConsoleColor fgColor, Stat stat, int modifier, string useMessage, int id) : base(name, appearance, fgColor, id)
        {
            this.AffectedStat = stat;
            this.Modifier = modifier;
            this.UseMessage = useMessage;
        }
        public string MessageUponUse() => UseMessage + Modifier + " " + AffectedStat + "."; //String that goes in GameMessages.
        public string MessageUponPickUp() => "You picked up: " + Name;                      //String that goes in GameMessages.
        public virtual string GetName() => Name;
    }
}
