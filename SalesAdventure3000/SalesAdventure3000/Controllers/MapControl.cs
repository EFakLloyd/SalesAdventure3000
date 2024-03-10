﻿using Engine;
using Engine.Models;
using SalesAdventure3000_UI.Views;
using System;

namespace SalesAdventure3000_UI.Controllers
{
    public class MapControl
    {
        public static (int x, int y,AdventureView.Actions currentAction) GetInput(Session currentSession)
       
        {

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
                    return(x,y, AdventureView.Actions.OpenBackpack);
                    

                case ConsoleKey.E:
                    return (x, y, AdventureView.Actions.OpenEquipment);

                case ConsoleKey.Escape:
                    return (x, y, AdventureView.Actions.GoToMenu);

                default:
                    return (x, y, AdventureView.Actions.StayOnMap);

            }
            return (x, y, AdventureView.Actions.StayOnMap);

           

        }
    }

}

