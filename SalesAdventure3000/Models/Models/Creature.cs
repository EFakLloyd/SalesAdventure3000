using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Engine.Models.Equipment;
using static Engine.Models.Item;

namespace Engine.Models
{
    public class Creature : Entity
    {
        protected int Strength;
        public int Vitality { get; private set; }
        protected int MaxVitality;
        protected int Coolness;   
        public int AvatarId { get; private set; }
        public List<Item> Backpack { get; private set; }   //Monsters may also want to carry items as loot for the player.
        public int Armour { get; private set; }

        public Creature() { }
        public Creature(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int coolness, int avatarId, int armour, int id ) : base(name, appearance, fgColor, id)
        {
            this.Strength = strength;
            this.Vitality = vitality;
            this.MaxVitality = vitality;
            this.Coolness = coolness;
            this.AvatarId = avatarId;
            this.Armour = armour;
            this.Backpack = new List<Item>();
        }
        public Creature(string name, string appearance, ConsoleColor fgColor, int strength, int vitality, int coolness, int avatarId, int armour, List<Item> backpack, int id) : base(name, appearance, fgColor, id)
        {
            this.Strength = strength;
            this.Vitality = vitality;
            this.MaxVitality = vitality;
            this.Coolness = coolness;
            this.AvatarId = avatarId;
            this.Armour = armour;
            this.Backpack = backpack;
        }
        public void AdjustStat(Item.Stat affectedStat, int modifier, Adjustment mod) //Adjusts one of the stats by the supplied modifer.
        {
            modifier *= mod == Adjustment.Up ? 1 : -1;

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
        public (string message, bool isDead) Attack(Creature opponent)
        {
            int damage = 0;
            Random roll = new Random();
            for (int i = 0; i < Strength; i++)
            {
                damage = roll.Next(1, 4) == 1 ? damage++ : damage; //Every point in strength gives a 1/3 chance to do 1 damage
            }
            damage = Math.Max(damage - opponent.Armour, 0); //Adjust for armour
            if (damage > 0)
                opponent.AdjustStat(Stat.Vitality, damage, Adjustment.Down);
            return (MessageUponAttack(damage), opponent.IsDead());
        }
        //public int RollForAttack(Creature opponent)
        //{
        //    int damage = 0;
        //    Random roll = new Random();
        //    for (int i = 0; i < Strength; i++)
        //    {
        //        damage = roll.Next(1, 4) == 1 ? damage++ : damage; //Every point in strength gives a 1/3 chance to do 1 damage. 
        //    }
        //    return Math.Max(damage - opponent.Armour, 0); //Adjust for player armour. Return no less than 0.
        //}
        protected virtual string MessageUponAttack(int damage) //Returns string for GameMessage. Takes into account the weapon used.
        {
            return "";
        }
        protected bool IsDead()
        {
            return Vitality > 0 ? false : true;
        }
    }
}
