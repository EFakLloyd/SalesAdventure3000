using Engine;
using SalesAdventure3000_UI.Controllers;
using SalesAdventure3000_UI.Views.DisplayElements;
using System;
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
                    StatsWindow.Draw(currentSession.CurrentPlayer.GetData());
                AvatarDisplay.Draw(currentSession.Avatars, currentSession.CurrentPlayer.AvatarId, currentSession.CurrentPlayer.Armour, 5); //third param. should be monster.AvatarId
                if (updateMessages)
                    MessageWindow.Draw(currentSession.GameMessages);
                BattleMenuWindow.Draw(currentAction, menuIndex);
                if (updatebackpack)
                    BackpackWindow.Draw(currentAction, currentSession.CurrentPlayer.Backpack, backpackIndex);

                updateStats = updateMessages = updatebackpack = false;

                if (currentAction == Actions.StayOnMap)
                {
                    Console.Clear();
                    return View.Adventure;
                }
                else if (currentAction == Actions.ContinueFight)
                {
                    var input = InGameMenuControl.GetInput(menuIndex, 4);
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
                    var input = InGameMenuControl.GetInput(backpackIndex, currentSession.CurrentPlayer.Backpack.Count);
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
