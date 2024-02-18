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
        public Monster(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int coolness, string attackMessage) :base(name, appearance, fgColor, strength, vitality, coolness) 
        {
            this.AttackMessage = attackMessage;
        }
    }
}
