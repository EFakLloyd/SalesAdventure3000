using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Views;
using System;

namespace SalesAdventure3000_UI.Controllers
{
    public static class MapControl
    {
        public static (int x, int y, ViewEnums.Actions currentAction) GetInput(Position playerCoordinates)  //Player may choose to try to move to a new tile, or enter menus (Action).
        {
            int y = playerCoordinates.Y;
            int x = playerCoordinates.X;
            
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
                    return (x, y, ViewEnums.Actions.NavigateMap);
            }
            return (x, y, ViewEnums.Actions.NavigateMap);
        }
    }

}

