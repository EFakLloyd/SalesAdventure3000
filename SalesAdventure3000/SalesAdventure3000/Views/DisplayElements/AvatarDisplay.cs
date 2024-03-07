using System;
using System.Collections.Generic;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal class AvatarDisplay
    {
        public static void Draw(List<string[]> avatars, int playerAvatarId, int playerArmour, int monsterAvatarId)
        {
            playerAvatarId = playerArmour >= 10 ? playerAvatarId + 1 : playerAvatarId;
            int padWidth = (GameDimensions.Width * 2 - (avatars[playerAvatarId][6].Length + avatars[monsterAvatarId][6].Length + 10)) / 2;
            Console.SetCursorPosition(0, 4);
            Console.WriteLine("\n\n");
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("".PadRight(padWidth) + avatars[playerAvatarId][i] + "".PadRight(10));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(avatars[monsterAvatarId][i].PadRight(padWidth) + "\n");
            }
            Console.WriteLine("\n\n\n");
            Console.ResetColor();
        }
    }
}
