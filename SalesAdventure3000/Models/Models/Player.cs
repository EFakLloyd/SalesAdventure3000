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
        public int MaxVitality { get; set; }
        public int[] Coordinate { get; set; }
        public int Armour { get; set; }
        public Dictionary<Equipment.EqType,Equipment?> EquippedItems { get; set; }
        public Player(string name, int[] coordinate) : base(name, "@", ConsoleColor.DarkMagenta, 15, 15, 5)
        {
            this.MaxVitality = 25;
            this.Name = name;
            this.Coordinate = coordinate;
            this.Armour = 0;
            this.EquippedItems = new Dictionary<Equipment.EqType, Equipment?>();
            this.EquippedItems.Add(EqType.Head, null);
            this.EquippedItems.Add(EqType.Weapon, null);
            this.EquippedItems.Add(EqType.Torso, null);
            this.EquippedItems.Add(EqType.Bling, null);
            this.Backpack = new List<Item>();
            Backpack.Add(ItemFactory.CreateItem(2006));
        }
        public string MessageUponAttack(int damage)
        {
            string weapon = EquippedItems[EqType.Weapon] == null ? "fists" : EquippedItems[EqType.Weapon].Name;
            return "You swing your " + weapon + " for " + damage + " damage.";
        }
        public int RollForAttack(int playerArmour)
        {
            int damage = 0;
            Random roll = new Random();
            for (int i = 0; i < Strength; i++)
            {
                damage = roll.Next(1, 4) == 1 ? damage++ : damage;
            }
            return damage;
        }
        public void AdjustPlayerStat(Item.Stat affectedStat, int modifier) // Justerar en av spelarens stats
        {
            switch (affectedStat)
            {
                case Stat.Strength:
                    Strength += modifier;
                    break;
                case Stat.Vitality:
                    Vitality = Math.Min(Vitality + modifier, MaxVitality);
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
        public void TakeOff(Equipment.EqType type)
        {
            PutInBackpack(EquippedItems[type]);
            AdjustPlayerStat(EquippedItems[type].AffectedStat, EquippedItems[type].Modifier*-1);
            EquippedItems[type] = null;
        }
        public void PutOn(Equipment equipment)
        {
            if (EquippedItems[equipment.Type] != null)
                TakeOff(equipment.Type);
            AdjustPlayerStat(equipment.AffectedStat, equipment.Modifier);
            RemoveFromBackpack(equipment);
        }
        public void UseConsumable(Consumable consumable)
        {
            AdjustPlayerStat(consumable.AffectedStat, consumable.Modifier);
            if (consumable.Duration != null)
                consumable.Timer = true;
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
