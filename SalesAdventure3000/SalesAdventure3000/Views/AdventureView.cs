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

        public static View AdventureDisplay(Session currentSession)
        {
            Position oldCoordinates = currentSession.CurrentPlayer.Coordinates;
            Actions currentAction = Actions.StayOnMap;
            Updater updater = new Updater(currentSession, ref selectedBackpackIndex, ref selectedEquipmentIndex, ref currentAction);

            Console.Clear();
            WorldDisplay.DrawWorld(currentSession.CurrentWorld.Map);

            while (true)
            {
                updater.Draw(new Element[] { Element.Stats, Element.WorldTiles, Element.Messages, Element.Equipment, Element.Backpack });

                if (currentAction == Actions.StayOnMap)
                    mapNavigation();
                else if (currentAction == Actions.OpenEquipment)
                    openEquipment();
                else if (currentAction == Actions.OpenBackpack)
                    openBackpack();
                else if (currentAction == Actions.GoToMenu)     //The player opted to go back to the main menu
                    return View.Start;
                else if (currentAction == Actions.ContinueFight)
                    return View.Battle;
            }

            void mapNavigation()
            {
                currentSession.CheckTimersAndResolve();

                var input = MapControl.GetInput(currentSession);
                currentAction = input.currentAction;
                oldCoordinates = currentSession.CurrentPlayer.Coordinates;

                if (currentSession.AnyMonsterAt(input.x, input.y))
                    currentAction = Actions.ContinueFight;
                else
                    currentSession.MovePlayer(input.x, input.y);
            }
            void openBackpack()
            {
                var input = InGameMenuControl.GetInput(selectedBackpackIndex, currentSession.CurrentPlayer.Backpack.Count); //Controller returns tuple of 3 values
                selectedBackpackIndex = input.selectedIndex;    //Sets the currently selected item in backpack

                if (input.confirmedChoice == true)  //Player decided to use an item
                {
                    currentSession.UseItem(currentSession.CurrentPlayer.Backpack[selectedBackpackIndex]);
                    selectedBackpackIndex = selectedBackpackIndex == currentSession.CurrentPlayer.Backpack.Count ? selectedBackpackIndex - 1 : selectedBackpackIndex;
                }
                if (!input.stayInLoop)
                    currentAction = Actions.StayOnMap;
            }
            void openEquipment()
            {
                var input = InGameMenuControl.GetInput(selectedEquipmentIndex, currentSession.CurrentPlayer.EquippedItems.Count);
                selectedEquipmentIndex = input.selectedIndex;

                if (input.confirmedChoice == true)
                    currentSession.UseItem(currentSession.CurrentPlayer.GetEquippedItems()[selectedEquipmentIndex], "removeEquipment");
                if (!input.stayInLoop)
                    currentAction = Actions.StayOnMap;
            }
        }
    }
}
