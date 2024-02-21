using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : Creature
    {
        public int MaxVitality { get; set; }
        public int[] Coordinate { get; set; }
        public int Armour { get; set; }
        public EquipmentSlots EquippedItems { get; set; }
        public Player(string name, int[] coordinate) : base(name, "@", ConsoleColor.DarkMagenta, 15, 15, 5)
        {
            this.MaxVitality = 25;
            this.Name = name;
            this.EquippedItems = new EquipmentSlots();
            this.Coordinate = coordinate;
            this.Armour = 0;
        }
        public string MessageUponAttack(int damage)
        {
            string weapon = EquippedItems.Weapon == null ? "fists" : EquippedItems.Weapon.Name;
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
    }
}
