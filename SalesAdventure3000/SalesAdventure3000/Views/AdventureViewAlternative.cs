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
    public class AdventureView2
    {
        private static int backpackIndex = 0;
        private static int equipmentIndex = 0;
        private static List<Equipment?> equipment = new List<Equipment>();

        public static View Display(Session currentSession)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            int width = 42;
            int height = 15;
            int[] oldCoordinates = currentSession.CurrentPlayer.Coordinates;
            Actions currentAction = Actions.StayOnMap;
            bool updateStats = true;
            bool updateWholeMap = true;
            bool updateMessages = true;
            bool updateEquipment = true;
            bool updatebackpack = true;

            while (true)
            {
                if (updateStats)
                    StatsWindow.DrawPlayerStats(currentSession.CurrentPlayer);
                if (updateWholeMap)
                    WorldDisplay.DrawWorld(currentSession.CurrentWorld.Map);
                else if (currentAction == Actions.StayOnMap)
                    WorldDisplay.UpdateWorld(currentSession.CurrentWorld.Map, oldCoordinates, currentSession.CurrentPlayer.Coordinates);
                if (updateMessages)
                    MessageWindow.DrawGameMessages(currentSession.GameMessages);
                if (updateEquipment || currentAction == Actions.OpenEquipment)
                    EquipmentWindow.DrawEquipment(currentAction, currentSession.CurrentPlayer.EquippedItems, equipmentIndex);
                if (updatebackpack || currentAction == Actions.OpenBackpack)
                    BackpackWindow.DrawBackpack(currentAction, currentSession.CurrentPlayer.Backpack, backpackIndex);

                updateStats = updateWholeMap = updateMessages = updateEquipment = updatebackpack = false;

                if (currentAction == Actions.StayOnMap)
                {
                    oldCoordinates = currentSession.CurrentPlayer.Coordinates;
                    currentAction = MapControl.Control(currentSession);
                    //these should only update based on userinput from MapControl.
                    updateEquipment = true;
                    updatebackpack = true;
                }             
                else if (currentAction == Actions.OpenBackpack) 
                {
                    var input = PlayerInventoryControl.GetInput(backpackIndex, currentSession.CurrentPlayer.Backpack.Count);
                    backpackIndex = input.selectedIndex;
                    if (input.confirmedChoice == true)
                    {
                        currentSession.UseItem(currentSession.CurrentPlayer.Backpack[backpackIndex]);
                        updateMessages = updateStats = updateEquipment = true;
                    }
                    currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;
                    updatebackpack = true;
                }
                else if (currentAction == Actions.OpenEquipment)
                {
                    var input = PlayerInventoryControl.GetInput(equipmentIndex,currentSession.CurrentPlayer.EquippedItems.Count);
                    equipmentIndex = input.selectedIndex;
                    if (input.confirmedChoice == true)
                    {
                        currentSession.UseItem(equipment[equipmentIndex]);
                        updateMessages = updateStats = updatebackpack = true;
                    }
                    currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;
                    updateEquipment = true;
                }
                else if (currentAction == Actions.GoToMenu)
                {                
                    Console.Clear();
                    return View.Exit;
                }
            }
        }
    }
}
