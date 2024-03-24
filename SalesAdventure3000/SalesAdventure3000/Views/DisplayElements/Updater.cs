using Engine;

namespace SalesAdventure3000_UI.Views.DisplayElements
{
    internal ref struct Updater
    {
        public Session currentSession;
        public ref int backpackIndex;
        public ref int battleOrEquipmentIndex;
        public ref ViewEnums.Actions currentAction;

        public Updater(Session session, ref int selectedBackpackIndex, ref int selectedEquipmentIndex, ref ViewEnums.Actions currentAction)
        {
            this.currentSession = session;
            this.backpackIndex = ref selectedBackpackIndex;
            this.battleOrEquipmentIndex = ref selectedEquipmentIndex;
            this.currentAction = ref currentAction;
        }

        public void Draw(ViewEnums.Element[] elements)
        {
            foreach (ViewEnums.Element element in elements)
            {
                switch (element)
                {
                    case ViewEnums.Element.Stats:
                        StatsWindow.Draw(currentSession.CurrentPlayer.GetData());
                        break;
                    case ViewEnums.Element.WorldTiles:
                        WorldDisplay.DrawTiles(currentSession.CurrentWorld.Map, currentSession.CurrentPlayer.OldCoordinates, currentSession.CurrentPlayer.Coordinates);
                        break;
                    case ViewEnums.Element.Messages:
                        MessageWindow.Draw(currentSession.GameMessages);
                        break;
                    case ViewEnums.Element.BattleMenu:
                        BattleMenuWindow.Draw(currentAction, battleOrEquipmentIndex);
                        break;
                    case ViewEnums.Element.Equipment:
                        EquipmentWindow.Draw(currentAction, currentSession.CurrentPlayer.EquippedItems, battleOrEquipmentIndex);
                        break;
                    case ViewEnums.Element.Backpack:
                        BackpackWindow.Draw(currentAction, currentSession.CurrentPlayer.Backpack, backpackIndex); 
                        break;
                    }
            }
        }
    }
}
