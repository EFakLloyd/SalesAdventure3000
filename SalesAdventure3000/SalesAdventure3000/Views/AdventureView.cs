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

        public static View Display(Session currentSession)
        {
            Position oldCoordinates = currentSession.CurrentPlayer.Coordinates;
            Actions currentAction = Actions.StayOnMap;

            StatsWindow.Draw(currentSession.CurrentPlayer.GetData());
            WorldDisplay.DrawWorld(currentSession.CurrentWorld.Map);
            MessageWindow.Draw(currentSession.GameMessages);
            EquipmentWindow.Draw(currentAction, currentSession.CurrentPlayer.EquippedItems, selectedEquipmentIndex);
            BackpackWindow.Draw(currentAction, currentSession.CurrentPlayer.Backpack, selectedBackpackIndex);

            while (true)
            {
                if (currentAction == Actions.StayOnMap)
                    mapNavigation();
                if (currentAction == Actions.OpenEquipment)
                    openEquipment();
                if (currentAction == Actions.OpenBackpack)
                    openBackpack();
                if (currentAction == Actions.GoToMenu)     //The player opted to go back to the main menu
                {
                    Console.Clear();
                    return View.Start;
                }
            }

            void mapNavigation()
            {
                MessageWindow.Draw(currentSession.GameMessages);
                EquipmentWindow.Draw(currentAction, currentSession.CurrentPlayer.EquippedItems, selectedEquipmentIndex);
                BackpackWindow.Draw(currentAction, currentSession.CurrentPlayer.Backpack, selectedBackpackIndex);
                WorldDisplay.DrawTiles(currentSession.CurrentWorld.Map, oldCoordinates, currentSession.CurrentPlayer.Coordinates);

                oldCoordinates = currentSession.CurrentPlayer.Coordinates;
                currentAction = MapControl.GetInput(currentSession);

                //CurrentSession.MovePlayer
            }
            void openBackpack()
            {
                EquipmentWindow.Draw(currentAction, currentSession.CurrentPlayer.EquippedItems, selectedEquipmentIndex);

                while (true)
                {
                    BackpackWindow.Draw(currentAction, currentSession.CurrentPlayer.Backpack, selectedBackpackIndex);

                    var input = InGameMenuControl.GetInput(selectedBackpackIndex, currentSession.CurrentPlayer.Backpack.Count); //Controller returns tuple of 3 values
                    selectedBackpackIndex = input.selectedIndex;    //Sets the currently selected item in backpack

                    if (input.confirmedChoice == true)  //Player decided to use an item
                    {
                        currentSession.UseItem(currentSession.CurrentPlayer.Backpack[selectedBackpackIndex]);
                        selectedBackpackIndex = selectedBackpackIndex == currentSession.CurrentPlayer.Backpack.Count ? selectedBackpackIndex - 1 : selectedBackpackIndex;
                        EquipmentWindow.Draw(currentAction, currentSession.CurrentPlayer.EquippedItems, selectedEquipmentIndex);
                        StatsWindow.Draw(currentSession.CurrentPlayer.GetData());
                    }
                    if (!input.stayInLoop)
                    {
                        currentAction = Actions.StayOnMap;
                        break;
                    }
                    //currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;   //Player decided to leave the inventory menu
                }
            }
            void openEquipment()
            {
                BackpackWindow.Draw(currentAction, currentSession.CurrentPlayer.Backpack, selectedBackpackIndex);

                while (true) 
                {
                    EquipmentWindow.Draw(currentAction, currentSession.CurrentPlayer.EquippedItems, selectedEquipmentIndex);

                    var input = InGameMenuControl.GetInput(selectedEquipmentIndex, currentSession.CurrentPlayer.EquippedItems.Count);
                    selectedEquipmentIndex = input.selectedIndex;

                    if (input.confirmedChoice == true)
                    {
                        currentSession.UseItem(currentSession.CurrentPlayer.GetEquippedItems()[selectedEquipmentIndex], "removeEquipment");
                        BackpackWindow.Draw(currentAction, currentSession.CurrentPlayer.Backpack, selectedBackpackIndex);
                        StatsWindow.Draw(currentSession.CurrentPlayer.GetData());
                    }
                    if (!input.stayInLoop)
                    {
                        currentAction = Actions.StayOnMap;
                        break;
                    }
                }
            }
        }
    }
}
