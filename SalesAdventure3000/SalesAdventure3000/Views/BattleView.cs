using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using SalesAdventure3000_UI.Views.DisplayElements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static SalesAdventure3000_UI.Views.AdventureView;
using static SalesAdventure3000_UI.Views.ViewType;

namespace SalesAdventure3000_UI.Views
{
    public class BattleView
    {
        //internal enum Actions
        //{
        //    ReturnToMap,
        //    ContinueFight,
        //    OpenBackpack
        //}

        private static int menuIndex = 0;
        private static int backpackIndex = 0;

        public static View Display(Session currentSession)
        {
            int width = 42;
            int height = 15;
            Actions currentAction = Actions.ContinueFight;

            bool updateStats = true;
            bool drawAvatars = true;
            bool updateMessages = true;
            bool drawBattleMenu = true;
            bool updatebackpack = true;

            while (true)
            {
                if (updateStats)
                    StatsWindow.DrawPlayerStats(currentSession.CurrentPlayer);
                AvatarDisplay.DrawAvatars(currentSession.Avatars, currentSession.CurrentPlayer.AvatarId, 5); //third param. should be monster.AvatarId
                if (updateMessages)
                    MessageWindow.DrawGameMessages(currentSession.GameMessages);
                BattleMenuWindow.DrawBattleMenu(currentAction, menuIndex);
                if (updatebackpack)
                    BackpackWindow.DrawBackpack(currentAction, currentSession.CurrentPlayer.Backpack, backpackIndex);

                updateStats = updateMessages = updatebackpack = false;



                //DrawPlayerStats();
                //DrawAvatars();
                //DrawInfoWindow();
                //DrawBattleMenu();
                //DrawBackpack();

                if (currentAction == Actions.StayOnMap)
                {
                    Console.Clear();
                    return View.Adventure;
                }
                else if (currentAction == Actions.ContinueFight)
                {
                    var input = PlayerInventoryControl.GetInput(menuIndex, 4);
                    menuIndex = input.selectedIndex;
                    if (input.confirmedChoice == true)
                    {
                        switch (input.selectedIndex)
                        {
                            case 0:
                                //Attack();
                                updateStats = updateMessages = true;
                                break;
                            case 1:
                                //RecklessAttack();
                                break;
                            case 2:
                                currentAction = Actions.OpenBackpack;
                                updatebackpack = true;
                                break;
                            case 3:
                                //Flee();
                                currentAction = Actions.StayOnMap;
                                break;
                        }
                    }
                }
                else if (currentAction == Actions.OpenBackpack) 
                {
                    var input = PlayerInventoryControl.GetInput(backpackIndex, currentSession.CurrentPlayer.Backpack.Count);
                    backpackIndex = input.selectedIndex;
                    if (input.confirmedChoice == true)
                    {
                        currentSession.UseItem(currentSession.CurrentPlayer.Backpack[backpackIndex]);
                        updateMessages = updateStats = true;
                    }
                    currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;
                    updatebackpack = true;
                }
            }

            //    void DrawPlayerStats()
            //    {
            //        Console.Write($"╔═{currentSession.CurrentPlayer.Name}".PadRight(width * 2 - 1, '═') + "╗\n" +
            //            $"║ Strength: {currentSession.CurrentPlayer.Strength}".PadRight(width - 1, ' ') + $"Vitality: {currentSession.CurrentPlayer.Vitality}".PadRight(width, ' ') + "║\n" +
            //            $"║ Coolness: {currentSession.CurrentPlayer.Coolness}".PadRight(width - 1, ' ') + $"Armour: {currentSession.CurrentPlayer.Armour}".PadRight(width, ' ') + "║\n" +
            //            "╚".PadRight(width * 2 - 1, '═') + "╝\n");
            //    }

            //    void DrawAvatars()
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            Console.ForegroundColor = ConsoleColor.White;
            //            Console.Write(currentSession.Avatars[currentSession.CurrentPlayer.AvatarId][i].PadLeft(37) + "".PadRight(10));
            //            Console.ForegroundColor = ConsoleColor.Red;
            //            Console.Write(currentSession.Avatars[currentSession.CurrentPlayer.AvatarId][i].PadRight(37 / 2) + "\n");
            //            //currentSession.Avatars[Monster.AvatarId][i]
            //        }
            //    }

            //    void DrawInfoWindow()
            //    {
            //        Console.WriteLine("╔".PadRight(width * 2 - 1, '═') + "╗");

            //        for (int i = 3; i > 0; i--)
            //        {
            //            if (i <= currentSession.GameMessages.Count())
            //            {
            //                Console.Write($"║ ");
            //                Console.ForegroundColor = i == 1 ? ConsoleColor.Cyan : ConsoleColor.Gray;
            //                Console.Write($"{currentSession.GameMessages[currentSession.GameMessages.Count() - i]}".PadRight(width * 2 - 3, ' '));
            //                Console.ResetColor();
            //                Console.Write("║\n");
            //            }
            //        }
            //        Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            //    }

            //    void DrawBattleMenu()
            //    {
            //        Console.ForegroundColor = currentAction == Actions.ContinueFight ? ConsoleColor.Cyan : ConsoleColor.Gray;
            //        Console.WriteLine("╔".PadRight(width * 2 - 1, '═') + "╗");
            //        for (int i = 0; i < battleOptions.Length; i++)
            //        {
            //            string[] selection = i == backpackIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
            //            if (i % 2 == 0)
            //                Console.Write($"║ {selection[0]}{battleOptions[i]}{selection[1]}".PadRight(width - 1, ' '));
            //            else
            //                Console.Write($"{selection[0]}{battleOptions[i]}{selection[1]}".PadRight(width, ' ') + "║\n");
            //            if (i % 2 == 0 && i == battleOptions.Length - 1)
            //                Console.Write("".PadRight(width, ' ') + "║\n");
            //        }
            //        Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            //        Console.ResetColor();
            //    }
            //    void DrawBackpack()
            //    {
            //        Console.ForegroundColor = currentAction == Actions.OpenBackpack ? ConsoleColor.Cyan : ConsoleColor.Gray;
            //        Console.WriteLine("╔═BACKPACK═[B]".PadRight(width * 2 - 1, '═') + "╗");
            //        for (int i = 0; i < currentSession.CurrentPlayer.Backpack.Count; i++)
            //        {
            //            string[] selection = i == backpackIndex ? new string[] { "[", "]" } : new string[] { " ", " " };
            //            if (i % 2 == 0)
            //                Console.Write($"║ {selection[0]}{currentSession.CurrentPlayer.Backpack[i].GetName()}{selection[1]}".PadRight(width - 1, ' '));
            //            else
            //                Console.Write($"{selection[0]}{currentSession.CurrentPlayer.Backpack[i].GetName()}{selection[1]}".PadRight(width, ' ') + "║\n");
            //            if (i % 2 == 0 && i == currentSession.CurrentPlayer.Backpack.Count - 1)
            //                Console.Write("".PadRight(width, ' ') + "║\n");
            //        }
            //        Console.WriteLine("╚".PadRight(width * 2 - 1, '═') + "╝");
            //        Console.ResetColor();
            //    }
        }
    }
}
