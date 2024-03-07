namespace Engine.Models
{
    public class Monster : Creature
    {
        protected string AttackMessage;
        public Monster(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int armour, int coolness, string attackMessage, int avatarId, int id) : base(name, appearance, fgColor, strength, vitality, coolness, avatarId, armour, id)
        {
            this.AttackMessage = attackMessage;
        }
        protected override string MessageUponAttack(int damage) //Returns message for either miss or hit to the player.
        {
            if (damage == 0)
                return "The " + Name + " misses you, barely.";
            else
                return AttackMessage + damage + " damage.";
        }
        public void SetVitality(int vitality)
        {
            Vitality = vitality;
        }
        public new (ConsoleColor fgColor, string appearance, int avatarId) GetVisuals()
        {
            return (FGColor, Appearance, AvatarId);
        }
    }
}
