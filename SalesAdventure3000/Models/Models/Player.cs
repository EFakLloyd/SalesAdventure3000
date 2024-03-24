using static Engine.Models.Equipment;

namespace Engine.Models
{
    public class Player : Creature
    {
        public Position OldCoordinates { get; private set; }
        public Dictionary<Equipment.Slot, Equipment?> EquippedItems { get; private set; } //Dict with a key for each of the equipment slots. Null = empty.
                                                                                          //Dict only takes unique keys, so we cannot wear more than one item per slot.
        public Player() { }
        public Player(string name, Position coordinates, int avatarId)
            : base(name, "@", ConsoleColor.DarkMagenta, 15, 15, 5, avatarId, 0, 0)
        {
            this.MaxVitality = Vitality;
            this.EquippedItems = new Dictionary<Equipment.Slot, Equipment?>
            {
                { Slot.Head, null },
                { Slot.Weapon, null },
                { Slot.Torso, null },
                { Slot.Bling, null }
            };
            Backpack.Add(ConsumableFactory.CreateConsumable(3000));
            SetCoordinates(coordinates);
            SetOldCoordinates(Coordinates);
        }
        public (string message, bool opponentIsDead) RecklessAttack(Creature opponent)
        {
            int damage = 0;
            Random roll = new Random();
            for (int i = 0; i < Strength * 1.5; i++)
            {
                if (roll.Next(1, 4) == 1)   //Every point in strength gives a 1/3 chance to do 1 damage
                    damage++;
            }
            damage = Math.Max(damage - opponent.Armour, 0); //Adjust for armour
            if (damage > 0)
                opponent.AdjustStat(Stat.Vitality, damage, Adjustment.Down);
            return (MessageUponAttack(damage), opponent.IsDead());
        }
        protected override string MessageUponAttack(int damage) //Returns string for GameMessage. Takes into account the weapon used.
        {
            string weapon = EquippedItems[Slot.Weapon] == null ? "fists" : EquippedItems[Slot.Weapon].Name;
            if (damage == 0)
                return "You miss!";
            return "You swing your " + weapon + " for " + damage + " damage.";
        }
        public void TakeOff(Equipment.Slot type)  //Removes equipment from slot and places it in the backpack.
        {
            PutInBackpack(EquippedItems[type]);
            AdjustStat(EquippedItems[type].AffectedStat, EquippedItems[type].Modifier, Adjustment.Down);    //Readjusts player stats.
            EquippedItems[type] = null;
        }
        public void PutOn(Equipment equipment)  //Applies equipment to the appropriate equipment slot.
        {
            if (EquippedItems[equipment.Type] != null)  //Removes existing item from slot, if any.
                TakeOff(equipment.Type);
            EquippedItems[equipment.Type] = equipment;
            AdjustStat(equipment.AffectedStat, equipment.Modifier, Adjustment.Up);   //Apply bonus from equipment.
            RemoveFromBackpack(equipment);
        }
        public void UseConsumable(Consumable consumable)    //Raises relevant stat, turns on timer for consumables which have one.
        {
            AdjustStat(consumable.AffectedStat, consumable.Modifier, Adjustment.Up);
            consumable.Activate();
            if (consumable.Uses == 0)
                RemoveFromBackpack(consumable);
        }
        public Dictionary<Stat, string> GetData()
        {
            Dictionary<Stat, string> playerStats = new Dictionary<Stat, string>
            {
                { Stat.Name,Name },
                { Stat.Vitality,Vitality.ToString() },
                { Stat.Strength,Strength.ToString() },
                { Stat.MaxVitality,MaxVitality.ToString() },
                { Stat.Coolness,Coolness.ToString() },
                { Stat.Armour,Armour.ToString() },
                { Stat.AvatarId,AvatarId.ToString() }
            };
            return playerStats;
        }
        public Item?[] GetEquippedItems()
        {
            List<Equipment?> equipment = new List<Equipment>();
            foreach (KeyValuePair<Equipment.Slot, Equipment?> item in EquippedItems)
                equipment.Add(item.Value);
            return equipment.ToArray();
        }
        public void SetLoadedValues(string name, int avatarId, int maxVitality, int vitality, int strength, int coolness, int armour, Position coordinates, Dictionary<Equipment.Slot, Equipment?> equippedItems, List<Item> backpack)
        {
            Name = name;
            Appearance = "@";
            FGColor = ConsoleColor.DarkMagenta;
            AvatarId = avatarId;
            MaxVitality = maxVitality;
            Vitality = vitality;
            Strength = strength;
            Coolness = coolness;
            Armour = armour;
            Coordinates = coordinates;
            EquippedItems = equippedItems;
            Backpack = backpack;
        }
        public void SetOldCoordinates(Position position) => OldCoordinates = position;  //Used for redrawing tiles on map.
        public void PutInBackpack(Item item) => Backpack.Add(item);
        private void RemoveFromBackpack(Item item) => Backpack.Remove(item);
    }
}
