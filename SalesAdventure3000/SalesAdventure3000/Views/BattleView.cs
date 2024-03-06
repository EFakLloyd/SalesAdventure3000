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
        private static int menuIndex = 0;
        private static int backpackIndex = 0;

        public static View Display(Session currentSession)
        {
            Actions currentAction = Actions.ContinueFight;

            bool updateStats = true;
            bool updateMessages = true;
            bool updatebackpack = true;

            while (true)
            {
                if (updateStats)
                    StatsWindow.DrawPlayerStats(currentSession.CurrentPlayer);
                AvatarDisplay.DrawAvatars(currentSession.Avatars, currentSession.CurrentPlayer.AvatarId, currentSession.CurrentPlayer.Armour, 5); //third param. should be monster.AvatarId
                if (updateMessages)
                    MessageWindow.DrawGameMessages(currentSession.GameMessages);
                BattleMenuWindow.DrawBattleMenu(currentAction, menuIndex);
                if (updatebackpack)
                    BackpackWindow.DrawBackpack(currentAction, currentSession.CurrentPlayer.Backpack, backpackIndex);

                updateStats = updateMessages = updatebackpack = false;

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
        }
    }
}
