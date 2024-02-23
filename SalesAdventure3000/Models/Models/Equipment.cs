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
        public enum EqType
        {
            Head,
            Torso,
            Weapon,
            Bling
        }
        public bool Equipped { get; set; }
        public EqType Type { get; set; }
        public Equipment(string name, string appearance, ConsoleColor fgColor, EqType type, Stat stat, int modifier, string useMessage) : base(name, appearance, fgColor, stat, modifier, useMessage)
        {
            this.Equipped = false;
            this.Type = type;
        }

        public void EquipItem(Player player, int index) //KEFF kod, skriv om Jens
        {
            //Equipped = !Equipped;
            //int upOrDown = Equipped ? 1 : -1;
            //player.RemoveFromBackpack(index);
            //player.AdjustPlayerStat(AffectedStat, Modifier * upOrDown);
            //player.ToggleEquipmentOn(this, Equipped);

        }
    }
}
