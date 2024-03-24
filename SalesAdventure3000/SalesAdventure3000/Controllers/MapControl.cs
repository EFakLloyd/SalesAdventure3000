using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Views;
using System;

namespace SalesAdventure3000_UI.Controllers
{
    public class MapControl
    {
        public static (int x, int y, ViewEnums.Actions currentAction) GetInput(Session currentSession)
       
        {
            Console.SetCursorPosition(GameDimensions.Width * 2-1, 0);
            int y = currentSession.CurrentPlayer.Coordinates.Y;
            int x = currentSession.CurrentPlayer.Coordinates.X;
            
            ConsoleKeyInfo input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.LeftArrow:
                    x--;
                    break;

                case ConsoleKey.RightArrow:
                    x++;
                    break;

                case ConsoleKey.DownArrow:
                    y++;
                    break;

                case ConsoleKey.UpArrow:
                    y--;
                    break;
                case ConsoleKey.B:
                    return(x,y, ViewEnums.Actions.OpenBackpack);
                    

                case ConsoleKey.E:
                    return (x, y, ViewEnums.Actions.OpenEquipment);

                case ConsoleKey.Escape:
                    return (x, y, ViewEnums.Actions.GoToMenu);

                default:
                    return (x, y, ViewEnums.Actions.StayOnMap);

            }
            return (x, y, ViewEnums.Actions.StayOnMap);

           

        }
    }

}

