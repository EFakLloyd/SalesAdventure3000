using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using SalesAdventure3000_UI.Views.DisplayElements;
using System;
using System.Threading;
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
                AvatarDisplay.Draw(currentSession.Avatars, currentSession.CurrentPlayer.AvatarId, currentSession.CurrentPlayer.Armour, 
                    currentSession.CurrentMonster.GetVisuals().avatarId, currentSession.CurrentMonster.GetVisuals().fgColor);
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
                    (bool playerIsAlive, bool monsterIsDead) combatResult = (true, false);

                    if (input.confirmedChoice == true)
                    {
                        switch (input.selectedIndex)
                        {
                            case 0:
                                combatResult = currentSession.HandleCombat(Session.CombatAction.Attack);
                                updateStats = updateMessages = true;
                                break;
                            case 1:
                                combatResult = currentSession.HandleCombat(Session.CombatAction.RecklessAttack);
                                break;
                            case 2:
                                currentAction = Actions.OpenBackpack;
                                combatResult = currentSession.HandleCombat(Session.CombatAction.UseItem);
                                updateStats = updateMessages = updatebackpack = true;
                                break;
                            case 3:
                                combatResult = currentSession.HandleCombat(Session.CombatAction.Flee);
                                currentAction = Actions.StayOnMap;
                                break;
                        }
                        Thread.Sleep(1000);
                        if (!combatResult.playerIsAlive)
                        {
                            DeathWIndow.Draw();
                            return View.Start;
                        }
                        if (combatResult.monsterIsDead)
                            currentAction = Actions.StayOnMap;
                    }
                }
                else if (currentAction == Actions.OpenBackpack)
                {
                    var input = InGameMenuControl.GetInput(backpackIndex, currentSession.CurrentPlayer.Backpack.Count);
                    backpackIndex = input.selectedIndex;
                    if (input.confirmedChoice == true)
                    {
                        if (currentSession.CurrentPlayer.Backpack[backpackIndex] is Equipment)
                            currentSession.GameMessages.Add("Cannot use equipment during battle");
                        else
                            currentSession.UseItem(currentSession.CurrentPlayer.Backpack[backpackIndex]);
                            updateMessages = updateStats = true;
                    }
                    currentAction = input.stayInLoop ? currentAction : Actions.ContinueFight;
                    updatebackpack = true;
                }
            }
        }
    }
}
