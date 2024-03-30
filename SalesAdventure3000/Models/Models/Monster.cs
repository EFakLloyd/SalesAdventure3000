namespace Engine.Models
{
    public class Monster : Creature
    {
        protected string attackMessage;
        public Monster(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int armour, int coolness, string attackMessage, int avatarId, int id) : base(name, appearance, fgColor, strength, vitality, coolness, avatarId, armour, id)
        {
            this.attackMessage = attackMessage;
        }
        protected override string messageUponAttack(int damage) //Returns message for either miss or hit to the player.
        {
            if (damage == 0)
                return "The " + Name + " misses you, barely.";
            else
                return attackMessage + damage + " damage.";
        }
        public string MessageUponDefeat() => "You've defeated the " + Name + "!";
        public void SetVitality(int vitality) => base.vitality = vitality;
        public new (ConsoleColor fgColor, string appearance, int avatarId) GetVisuals() => (fgColor, appearance, AvatarId); //Monsters also have an avatar (compared to entity).
    }
}
