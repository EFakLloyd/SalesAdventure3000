using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            this.EquippedItems.Add(EqType.Head, null);
            this.EquippedItems.Add(EqType.Weapon, null);
            this.EquippedItems.Add(EqType.Torso, null);
            this.EquippedItems.Add(EqType.Bling, null);

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
        public void ToggleEquipmentOn(Equipment equipment, bool toggle) //Keff kod. Fundera på det här.
        {
            //if (toggle && this.EquippedItems[equipment.Type] != null)
            //{
            //    this.PutInBackpack(this.EquippedItems[equipment.Type]);
            //    this.EquippedItems[equipment.Type].EquipItem
            //}

                
            //if (toggle)
            //    this.EquippedItems[equipment.Type] = equipment;
            //if (!toggle)
            //    this.EquippedItems[equipment.Type].
        }
        public void PutInBackpack(Item item)
        {
            Backpack.Add(item);
        }
        public void RemoveFromBackpack(int index)
        {
            Backpack.RemoveAt(index);
        }
    }
}
