using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Engine.Models.Equipment;
using static Engine.Models.Item;

namespace Engine.Models
{
    public class Player : Creature
    {
        public int MaxVitality { get; set; }    //Upper limit of player Vit.
        public int[] Coordinate { get; set; }   //Coordinates saved separately for ease of access.
        public int Armour { get; set; }         //Only player objects have armour.
        public Dictionary<Equipment.EqType,Equipment?> EquippedItems { get; set; }  //Dict with a key for each of the equipment slots. Null = empty.
                                                                                    //Dict only takes unique keys, so we cannot wear more than one item per slot.
        public Player(string name, int[] coordinate) : base(name, "@", ConsoleColor.DarkMagenta, 15, 15, 5)
        {
            this.MaxVitality = 25;
            this.Name = name;
            this.Coordinate = coordinate;
            this.Armour = 0;
            this.EquippedItems = new Dictionary<Equipment.EqType, Equipment?>();
            this.EquippedItems.Add(EqType.Head, EquipmentFactory.CreateEquipment(2000));
            this.EquippedItems.Add(EqType.Weapon, EquipmentFactory.CreateEquipment(2002));
            this.EquippedItems.Add(EqType.Torso, EquipmentFactory.CreateEquipment(2001));
            this.EquippedItems.Add(EqType.Bling, EquipmentFactory.CreateEquipment(2005));
            this.Backpack = new List<Item>();
            Backpack.Add(ConsumableFactory.CreateConsumable(3000));
            Backpack.Add(ConsumableFactory.CreateConsumable(3002));
            Backpack.Add(ConsumableFactory.CreateConsumable(3003));
        }
        public string MessageUponAttack(int damage) //Returns string for GameMessage. Takes into account the weapon used.
        {
            string weapon = EquippedItems[EqType.Weapon] == null ? "fists" : EquippedItems[EqType.Weapon].Name;
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
        public void TakeOff(Equipment.EqType type)  //Removes equipment from slot and places it in the backpack.
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
        public void UseConsumable(Consumable consumable)    //
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
