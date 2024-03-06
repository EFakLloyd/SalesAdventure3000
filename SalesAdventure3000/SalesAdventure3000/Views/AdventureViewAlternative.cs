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
        //private static List<Equipment?> equipment22 = new List<Equipment>();

        public static View Display(Session currentSession)
        {
            int[] oldCoordinates = currentSession.CurrentPlayer.Coordinates;
            Actions currentAction = Actions.StayOnMap;

            //These bools are used to keep track on which view elements should be redrawn. On view entry all is drawn, therefore set to true
            bool updateStats = true;
            bool updateWholeMap = true;
            bool updateMessages = true;
            bool updateEquipment = true;
            bool updatebackpack = true;

            while (true)
            {
                /*The block below checks the update bools and describes which elements should be dedrawn, as well as other instructions*/
                //Handles updates to the map
                if (updateWholeMap)     //This will probably only happen on first draw of the map
                    WorldDisplay.DrawWorld(currentSession.CurrentWorld.Map);
                else if (currentAction == Actions.StayOnMap)    //StayOnMap means that the player moved (or tried to move) on the map
                    WorldDisplay.UpdateWorld(currentSession.CurrentWorld.Map, oldCoordinates, currentSession.CurrentPlayer.Coordinates); //Supplies coordinates to be redrawn.

                //Non interactable windows.
                if (updateStats)
                    StatsWindow.DrawPlayerStats(currentSession.CurrentPlayer);
                if (updateMessages)
                    MessageWindow.DrawGameMessages(currentSession.GameMessages);

                //Player containers need more info to be drawn correctly. They can be affected directly or indirectly, and both need to trigger a redraw
                if (updateEquipment || currentAction == Actions.OpenEquipment)  //If currentAction is set the window is drawn differently.
                    EquipmentWindow.DrawEquipment(currentAction, currentSession.CurrentPlayer.EquippedItems, equipmentIndex); //currentAction used as bool => is window active?
                if (updatebackpack || currentAction == Actions.OpenBackpack)
                    BackpackWindow.DrawBackpack(currentAction, currentSession.CurrentPlayer.Backpack, backpackIndex);

                updateStats = updateWholeMap = updateMessages = updateEquipment = updatebackpack = false;   //All windows are assumed to not require redraw

                if (currentAction == Actions.StayOnMap)
                {
                    oldCoordinates = currentSession.CurrentPlayer.Coordinates;
                    currentAction = MapControl.Control(currentSession);
                    //these below should only update based on userinput from MapControl
                    updateEquipment = updatebackpack = updateMessages = true;
                }             
                else if (currentAction == Actions.OpenBackpack) 
                {
                    var input = PlayerInventoryControl.GetInput(backpackIndex, currentSession.CurrentPlayer.Backpack.Count);
                    backpackIndex = input.selectedIndex;
                    if (input.confirmedChoice == true)
                    {
                        currentSession.UseItem(currentSession.CurrentPlayer.Backpack[backpackIndex]);
                        updateMessages = updateStats = updateEquipment = true;  //Input may trigger need to update these
                    }
                    currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;
                    updatebackpack = true;  //Backpack will always need to be updated
                }
                else if (currentAction == Actions.OpenEquipment)
                {
                    List<Equipment?> equipment = new List<Equipment> ();    //We need the dict as a indexable list, for access.
                    foreach (KeyValuePair<Equipment.Slot, Equipment?> item in currentSession.CurrentPlayer.EquippedItems)
                        equipment.Add(item.Value);
                    var input = PlayerInventoryControl.GetInput(equipmentIndex,currentSession.CurrentPlayer.EquippedItems.Count);
                    equipmentIndex = input.selectedIndex;
                    if (input.confirmedChoice == true)
                    {
                        currentSession.UseItem(equipment[equipmentIndex]);
                        updateMessages = updateStats = updatebackpack = true;  //Input may trigger need to update these
                    }
                    currentAction = input.stayInLoop ? currentAction : Actions.StayOnMap;
                    updateEquipment = true;  //Equipment will always need to be updated
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
