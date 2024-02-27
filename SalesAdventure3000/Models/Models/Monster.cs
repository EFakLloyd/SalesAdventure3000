using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster : Creature
    {
        public string AttackMessage { get; set; }
        public Monster(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int coolness, string attackMessage, int id) :base(name, appearance, fgColor, strength, vitality, coolness, id) 
        {
            this.AttackMessage = attackMessage;
        }
        public string MessageUponAttack(int damage) //Returns message for either miss or hit to the player.
        {
            if (damage == 0)
                return "The " + Name + " misses you, barely.";
            else
                return AttackMessage + damage + " damage.";
        }
        public int RollForAttack(int playerArmour)  
        {
            int damage = 0;
            Random roll = new Random();
            for (int i = 0; i < Strength; i++)
            {
                damage = roll.Next(1,4) == 1 ? damage++ : damage; //Every point in strength gives a 1/3 chance to do 1 damage. 
            }
            return Math.Max(damage - playerArmour, 0); //Adjust for player armour. Return no less than 0.
        }

    }
}
