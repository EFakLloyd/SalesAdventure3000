using Engine;
using static SalesAdventure3000_UI.Views.ViewEnums;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal ref struct Updater
    {
        public Session currentSession;
        public ref int backpackIndex;
        public ref int battleOrEquipmentIndex;
        public ref Actions currentAction;

        public Updater(Session session, ref int selectedBackpackIndex, ref int selectedEquipmentIndex, ref Actions currentAction)   //Updater keeps tabs on values that are frequently updated via refs.
        {
            this.currentSession = session;
            this.backpackIndex = ref selectedBackpackIndex;
            this.battleOrEquipmentIndex = ref selectedEquipmentIndex;
            this.currentAction = ref currentAction;
        }

        public void Draw(Element[] elements)    //Specified elements are redrawn.
        {
            foreach (Element element in elements)
            {
                switch (element)
                {
                    case Element.Stats:
                        StatsWindow.Draw(currentSession.CurrentPlayer.GetData());
                        break;
                    case Element.WorldTiles:
                        WorldDisplay.DrawTiles(currentSession.CurrentWorld.Map, currentSession.CurrentPlayer.OldCoordinates, currentSession.CurrentPlayer.Coordinates);
                        break;
                    case Element.Messages:
                        MessageWindow.Draw(currentSession.GameMessages);
                        break;
                    case Element.BattleMenu:
                        BattleMenuWindow.Draw(currentAction, battleOrEquipmentIndex);
                        break;
                    case Element.Equipment:
                        EquipmentWindow.Draw(currentAction, currentSession.CurrentPlayer.EquippedItems, battleOrEquipmentIndex);
                        break;
                    case Element.Backpack:
                        BackpackWindow.Draw(currentAction, currentSession.CurrentPlayer.Backpack, backpackIndex); 
                        break;
                    }
            }
        }
    }
}
