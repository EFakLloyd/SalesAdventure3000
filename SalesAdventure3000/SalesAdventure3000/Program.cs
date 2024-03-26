using Engine;
using SalesAdventure3000_UI;
using SalesAdventure3000_UI.Views;
using System;
using static SalesAdventure3000_UI.Views.ViewEnums;

namespace SalesAdventure3000
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            View currentView = View.Start;  //Game starts in the menu.

            Session currentSession = new Session(GameDimensions.Height, GameDimensions.Width);  //currentSession is created with global dimensions.

            while (true)
            {
                if (currentView == View.Start)
                    currentView = MenuView.Display(currentSession);

                if (currentView == View.Adventure)
                    currentView = AdventureView.AdventureDisplay(currentSession);

                if (currentView == View.Battle)
                    currentView = BattleView.BattleDisplay(currentSession);

                if (currentView == View.Exit)
                    break;
            }
        }
    }
}