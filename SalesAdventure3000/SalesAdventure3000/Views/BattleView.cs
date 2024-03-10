using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using SalesAdventure3000_UI.Views.DisplayElements;
using System;
using System.Collections.Generic;
using static SalesAdventure3000_UI.Views.AdventureView;
using static SalesAdventure3000_UI.Views.ViewType;

namespace SalesAdventure3000_UI.Views
{
    public class BattleView
    {
        public static View Display(Session currentSession)
        {
            Actions currentAction = Actions.ContinueFight;
            int menuIndex = 0;
            int backpackIndex = 0;
            (bool playerIsAlive, bool monsterIsDead) combatResult = (true, false);
            List<Item> consumablesInBackpack = new List<Item>();
            foreach (Item item in currentSession.CurrentPlayer.Backpack)
            {
                if (item is Consumable)
                    consumablesInBackpack.Add(item);
            }

            StatsWindow.Draw(currentSession.CurrentPlayer.GetData());
            AvatarDisplay.Draw(currentSession.Avatars, currentSession.CurrentPlayer.AvatarId, currentSession.CurrentPlayer.Armour,
                        currentSession.CurrentMonster.GetVisuals().avatarId, currentSession.CurrentMonster.GetVisuals().fgColor);
            MessageWindow.Draw(currentSession.GameMessages);
            BattleMenuWindow.Draw(currentAction, menuIndex);
            BackpackWindow.Draw(currentAction, consumablesInBackpack, backpackIndex);

            while (true)
            {
                if (!combatResult.playerIsAlive)
                {
                    DeathWindow.Draw();
                    return View.Start;
                }
                if (currentAction == Actions.StayOnMap || combatResult.monsterIsDead)
                {
                    Console.Clear();
                    return View.Adventure;
                }
                selectBattleMenuAction();

                void selectBattleMenuAction()
                {
                    while (true)
                    {
                        BattleMenuWindow.Draw(currentAction, menuIndex);
                        var input = InGameMenuControl.GetInput(menuIndex, 4);
                        menuIndex = input.selectedIndex;

                        if (input.confirmedChoice == true)
                        {
                            switch (input.selectedIndex)
                            {
                                case 0:
                                    combatResult = currentSession.HandleCombat(Session.CombatAction.Attack);
                                    break;
                                case 1:
                                    combatResult = currentSession.HandleCombat(Session.CombatAction.RecklessAttack);
                                    break;
                                case 2:
                                    currentAction = Actions.OpenBackpack;
                                    if (playerSelectsItemFromBackpack())
                                        combatResult = currentSession.HandleCombat(Session.CombatAction.UseItem);
                                    break;
                                case 3:
                                    combatResult = currentSession.HandleCombat(Session.CombatAction.Flee);
                                    currentAction = Actions.StayOnMap;
                                    break;
                            }
                            if (!combatResult.playerIsAlive || combatResult.monsterIsDead || currentAction == Actions.StayOnMap)
                                break;

                            StatsWindow.Draw(currentSession.CurrentPlayer.GetData());
                            MessageWindow.Draw(currentSession.GameMessages);
                            BattleMenuWindow.Draw(currentAction, menuIndex);
                            BackpackWindow.Draw(currentAction, consumablesInBackpack, backpackIndex);
                        }
                    }
                }

                bool playerSelectsItemFromBackpack()
                {
                    BattleMenuWindow.Draw(currentAction, menuIndex);

                    while (currentAction == Actions.OpenBackpack)
                    {
                        BackpackWindow.Draw(currentAction, consumablesInBackpack, backpackIndex);

                        var input = InGameMenuControl.GetInput(backpackIndex, consumablesInBackpack.Count);

                        backpackIndex = input.selectedIndex;
                        if (input.confirmedChoice == true)
                        {
                            currentSession.UseItem(consumablesInBackpack[backpackIndex]);
                            consumablesInBackpack.Clear();
                            foreach (Item item in currentSession.CurrentPlayer.Backpack)
                            {
                                if (item is Consumable)
                                    consumablesInBackpack.Add(item);
                            }
                            currentAction = Actions.ContinueFight;
                            return true;
                        }
                        if (!input.stayInLoop)
                        {
                            currentAction = Actions.ContinueFight;
                            return false;
                        }
                    }
                    return false;
                }
            }
        }
    }
}
