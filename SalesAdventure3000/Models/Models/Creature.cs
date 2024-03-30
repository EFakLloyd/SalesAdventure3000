namespace Engine.Models
{
    public abstract class Creature : Entity
    {
        protected int strength;
        protected int vitality;
        protected int maxVitality;
        protected int coolness;
        public int AvatarId { get; protected set; }
        public List<Item> Backpack { get; protected set; }   //Monsters may also want to carry items as loot for the player.
        public int Armour { get; protected set; }
        public Creature() { }
        public Creature(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int coolness, int avatarId, int armour, int id) : base(name, appearance, fgColor, id)
        {
            this.strength = strength;
            this.vitality = vitality;
            this.maxVitality = vitality;
            this.coolness = coolness;
            this.AvatarId = avatarId;
            this.Armour = armour;
            this.Backpack = new List<Item>();
        }
        public void AdjustStat(Item.Stat affectedStat, int modifier, Adjustment mod) //Adjusts one of the stats by the supplied modifer.
        {
            modifier *= mod == Adjustment.Up ? 1 : -1;

            switch (affectedStat)
            {
                case Stat.Strength:
                    strength += modifier;
                    break;
                case Stat.Vitality:
                    vitality = Math.Min(vitality + modifier, maxVitality); //Ensures that Vitality does not go above the maximum value.
                    break;
                case Stat.MaxVitality:
                    maxVitality += modifier;
                    break;
                case Stat.Armour:
                    Armour += modifier;
                    break;
                case Stat.Coolness:
                    coolness += modifier;
                    break;
                default:
                    break;
            }
        }
        public (string message, bool opponentIsDead) Attack(Creature opponent)  //Standard attack.
        {
            int damage = 0;
            Random roll = new Random();
            for (int i = 0; i < strength; i++)
            {
                if (roll.Next(1, 4) == 1)   //Every point in strength gives a 1/3 chance to do 1 damage.
                    damage++;
            }
            damage = Math.Max(damage - opponent.Armour, 0);                 //Adjust for armour
            if (damage > 0)
                opponent.AdjustStat(Stat.Vitality, damage, Adjustment.Down);
            return (messageUponAttack(damage), opponent.IsDead());
        }
        protected abstract string messageUponAttack(int damage);            //Returns string for GameMessage.
        public bool IsDead() => vitality > 0 ? false : true;     
    }
}
