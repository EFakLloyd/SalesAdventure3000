using System.Drawing;
using static Engine.Models.Equipment;

namespace Engine.Models
{
    public class Player : Creature
    {
        private Dictionary<Equipment.Slot, Equipment?> EquippedItems;  //Dict with a key for each of the equipment slots. Null = empty.
                                                                                    //Dict only takes unique keys, so we cannot wear more than one item per slot.
        public Player()
        {

        }
        public Player(string name, int[] coordinates, int avatar) : base(name, "@", ConsoleColor.DarkMagenta, 15, 15, 5, 0000, avatar)
        {
            this.Name = name;
            this.AvatarId = avatar;
            this.Coordinates = coordinates;
            this.Armour = 0;
            this.EquippedItems = new Dictionary<Equipment.Slot, Equipment?>();
            this.EquippedItems.Add(Slot.Head, EquipmentFactory.CreateEquipment(2000));
            this.EquippedItems.Add(Slot.Weapon, EquipmentFactory.CreateEquipment(2002));
            this.EquippedItems.Add(Slot.Torso, EquipmentFactory.CreateEquipment(2001));
            this.EquippedItems.Add(Slot.Bling, EquipmentFactory.CreateEquipment(2005));
            this.Backpack = new List<Item>();
            Backpack.Add(ConsumableFactory.CreateConsumable(3000));
            Backpack.Add(ConsumableFactory.CreateConsumable(3002));
            Backpack.Add(ConsumableFactory.CreateConsumable(3003));
        }
        public Player(string name, string appearance, int[] coordinates, int avatarId, ConsoleColor fgColor, int strength, int vitality, int maxVitality, int coolness, Dictionary<Equipment.Slot, Equipment?> equipment, List<Item> backpack) 
        {
            this.Name = name;
            this.Coordinates = coordinates; 
            this.AvatarId = avatarId;
            this.FGColor = fgColor;
            this.Appearance = appearance;
            this.Strength = strength;
            this.Vitality = vitality;
            this.MaxVitality = maxVitality;
            this.Coolness = coolness;
            this.EquippedItems = equipment;
            this.Backpack = backpack;
        }
        public string MessageUponAttack(int damage) //Returns string for GameMessage. Takes into account the weapon used.
        {
            string weapon = EquippedItems[Slot.Weapon] == null ? "fists" : EquippedItems[Slot.Weapon].Name;
            return "You swing your " + weapon + " for " + damage + " damage.";
        }
        public int RollForAttack()  //Rolls damage. See Monster class.
        {
            int damage = 0;
            Random roll = new Random();
            for (int i = 0; i < Strength; i++)
            {
                damage = roll.Next(1, 4) == 1 ? damage++ : damage;
            }
            return damage;
        }
        public void TakeOff(Equipment.Slot type)  //Removes equipment from slot and places it in the backpack.
        {
            PutInBackpack(EquippedItems[type]);
            AdjustStat(EquippedItems[type].AffectedStat, EquippedItems[type].Modifier*-1);    //Readjusts player stats.
            EquippedItems[type] = null;
        }
        public void PutOn(Equipment equipment)  //Applies equipment to the appropriate equipment slot.
        {
            if (EquippedItems[equipment.Type] != null)  //Removes existing item from slot, if any.
                TakeOff(equipment.Type);
            AdjustStat(equipment.AffectedStat, equipment.Modifier);   //Apply bonus from equipment.
            RemoveFromBackpack(equipment);  
        }
        public void UseConsumable(Consumable consumable)    //Raises relevant stat, turns on timer for consumables which have one.
        {
            AdjustStat(consumable.AffectedStat, consumable.Modifier);
            if (consumable.Duration != null)
                consumable.TimerIsOn = true;
        }
        public void PutInBackpack(Item item)
        {
            Backpack.Add(item);
        }
        public void RemoveFromBackpack(Item item)
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
