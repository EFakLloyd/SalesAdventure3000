using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using SalesAdventure3000_UI.Views.DisplayElements;
using System;
using static Engine.Session;
using static SalesAdventure3000_UI.Views.ViewEnums;

namespace SalesAdventure3000_UI.Views
{
    public class BattleView
    {
        public static View BattleDisplay(Session currentSession)    //Takes input and calls methods for resolving fightning.
        {
            CombatAction combatAction = CombatAction.Flee;  //By default the player will try to flee.
            Actions currentAction = Actions.Fight;          //By default, we want to go into the battle menu.
            int menuIndex = 0;
            int selectedBackpackIndex = 0;
            (bool playerIsAlive, bool monsterIsDead) combatResult = (true, false);      //Informs the view what to display, based on combat results.
            Updater updater = new Updater(currentSession, ref selectedBackpackIndex, ref menuIndex, ref currentAction); //Updater object is created view.

            Console.Clear();
            AvatarDisplay.Draw(currentSession.Avatars, currentSession.CurrentPlayer.AvatarId, currentSession.CurrentPlayer.Armour,
                        currentSession.CurrentMonster.GetVisuals().avatarId, currentSession.CurrentMonster.GetVisuals().fgColor);

            while (true)
            {
                bool executeCombatAction = false;                                       //True means that some kind of combat rolls will be done.

                updater.Draw(new Element[] { Element.Stats, Element.Messages, Element.BattleMenu, Element.Backpack });

                if (currentAction == Actions.Fight)                                     //These If cases determines which menu is active.
                    selectBattleMenuAction();
                else if (currentAction == Actions.OpenBackpack)
                    openBackpack();

                if (executeCombatAction)                
                    combatResult = currentSession.HandleCombat(combatAction);           //Calls HandleCombat with appropriate action.
                if (!combatResult.playerIsAlive)                                        //Player is dead. Go to game over screen :(...
                {
                    DeathWindow.Draw();
                    return View.Start;
                }
                if (currentAction == Actions.NavigateMap || combatResult.monsterIsDead) //Monster is dead, or player ran away. Go back to map.
                {
                    currentAction = Actions.NavigateMap;
                    return View.Adventure;
                }

                #region Helper Methods
                void selectBattleMenuAction()
                {
                    var input = InGameMenuControl.GetInput(menuIndex, 4);
                    menuIndex = input.selectedIndex;

                    if (input.confirmedChoice == true)
                    {
                        executeCombatAction = true;     //The player decided on an action, which we set as combatAction.
                        switch (input.selectedIndex)
                        {
                            case 0:
                                combatAction = CombatAction.Attack;
                                break;
                            case 1:
                                combatAction = CombatAction.RecklessAttack;
                                break;
                            case 2:
                                combatAction = CombatAction.UseItem;
                                currentAction = Actions.OpenBackpack;
                                executeCombatAction = false;
                                break;
                            case 3:
                                combatAction = CombatAction.Flee;
                                currentAction = Actions.NavigateMap;
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
                        bool useIsAllowed = (currentSession.CurrentPlayer.Backpack[selectedBackpackIndex] is not Equipment);    //Equipment may not be used in battle.
                        currentSession.UseItem(currentSession.CurrentPlayer.Backpack[selectedBackpackIndex], calledFromBattleView: true);   //UseItem is called. We inform that the call is from BattleView.
                        if (useIsAllowed)
                            executeCombatAction = true;
                        currentAction = Actions.Fight;
                    }
                    else if (!input.stayInLoop)     //Player decided not to use an item.
                    {
                        currentAction = Actions.Fight;
                    }
                }
                #endregion
            }
        }
    }
}
