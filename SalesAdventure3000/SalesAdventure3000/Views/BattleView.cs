using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using SalesAdventure3000_UI.Views.DisplayElements;
using System;
using static SalesAdventure3000_UI.Views.ViewEnums;

namespace SalesAdventure3000_UI.Views
{
    public class BattleView
    {
        public static View BattleDisplay(Session currentSession)
        {
            Session.CombatAction combatAction = Session.CombatAction.Flee;
            Actions currentAction = Actions.ContinueFight;
            int menuIndex = 0;
            int selectedBackpackIndex = 0;
            (bool playerIsAlive, bool monsterIsDead) combatResult = (true, false);
            Updater updater = new Updater(currentSession, ref selectedBackpackIndex, ref menuIndex, ref currentAction);

            Console.Clear();
            AvatarDisplay.Draw(currentSession.Avatars, currentSession.CurrentPlayer.AvatarId, currentSession.CurrentPlayer.Armour,
                        currentSession.CurrentMonster.GetVisuals().avatarId, currentSession.CurrentMonster.GetVisuals().fgColor);

            while (true)
            {
                bool executeCombatAction = false;

                updater.Draw(new Element[] { Element.Stats, Element.Messages, Element.BattleMenu, Element.Backpack });

                if (currentAction == Actions.ContinueFight)
                    selectBattleMenuAction();
                else if (currentAction == Actions.OpenBackpack)
                    openBackpack();

                if (executeCombatAction)
                    combatResult = currentSession.HandleCombat(combatAction);
                if (!combatResult.playerIsAlive)
                {
                    DeathWindow.Draw();
                    return View.Start;
                }
                if (currentAction == Actions.StayOnMap || combatResult.monsterIsDead)
                {
                    currentAction = Actions.StayOnMap;
                    return View.Adventure;
                }

                void selectBattleMenuAction()
                {
                    var input = InGameMenuControl.GetInput(menuIndex, 4);
                    menuIndex = input.selectedIndex;

                    if (input.confirmedChoice == true)
                    {
                        executeCombatAction = true;
                        switch (input.selectedIndex)
                        {
                            case 0:
                                combatAction = Session.CombatAction.Attack;
                                break;
                            case 1:
                                combatAction = Session.CombatAction.RecklessAttack;
                                break;
                            case 2:
                                combatAction = Session.CombatAction.UseItem;
                                currentAction = Actions.OpenBackpack;
                                executeCombatAction = false;
                                break;
                            case 3:
                                combatAction = Session.CombatAction.Flee;
                                currentAction = Actions.StayOnMap;
                                break;
                        }
                    }
                }
                void openBackpack()
                {
                    var input = InGameMenuControl.GetInput(selectedBackpackIndex, currentSession.CurrentPlayer.Backpack.Count);

                    selectedBackpackIndex = input.selectedIndex;

                    if (input.confirmedChoice == true )
                    {
                        currentSession.UseItem(currentSession.CurrentPlayer.Backpack[selectedBackpackIndex]);
                        if (currentSession.CurrentPlayer.Backpack[selectedBackpackIndex] is not Equipment) 
                            executeCombatAction = true;
                        currentAction = Actions.ContinueFight;
                    }
                    else if (!input.stayInLoop)
                    {
                        currentAction = Actions.ContinueFight;
                    }
                }
            }
        }
    }
}
