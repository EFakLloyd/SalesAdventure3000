using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAdventure3000
{
    public class Item : Entity
    {
        public Item(int Id, int y, int x, string name, char character, ConsoleColor bGColor, ConsoleColor fGColor, List<Item> backpack) : base(Id, y, x, name, character, bGColor, backpack)
        {
        }
    }
}
