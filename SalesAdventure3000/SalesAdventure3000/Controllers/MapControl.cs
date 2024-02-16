using Engine.Models;
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
        public static bool Control(Session currentSession)
        {
            
            int y = currentSession.CurrentPlayer.Coordinate[0,0];
            int x = currentSession.CurrentPlayer.Coordinate[0,1];
            ConsoleKeyInfo input = Console.ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (x - 1 >= 0 && currentSession.CurrentWorld.Map[y, x - 1].Passable)
                        {
                            currentSession.CurrentWorld.Map[y, x - 1].Occupant = currentSession.CurrentPlayer;
                            currentSession.CurrentWorld.Map[y, x].Occupant = null;
                            currentSession.CurrentPlayer.Coordinate[0, 1] = x - 1;

                        }
                        return true;
                        
                    case ConsoleKey.RightArrow:
                        if (x + 1 < currentSession.CurrentWorld.Map.GetLength(1) && currentSession.CurrentWorld.Map[y, x + 1].Passable)
                        {
                            currentSession.CurrentWorld.Map[y, x + 1].Occupant = currentSession.CurrentPlayer;
                            currentSession.CurrentWorld.Map[y, x].Occupant = null;
                            currentSession.CurrentPlayer.Coordinate[0, 1] = x + 1;
                        }
                    return true;
                    
                    case ConsoleKey.DownArrow:
                        if (y - 1 >= 0 && currentSession.CurrentWorld.Map[y - 1, x].Passable)
                        {
                            currentSession.CurrentWorld.Map[y - 1, x].Occupant = currentSession.CurrentPlayer;
                            currentSession.CurrentWorld.Map[y, x].Occupant = null;
                            currentSession.CurrentPlayer.Coordinate[0, 0] = y - 1;
                        }
                    return true;
                    
                    case ConsoleKey.UpArrow:
                        if (y + 1 < currentSession.CurrentWorld.Map.GetLength(0) && currentSession.CurrentWorld.Map[y + 1, x].Passable)
                        {
                            currentSession.CurrentWorld.Map[y + 1, x].Occupant = currentSession.CurrentPlayer;
                            currentSession.CurrentWorld.Map[y + 1, x].Occupant = null;
                            currentSession.CurrentPlayer.Coordinate[0, 0] = y + 1;
                        }
                    return true;
                    
                    case ConsoleKey.Escape:
                    return false;
                        
                
                    
            }

                return false;

        }
        //public static (int, bool) GetInput(int selectedCommand, int upperLimit)
        //{
            

            
        //}

       
        //public void MoveLeft()
        //{
        //    X--;
        //}
        //public void MoveUp()
        //{
        //    Y++;
        //}
        //public void MoveDown()
        //{
        //    Y--;
        //}
        //esc - till menu
        //e- equipment
        //b- backpack
        //currentsession.currentplayer
        //currentsession.currentWorld.map[].occupant=currentplayer
    } 
}
