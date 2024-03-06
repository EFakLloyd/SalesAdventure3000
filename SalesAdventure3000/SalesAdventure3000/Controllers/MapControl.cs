﻿using Engine.Models;
using SalesAdventure3000_UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace SalesAdventure3000_UI.Controllers
{
    public class MapControl
    {
        public static AdventureView.Actions Control(Session currentSession)
            //public static (int[] coordinates, AdventureView.Actions Action) Control(Session currentSession)
        {

            int y = currentSession.CurrentPlayer.Coordinates[0];
            int x = currentSession.CurrentPlayer.Coordinates[1];
            int oldX=x;
            int oldY= y;
            ConsoleKeyInfo input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.LeftArrow:
                    x --;
                    break;

                case ConsoleKey.RightArrow:
                    x ++;
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
                currentSession.CurrentPlayer.SetCoordinates( new int[] {y,x} );
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
    
