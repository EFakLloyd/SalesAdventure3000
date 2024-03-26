using Engine.Models;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static Engine.Models.Entity;

namespace Engine
{
    public class Session    //Session class handles world and player creation, interaction between the player and the world based on input, as well as saving and loading the game.
    {
        public enum CombatAction
        {
            Attack,
            RecklessAttack,
            UseItem,
            Flee
        }
        private Position gameDimensions;
        internal List<Consumable> consumablesOnTimer { get; private set; }
        public bool SaveGameExists { get; private set; }
        public World CurrentWorld { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public List<string> GameMessages { get; private set; }      //Holds strings that are displayed in the info box.
        public List<string[]> Avatars { get; private set; }
        public Monster CurrentMonster { get; private set; }
        public Session(int height, int width)
        {
            this.SaveGameExists = File.Exists("output.json");
            this.GameMessages = new List<string>();
            this.Avatars = LoadAvatars();
            this.gameDimensions = new Position(height, width);
            this.consumablesOnTimer = new List<Consumable>();
            this.CurrentMonster = MonsterFactory.CreateMonster(1001);
        }
        public void StartNewSession(string name, int avatar)
        {
            CurrentPlayer = new Player(name, new Position(gameDimensions.Y/2, gameDimensions.X/2), avatar);  //Standard player starting coordinates.
            CurrentWorld = new World(gameDimensions);
            CurrentWorld.Map[gameDimensions.Y / 2, gameDimensions.X / 2].NewOccupant(CurrentPlayer);
            CurrentWorld.InsertNewEntities(gameDimensions);
        }
        public void LoadSession()               //Reconstructs session with values returned from LoadJson()
        {
            var loadedObjects = JsonPackaging.LoadJson();   

            CurrentPlayer = loadedObjects.player;
            consumablesOnTimer = loadedObjects.consumables;
            CurrentWorld = new World(gameDimensions);
            CurrentWorld.Map[CurrentPlayer.Coordinates.Y, CurrentPlayer.Coordinates.X].NewOccupant(CurrentPlayer);
            CurrentWorld.InsertLoadedEntities(loadedObjects.entities);
            GameMessages = loadedObjects.messages;
        }
        public void SaveSession() 
        {
            JsonPackaging.CreateJson(this);
            SaveGameExists = File.Exists("output.json");    //Used for start menu.
        } 
        public bool AnyMonsterAt(int x, int y)      //Checks if player tries to enter a square with a monster.
        {
            if (CurrentWorld.Map[y, x].Occupant is Monster monster)
            {
                CurrentMonster = monster;           //If CurrentMonster is set to an object, battle vill commence.
                return true;
            }
            return false;
        }
        public void MovePlayer(int x, int y)        //Moves player, if possible. Picks up items and clears them from map and WorldEntities.
        {
            CurrentPlayer.SetOldCoordinates(CurrentPlayer.Coordinates);

            if (Ispassable(y, x))
            {
                if (CurrentWorld.Map[y, x].Occupant is Item item)
                {
                    CurrentPlayer.PutInBackpack(item);
                    GameMessages.Add(item.MessageUponPickUp());
                    CurrentWorld.RemoveEntity(item);
                }
                CurrentWorld.Map[CurrentPlayer.Coordinates.Y, CurrentPlayer.Coordinates.X].ClearOccupant();
                CurrentWorld.Map[y, x].NewOccupant(CurrentPlayer);
                CurrentPlayer.SetCoordinates(new Position(y, x));
            }
            bool Ispassable(int y, int x)
            {
                if (y <= 14 && x >= 0 && x < 42)
                    return CurrentWorld.Map[y, x].Passable;
                return false;
            }
        }
        public (bool playerIsAlive, bool monsterIsDead) HandleCombat(CombatAction action)   //Resolves combat results. Player always goes first.
        {
            bool playerIsAlive = true;
            bool monsterIsDead = false;
            (string message, bool opponentIsDead) playerRoll;
            (string message, bool opponentIsDead) monsterRoll;

            switch (action)     //Helper methods call player/monster attacks. GameMessages is updated and bools show if either combatant is dead.
            {
                case CombatAction.Attack:
                    playerAttack();
                    break;
                case CombatAction.RecklessAttack:   //Reckless attack does ~1.5 times the damage of a regular attack. The player is struck twice by the monster.
                    playerAttack(reckless: true);
                    if (!monsterIsDead)             //Since monster is alive, it attacks back.
                        monsterAttack();
                    break;
                case CombatAction.UseItem:
                    break;
                case CombatAction.Flee:
                    break;
            }
            if (!monsterIsDead)                     //Monster's turn to attack.
                monsterAttack();
            if (!playerIsAlive)
                CurrentPlayer = null;
            if (monsterIsDead)                      //Upon monster defeat, update messages, map, and entities.
            {
                GameMessages.Add(CurrentMonster.MessageUponDefeat());
                CurrentWorld.Map[CurrentMonster.Coordinates.Y, CurrentMonster.Coordinates.X].ClearOccupant();
                CurrentWorld.RemoveEntity(CurrentMonster);
                CurrentMonster = null;
            }

            return (playerIsAlive, monsterIsDead);  //Instructs the view where to go.

            #region Helper Methods
            void playerAttack(bool reckless = false)
            {
                var attackResult = reckless ? CurrentPlayer.RecklessAttack(CurrentMonster) : CurrentPlayer.Attack(CurrentMonster);
                GameMessages.Add(attackResult.message);
                monsterIsDead = attackResult.opponentIsDead;
            }
            void monsterAttack()
            {
                monsterRoll = CurrentMonster.Attack(CurrentPlayer);
                GameMessages.Add(monsterRoll.message);
                playerIsAlive = !monsterRoll.opponentIsDead;
            }
            #endregion
        }
        public void UseItem(Item item, bool removeEquipment = false, bool calledFromBattleView = false)   //Picks appropriate method based on item type. Different function based on bools.
        {
            if (removeEquipment)                                    //The player selected a equipped item directly from their equipment window.
                CurrentPlayer.TakeOff(((Equipment)item).Type);
            if (calledFromBattleView && item is Equipment)          //The player cannot use equipment during battle.
            {
                GameMessages.Add("Cannot use equipment during battle");
                return;
            }
            if (item is Consumable)
            {
                CurrentPlayer.UseConsumable((Consumable)item);
                if (((Consumable)item).TimerIsOn)
                    consumablesOnTimer.Add(((Consumable)item));
            }
            if (item is Equipment)
                CurrentPlayer.PutOn((Equipment)item);
            GameMessages.Add(item.MessageUponUse());
        }
        public void CheckTimersAndResolve()     //Checks consumables that are on timer. Reverses stat modification and removes from list if done.
        {
            for (int i = consumablesOnTimer.Count - 1; i >= 0; i--)
            {
                Consumable consumable = consumablesOnTimer[i];
                if (!consumable.Countdown())
                {
                    CurrentPlayer.AdjustStat(consumable.AffectedStat, consumable.Modifier, Adjustment.Down);
                    consumablesOnTimer.RemoveAt(i);
                }
            }
        }
        private List<string[]> LoadAvatars()    //Loads avatars from file.
        {
            List<string[]> avatars = new List<string[]>();

            using (StreamReader inputFile = new StreamReader("Images/avatars.txt"))
            {
                List<string> currentAvatar = new List<string>();
                string line;
                int lineCount = 0;

                while ((line = inputFile.ReadLine()) != null)
                {
                    currentAvatar.Add(line);
                    lineCount++;

                    if (lineCount % 10 == 0)
                    {
                        avatars.Add(currentAvatar.ToArray());
                        currentAvatar.Clear();
                    }
                }
            }
            return avatars;
        }
    }
}
