namespace Engine.Models
{
    public abstract class Creature : Entity
    {
        protected int Strength;
        protected int Vitality;
        protected int MaxVitality;
        protected int Coolness;
        public int AvatarId { get; protected set; }
        public List<Item> Backpack { get; protected set; }   //Monsters may also want to carry items as loot for the player.
        public int Armour { get; protected set; }

        public Creature() { }
        public Creature(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int coolness, int avatarId, int armour, int id) : base(name, appearance, fgColor, id)
        {
            this.Strength = strength;
            this.Vitality = vitality;
            this.MaxVitality = vitality;
            this.Coolness = coolness;
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
                    Strength += modifier;
                    break;
                case Stat.Vitality:
                    Vitality = Math.Min(Vitality + modifier, MaxVitality); //Ensures that Vitality does not go above the maximum value.
                    break;
                case Stat.MaxVitality:
                    MaxVitality += modifier;
                    break;
                case Stat.Armour:
                    Armour += modifier;
                    break;
                case Stat.Coolness:
                    Coolness += modifier;
                    break;
                default:
                    break;
            }
        }
        public (string message, bool opponentIsDead) Attack(Creature opponent)
        {
            int damage = 0;
            Random roll = new Random();
            for (int i = 0; i < Strength; i++)
            {
                if (roll.Next(1, 4) == 1)
                    damage++;
                //damage = roll.Next(0, 4) == 1 ? damage++ : damage; //Every point in strength gives a 1/3 chance to do 1 damage
            }
            damage = Math.Max(damage - opponent.Armour, 0); //Adjust for armour
            if (damage > 0)
                opponent.AdjustStat(Stat.Vitality, damage, Adjustment.Down);
            return (MessageUponAttack(damage), opponent.IsDead());
        }
        protected virtual string MessageUponAttack(int damage) //Returns string for GameMessage. Takes into account the weapon used.
        {
            return "";
        }
        public bool IsDead()
        {
            return Vitality > 0 ? false : true;
        }
    }
}
