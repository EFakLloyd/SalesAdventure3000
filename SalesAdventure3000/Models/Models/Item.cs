using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Item : Entity
    {
        public Item(string name, string appearance, ConsoleColor fgColor) : base(name, appearance, fgColor)
        {
            
        }
    }
}
