using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Engine.Models.Equipment;
using static Engine.Models.Item;

namespace Engine.Models
{
    public class Player : Creature
    {
        public int MaxVitality { get; set; }    //Upper limit of player Vit.
        public int Armour { get; set; }         //Only player objects have armour.
        public Dictionary<Equipment.Slot,Equipment?> EquippedItems { get; set; }  //Dict with a key for each of the equipment slots. Null = empty.
                                                                                    //Dict only takes unique keys, so we cannot wear more than one item per slot.
        public Player()
        {
            this.EquippedItems = new Dictionary<Equipment.Slot, Equipment?>();
        }
        public Player(string name, int[] coordinates, int avatar) : base(name, "@", ConsoleColor.DarkMagenta, 15, 15, 5, 0000, avatar)
        {
            this.MaxVitality = 25;
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
        public void AdjustPlayerStat(Item.Stat affectedStat, int modifier) //Adjusts one of the players stats by the supplied modifer.
        {
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
        public void TakeOff(Equipment.Slot type)  //Removes equipment from slot and places it in the backpack.
        {
            PutInBackpack(EquippedItems[type]);
            AdjustPlayerStat(EquippedItems[type].AffectedStat, EquippedItems[type].Modifier*-1);    //Readjusts player stats.
            EquippedItems[type] = null;
        }
        public void PutOn(Equipment equipment)  //Applies equipment to the appropriate equipment slot.
        {
            if (EquippedItems[equipment.Type] != null)  //Removes existing item from slot, if any.
                TakeOff(equipment.Type);
            AdjustPlayerStat(equipment.AffectedStat, equipment.Modifier);   //Apply bonus from equipment.
            RemoveFromBackpack(equipment);  
        }
        public void UseConsumable(Consumable consumable)    //Raises relevant stat, turns on timer for consumables which have one.
        {
            AdjustPlayerStat(consumable.AffectedStat, consumable.Modifier);
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
    }
}
