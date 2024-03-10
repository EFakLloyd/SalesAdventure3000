using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Views;
using System;

namespace SalesAdventure3000_UI.Controllers
{
    public class MapControl
    {
        public static AdventureView.Actions GetInput(Session currentSession)
        //public static (int[] coordinates, AdventureView.Actions Action) Control(Session currentSession)
        {
            Console.SetCursorPosition(GameDimensions.Width * 2-1, 0);
            int y = currentSession.CurrentPlayer.Coordinates.Y;
            int x = currentSession.CurrentPlayer.Coordinates.X;
            int oldX = x;
            int oldY = y;
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
                    return AdventureView.Actions.OpenBackpack;

                case ConsoleKey.E:
                    return AdventureView.Actions.OpenEquipment;

                case ConsoleKey.Escape:
                    return AdventureView.Actions.GoToMenu;

                default:
                    return AdventureView.Actions.StayOnMap;

            }

            if (Ispassable(y, x))
            {
                if (currentSession.CurrentWorld.Map[y, x].Occupant is Item item)
                {
                    currentSession.CurrentPlayer.PutInBackpack(item);
                    currentSession.GameMessages.Add(item.MessageUponPickUp());
                }
                currentSession.CurrentWorld.Map[y, x].NewOccupant(currentSession.CurrentPlayer);
                currentSession.CurrentWorld.Map[oldY, oldX].ClearOccupant();
                currentSession.CurrentPlayer.SetCoordinates(new Position(y, x));
            }
            return AdventureView.Actions.StayOnMap;


            bool Ispassable(int y, int x)
            {
                if (y <= 14 && x >= 0 && x < 42)
                    return currentSession.CurrentWorld.Map[y, x].Passable;
                return false;
            }

        }
    }

}

