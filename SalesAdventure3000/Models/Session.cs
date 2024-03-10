using Engine.Models;
using System.Security.Cryptography.X509Certificates;

namespace Engine
{
    public class Session    //Session class handles world and player creation, interaction between the player and the world based on input, saving and loading the game.
    {
        public enum CombatAction
        {
            Attack,
            RecklessAttack,
            UseItem,
            Flee
        }
        private Position gameDimensions;
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<string> GameMessages { get; set; }  //Holds strings that are displayed in the info box.
        public List<string[]> Avatars { get; set; }
        public Monster CurrentMonster { get; private set; }
        public Session(int height, int width)
        {
            this.GameMessages = new List<string>();
            this.Avatars = LoadAvatars();
            this.gameDimensions = new Position(height, width);
            CurrentMonster = MonsterFactory.CreateMonster(1000);
        }
        public void StartNewSession(string name, int avatar)
        {
            CurrentPlayer = new Player(name, new Position(7, 22), avatar);  //Standard player starting coordinates.
            CurrentWorld = new World(gameDimensions);
            CurrentWorld.Map[7, 22].NewOccupant(CurrentPlayer);
            CurrentWorld.InsertNewEntities(gameDimensions);
        }
        public void LoadSession()
        {
            var loadedObjects = JsonPackaging.LoadJson();

            CurrentPlayer = loadedObjects.player;
            CurrentWorld = new World(gameDimensions);
            CurrentWorld.Map[CurrentPlayer.Coordinates.Y, CurrentPlayer.Coordinates.X].NewOccupant(CurrentPlayer);
            CurrentWorld.InsertLoadedEntities(loadedObjects.entities);
            GameMessages = loadedObjects.messages;
        }
        public void SaveSession()
        {
            JsonPackaging.CreateJson(this);
        }
        public bool MovePlayer(int x, int y)
        {
            int oldX = CurrentPlayer.Coordinates.X;
            int oldY = CurrentPlayer.Coordinates.Y;


            if (Ispassable(y, x))
            {
                if (CurrentWorld.Map[y, x].Occupant is Item item)
                {
                    CurrentPlayer.PutInBackpack(item);
                    GameMessages.Add(item.MessageUponPickUp());
                }
                if (CurrentWorld.Map[y, x].Occupant is Monster monster)
                {
                    CurrentMonster = monster;
                    return true;
                }
                CurrentWorld.Map[y, x].NewOccupant(CurrentPlayer);
                CurrentWorld.Map[oldY, oldX].ClearOccupant();
                CurrentPlayer.SetCoordinates(new Position(y, x));
            }



            bool Ispassable(int y, int x)
            {
                if (y <= 14 && x >= 0 && x < 42)
                    return CurrentWorld.Map[y, x].Passable;
                return false;
            }
            return false;
        }
        public (bool playerIsAlive, bool monsterIsDead) HandleCombat(CombatAction action)
        {
            bool playerIsAlive = true;
            bool monsterIsDead = false;
            (string message, bool opponentIsDead) playerRoll;
            (string message, bool opponentIsDead) monsterRoll;

            switch (action)
            {
                case CombatAction.Attack:
                    playerAttack();
                    if (!monsterIsDead)
                        monsterAttack();
                    break;
                case CombatAction.RecklessAttack:
                    playerRecklessAttack();
                    if (!monsterIsDead)
                        monsterAttack();
                    break;
                case CombatAction.UseItem:
                    monsterAttack();
                    break;
                case CombatAction.Flee:
                    monsterAttack();
                    break;
            }
            if (!playerIsAlive)
                CurrentPlayer = null;
            if (monsterIsDead)
            {
                GameMessages.Add(CurrentMonster.MessageUponDefeat());
                CurrentMonster = null;
            }

            return (playerIsAlive, monsterIsDead);

            void playerAttack()
            {
                playerRoll = CurrentPlayer.Attack(CurrentMonster);
                GameMessages.Add(playerRoll.message);
                monsterIsDead = playerRoll.opponentIsDead;
            }
            void playerRecklessAttack()
            {
                var playerRecklessRoll = CurrentPlayer.RecklessAttack(CurrentMonster);
                GameMessages.Add(playerRecklessRoll.message);
                monsterIsDead = playerRecklessRoll.opponentIsDead;
            }
            void monsterAttack()
            {
                monsterRoll = CurrentMonster.Attack(CurrentPlayer);
                GameMessages.Add(monsterRoll.message);
                playerIsAlive = !monsterRoll.opponentIsDead;
            }
        }
        public void PickupItem(Item item)
        {

        }
        public void UseItem(Item item)
        {

        }
        private List<string[]> LoadAvatars()
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
