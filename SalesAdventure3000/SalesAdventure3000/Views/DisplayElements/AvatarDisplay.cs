using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal class AvatarDisplay
    {
        public static void DrawAvatars(List<string[]> avatars, int playerAvatarId, int playerArmour, int monsterAvatarId)
        {
            playerAvatarId = playerArmour >= 10 ? playerAvatarId + 1 : playerAvatarId;
            Console.SetCursorPosition(0, 4);
            Console.WriteLine("\n\n");
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(avatars[playerAvatarId][i].PadLeft(37) + "".PadRight(10));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(avatars[monsterAvatarId][i].PadRight(37 / 2) + "\n");
            }
            Console.WriteLine("\n\n\n");
            Console.ResetColor();
        }
    }
}
