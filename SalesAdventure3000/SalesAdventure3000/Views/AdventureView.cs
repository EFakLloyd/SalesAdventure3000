using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using SalesAdventure3000_UI.Views.DisplayElements;
using System;
using static SalesAdventure3000_UI.Views.ViewEnums;

namespace SalesAdventure3000_UI.Views
{
    public class AdventureView
    {
        private static int selectedBackpackIndex = 0;
        private static int selectedEquipmentIndex = 0;

        public static View AdventureDisplay(Session currentSession) //Takes input and handles navigation on map.
        {
            Actions currentAction = Actions.NavigateMap;            //Standard action is to navigate the map.
            Updater updater = new Updater(currentSession, ref selectedBackpackIndex, ref selectedEquipmentIndex, ref currentAction);    //Updater object is created view.

            Console.Clear();
            WorldDisplay.DrawWorld(currentSession.CurrentWorld.Map);

            while (true)
            {
                updater.Draw(new Element[] { Element.Stats, Element.WorldTiles, Element.Messages, Element.Equipment, Element.Backpack });

                if (currentAction == Actions.NavigateMap)
                    mapNavigation();
                else if (currentAction == Actions.OpenEquipment)
                    openEquipment();
                else if (currentAction == Actions.OpenBackpack)
                    openBackpack();
                else if (currentAction == Actions.GoToMenu)     //The player opted to go back to the main menu
                    return View.Start;
                else if (currentAction == Actions.Fight)
                    return View.Battle;
            }

            #region Helper Methods
            void mapNavigation()
            {
                currentSession.CheckTimersAndResolve();     //Every move attempt on map counts as a "tick" for items on countdown.

                var input = MapControl.GetInput(currentSession.CurrentPlayer.Coordinates);  //Controller returns tuple of multiple values.
                currentAction = input.currentAction;

                if (currentSession.AnyMonsterAt(input.x, input.y))
                    currentAction = Actions.Fight;
                else
                    currentSession.MovePlayer(input.x, input.y);    //Attempts to move player into tile.
            }
            void openBackpack()
            {
                var input = InGameMenuControl.GetInput(selectedBackpackIndex, currentSession.CurrentPlayer.Backpack.Count);
                selectedBackpackIndex = input.selectedIndex;        //Sets the currently selected item in backpack

                if (input.confirmedChoice == true)                  //Player decided to use an item
                {
                    currentSession.UseItem(currentSession.CurrentPlayer.Backpack[selectedBackpackIndex]);
                    selectedBackpackIndex = selectedBackpackIndex == currentSession.CurrentPlayer.Backpack.Count ? selectedBackpackIndex - 1 : selectedBackpackIndex;   //Adjust index to avoid exception.
                }
                if (!input.stayInLoop)                              //Player decided to leave the backpack menu.
                    currentAction = Actions.NavigateMap;
            }
            void openEquipment()
            {
                var input = InGameMenuControl.GetInput(selectedEquipmentIndex, currentSession.CurrentPlayer.EquippedItems.Count);
                selectedEquipmentIndex = input.selectedIndex;

                if (input.confirmedChoice == true && currentSession.CurrentPlayer.GetEquippedItems()[selectedEquipmentIndex] != null)   //Check for null to avoid exception.
                    currentSession.UseItem(currentSession.CurrentPlayer.GetEquippedItems()[selectedEquipmentIndex], removeEquipment: true);     //UseItem called from here removes equipment.
                if (!input.stayInLoop)
                    currentAction = Actions.NavigateMap;
            }
            #endregion
        }
    }
}
