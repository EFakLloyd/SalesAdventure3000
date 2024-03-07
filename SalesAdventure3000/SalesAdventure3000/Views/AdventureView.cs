using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Controllers;
using SalesAdventure3000_UI.Views.DisplayElements;
using System;
using static SalesAdventure3000_UI.Views.ViewType;

namespace SalesAdventure3000_UI.Views
{
    public class AdventureView
    {
        public enum Actions
        {
            StayOnMap,
            OpenEquipment,
            OpenBackpack,
            GoToMenu,
            ContinueFight
        }

        private static int selectedBackpackIndex = 0;
        private static int selectedEquipmentIndex = 0;

        public static View Display(Session currentSession)  //
        {
            Position oldCoordinates = currentSession.CurrentPlayer.Coordinates;
            Actions currentAction = Actions.StayOnMap;

            //These bools are used to keep track on which view elements should be redrawn. On view entry all is drawn, therefore set to true

            bool updateStats = true;
            bool updateWholeMap = true;
            bool updateMessages = true;
            bool updateEquipment = true;
            bool updatebackpack = true;

            while (true)
            {
                updateElements();
                getInput();
                if (currentAction == Actions.GoToMenu)     //The player opted to go back to the main menu
                {
                    Console.Clear();
                    return View.Start;
                }
            }

            void updateElements()
            {
                /*The block below checks the update bools and describes which elements should be redrawn, as well as other instructions*/

                //Handles updates to the map
                if (updateWholeMap)     //This will probably only happen on first draw of the map
                    WorldDisplay.DrawWorld(currentSession.CurrentWorld.Map);
                else if (currentAction == Actions.StayOnMap)    //StayOnMap means that the player moved (or tried to move) on the map
                    WorldDisplay.DrawTiles(currentSession.CurrentWorld.Map, oldCoordinates, currentSession.CurrentPlayer.Coordinates); //Supplies coordinates to be redrawn.

                //Non interactable windows
                if (updateStats)
                    StatsWindow.Draw(currentSession.CurrentPlayer.GetData());
                if (updateMessages)
                    MessageWindow.Draw(currentSession.GameMessages);

                //Player containers need more info to be drawn correctly. They can be affected directly or indirectly, and both need to trigger a redraw
                if (updateEquipment || currentAction == Actions.OpenEquipment)  //If currentAction is set the window is drawn differently.
                    EquipmentWindow.Draw(currentAction, currentSession.CurrentPlayer.EquippedItems, selectedEquipmentIndex); //currentAction used as bool => is window active?
                if (updatebackpack || currentAction == Actions.OpenBackpack)
                    BackpackWindow.Draw(currentAction, currentSession.CurrentPlayer.Backpack, selectedBackpackIndex);

                updateStats = updateWholeMap = updateMessages = updateEquipment = updatebackpack = false;   //All windows are assumed to not require redraw
            }

            void getInput()
            {
                //if-statements below are triggered based on what the user did last loop. Different controllers will be used. Input will determine which elements to update.

                if (currentAction == Actions.StayOnMap) //The player entered a movement direction on the controller
                {
                    oldCoordinates = currentSession.CurrentPlayer.Coordinates;
                    currentAction = MapControl.GetInput(currentSession);
                    updateEquipment = updatebackpack = updateMessages = true;
                }

                else if (currentAction == Actions.OpenBackpack) //The player selected the backpack, or is interacting with it
                {
                    var input = InGameMenuControl.GetInput(selectedBackpackIndex, currentSession.CurrentPlayer.Backpack.Count); //Controller returns tuple of 3 values
                    selectedBackpackIndex = input.selectedIndex;    //Sets the currently selected item in backpack

                    if (input.confirmedChoice == true)  //Player decided to use an item
                    {
                        currentSession.UseItem(currentSession.CurrentPlayer.Backpack[selectedBackpackIndex]);
                        updateMessages = updateStats = updateEquipment = true;  //Input may trigger need to update these
                    }
                    currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;   //Player decided to leave the inventory menu
                    updatebackpack = true;  //Backpack will always need to be updated
                }

                else if (currentAction == Actions.OpenEquipment)    //The player selected the equipment, or is interacting with it. See above.
                {
                    var input = InGameMenuControl.GetInput(selectedEquipmentIndex, currentSession.CurrentPlayer.EquippedItems.Count);
                    selectedEquipmentIndex = input.selectedIndex;

                    if (input.confirmedChoice == true)
                    {
                        currentSession.UseItem(currentSession.CurrentPlayer.GetEquippedItems()[selectedEquipmentIndex]);
                        updateMessages = updateStats = updatebackpack = true;
                    }
                    currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;
                    updateEquipment = true;  //Equipment will always need to be updated
                }
            }
        }
    }
}
