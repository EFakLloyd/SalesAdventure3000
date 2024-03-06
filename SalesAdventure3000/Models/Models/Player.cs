using System.Drawing;
using static Engine.Models.Equipment;

namespace Engine.Models
{
    public class Player : Creature
    {
        public Dictionary<Equipment.Slot, Equipment?> EquippedItems { get; private set; } //Dict with a key for each of the equipment slots. Null = empty.
                                                                                    //Dict only takes unique keys, so we cannot wear more than one item per slot.
        public Player() { }
        public Player(string name, int[] coordinates, int avatarId) 
            : base(name, "@", ConsoleColor.DarkMagenta, 15, 15, 5, avatarId, 0, 0)
        {
            this.MaxVitality = Vitality;
            this.EquippedItems = new Dictionary<Equipment.Slot, Equipment?>
            {
                { Slot.Head, EquipmentFactory.CreateEquipment(2000) },
                { Slot.Weapon, EquipmentFactory.CreateEquipment(2002) },
                { Slot.Torso, EquipmentFactory.CreateEquipment(2001) },
                { Slot.Bling, EquipmentFactory.CreateEquipment(2005) }
            };
            Backpack.Add(ConsumableFactory.CreateConsumable(3000));
            Backpack.Add(ConsumableFactory.CreateConsumable(3002));
            Backpack.Add(ConsumableFactory.CreateConsumable(3003));

            SetCoordinates(coordinates);
        }
        public Player(string name, string appearance, int[] coordinates, int avatarId, ConsoleColor fgColor, int strength, int vitality, int maxVitality, int coolness, int armour, Dictionary<Equipment.Slot, Equipment?> equipment, List<Item> backpack, int id)
            : base(name, appearance, fgColor, strength, vitality, coolness, avatarId, armour, backpack, id)
        {
            this.MaxVitality = maxVitality;
            this.EquippedItems = equipment;

            SetCoordinates(coordinates);
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
            AdjustStat(equipment.AffectedStat, equipment.Modifier, Adjustment.Up);   //Apply bonus from equipment.
            RemoveFromBackpack(equipment);  
        }
        public void UseConsumable(Consumable consumable)    //Raises relevant stat, turns on timer for consumables which have one.
        {
            AdjustStat(consumable.AffectedStat, consumable.Modifier, Adjustment.Up);
            consumable.Activate();
            RemoveFromBackpack(consumable);
        }
        public void PutInBackpack(Item item)
        {
            Backpack.Add(item);
        }
        private void RemoveFromBackpack(Item item)
        {
            Backpack.Remove(item);
        }
        public Dictionary<Stat, string> GetStats()
        {
            return new Dictionary<Stat, string>
            {
                { Stat.Name,Name },
                { Stat.Vitality,Vitality.ToString() },
                { Stat.Strength,Strength.ToString() },
                { Stat.MaxVitality,MaxVitality.ToString() },
                { Stat.Coolness,Coolness.ToString() },
                { Stat.Armour,Armour.ToString() }
            };
        }
        public Item?[] GetEquipment()
        {
            List<Equipment?> equipment = new List<Equipment>();
            foreach (KeyValuePair<Equipment.Slot, Equipment?> item in EquippedItems)
                equipment.Add(item.Value);
            return equipment.ToArray();
        }
    }
}
