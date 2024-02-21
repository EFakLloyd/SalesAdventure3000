using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Engine.Models.Item;

namespace Engine.Models
{
    public class Equipment : Item
    {
        public enum EquipmentType
        {
            Head,
            Torso,
            Weapon,
            Bling
        }
        public bool Equipped { get; set; }
        public EquipmentType Type { get; set; }
        public Equipment(string name, string appearance, ConsoleColor fgColor, EquipmentType type, Stat stat, int modifier, string useMessage) : base(name, appearance, fgColor, stat, modifier, useMessage)
        {
            this.Equipped = false;
            this.Type = type;
        }

        public Player EquipItem(Player player)
        {
            int mod = !Equipped ? 1 : -1;
            
            switch (AffectedStat)
            {
                case Stat.Strength:
                    player.Strength += Modifier * mod;
                    break;
                case Stat.Vitality:
                    player.Vitality += Modifier * mod;
                    break;
                case Stat.MaxVitality:
                    player.MaxVitality += Modifier * mod;
                    break;
                case Stat.Armour:
                    player.Armour += Modifier * mod;
                    break;
                case Stat.Coolness:
                    player.Coolness += Modifier * mod;
                    break;
                default:
                    break;
            }
            Equipment? item = !Equipped ? this : null;
            switch (Type)
            {
                case EquipmentType.Head:
                    player.EquippedItems.Head = item;
                    break;
                case EquipmentType.Torso:
                    player.EquippedItems.Torso = item;
                    break;
                case EquipmentType.Weapon:
                    player.EquippedItems.Weapon = item;
                    break;
                case EquipmentType.Bling:
                    player.EquippedItems.Bling = item;
                    break;
            }
            Equipped = !Equipped;
            return player;
        }
    }
}
