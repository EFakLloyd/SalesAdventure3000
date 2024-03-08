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
        public void MovePlayer()
        {
        }
        public (bool playerIsAlive, bool monsterIsDead) HandleCombat(CombatAction action)
        {
            bool playerIsAlive = true;
            bool monsterIsDead = false;
            (string message, bool opponentIsDead) playerAttack;
            (string message, bool opponentIsDead) monsterAttack;

            switch (action)
            {
                case CombatAction.Attack:
                    playerAttack = CurrentPlayer.Attack(CurrentMonster);
                    GameMessages.Add(playerAttack.message);
                    monsterIsDead = playerAttack.opponentIsDead;
                    break;
                case CombatAction.RecklessAttack:
                    var playerRecklessAttack = CurrentPlayer.Attack(CurrentMonster);
                    GameMessages.Add(playerRecklessAttack.message);
                    monsterIsDead = playerRecklessAttack.opponentIsDead;
                    if (!monsterIsDead)
                    {
                        var monsterExtraAttack = CurrentMonster.Attack(CurrentPlayer);
                        GameMessages.Add(monsterExtraAttack.message);
                    }
                    break;
                case CombatAction.UseItem:
                    break;
                case CombatAction.Flee:
                    break;
            }
            if (monsterIsDead)
            {
                GameMessages.Add(CurrentMonster.MessageUponDefeat());
                CurrentWorld.WorldEntities.Remove(CurrentMonster);
                CurrentMonster = null;
                return (playerIsAlive, monsterIsDead);
            }
            monsterAttack = CurrentMonster.Attack(CurrentPlayer);
            GameMessages.Add(monsterAttack.message);
            playerIsAlive = !monsterAttack.opponentIsDead;
            if (!playerIsAlive)
            {
                CurrentPlayer = null;
            }
            return (playerIsAlive, monsterIsDead);
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
