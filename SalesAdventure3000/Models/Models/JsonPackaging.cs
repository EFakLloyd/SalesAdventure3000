using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    static public class JsonPackaging
    {
        public static void CreateJson(Session session)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(session.CurrentPlayer, settings);
            json += "----";
            json += JsonConvert.SerializeObject(session.CurrentWorld.WorldEntities, settings);

            File.WriteAllText("output.json", json);
        }
        public static Player LoadPlayer()
        {
            string json = File.ReadAllText("output.json");

            string[] jsonStrings = json.Split("----");
            Player player = new Player();
            JObject playerObject = JObject.Parse(jsonStrings[0]);

            string name = (string)playerObject["Name"];
            string appearance = (string)playerObject["Appearance"];
            int avatarId = (int)playerObject["AvatarId"];
            int maxVitality = (int)playerObject["MaxVitality"];
            int vitality = (int)playerObject["Vitality"];
            int coolness = (int)playerObject["Coolness"];
            int strength = (int)playerObject["Strength"];
            int armour = (int)playerObject["Armour"];

            JArray coordinatesArray = (JArray)playerObject["Coordinates"];  // Parse coordinates array
            int[] coordinates = coordinatesArray.ToObject<int[]>();

            JArray backpackArray = (JArray)playerObject["Backpack"];        //Deserialize Backpack items
            List<Item> backpack = new List<Item>();

            foreach (JToken itemToken in backpackArray)
            {
                JObject itemObject = (JObject)itemToken;
                string typeOf = (string)itemObject["TypeOf"];

                if (typeOf == "Engine.Models.Consumable")
                {
                    Consumable consumable = ConsumableFactory.CreateConsumable((int)itemObject["Id"]);
                    int? duration = itemObject["Duration"]?.ToObject<int?>();     // Parse Duration, which can be null
                    bool timerIsOn = (bool)itemObject["TimerIsOn"];
                    int uses = (int)itemObject["Uses"];
                    backpack.Add(consumable);
                }
                else if (typeOf == "Engine.Models.Equipment")
                {
                    Equipment equipment = EquipmentFactory.CreateEquipment((int)itemObject["Id"]);
                    backpack.Add(equipment);
                }
            }

            JObject equippedItemsObject = (JObject)playerObject["EquippedItems"];            // Deserialize EquippedItems
            Dictionary<Equipment.Slot, Equipment> equippedItems = new Dictionary<Equipment.Slot, Equipment>();

            foreach (KeyValuePair<string, JToken> kvp in equippedItemsObject)
            {
                string itemName = kvp.Key;
                JObject itemData = (JObject)kvp.Value;
                if (itemData == null)
                    equippedItems.Add((Equipment.Slot)Enum.Parse(typeof(Equipment.Slot), itemName), null);
                else
                {
                    Equipment equipment = (EquipmentFactory.CreateEquipment((int)itemData["Id"]));
                    equippedItems.Add(equipment.Type, equipment);
                }
            }
            
            return new Player(name, appearance, coordinates, avatarId,ConsoleColor.Magenta, strength, vitality, maxVitality, coolness, armour, equippedItems, backpack, 0);
        }
        public static List<Entity> LoadEntities()
        {
            string json = File.ReadAllText("output.json");

            string[] jsonStrings = json.Split("----");
            List<Entity> entities = new List<Entity>();
            JArray entitiesArray = JArray.Parse(jsonStrings[1]);

            foreach (JToken itemToken in entitiesArray)
            {
                JObject itemObject = (JObject)itemToken;
                string typeOf = (string)itemObject["TypeOf"];
                JArray readCoordinatesArray = (JArray)itemObject["Coordinates"];
                int[] readCoordinates = readCoordinatesArray.ToObject<int[]>();

                if (typeOf == "Engine.Models.Monster")
                {
                    Monster monster = MonsterFactory.CreateMonster((int)itemObject["Id"]);
                    int adjustmentMod = (int)itemObject["Vitality"] - monster.Vitality;
                    monster.AdjustStat(Entity.Stat.Vitality, adjustmentMod, Entity.Adjustment.Down);
                    monster.SetCoordinates(readCoordinates);
                    entities.Add(monster);
                }
                else if (typeOf == "Engine.Models.Equipment")
                {
                    Equipment equipment = EquipmentFactory.CreateEquipment((int)itemObject["Id"]);
                    equipment.SetCoordinates(readCoordinates);
                    entities.Add(equipment);
                }
                else if (typeOf == "Engine.Models.Consumable")
                {
                    Consumable consumable = ConsumableFactory.CreateConsumable((int)itemObject["Id"]);
                    consumable.SetCoordinates(readCoordinates);
                    entities.Add(consumable);
                }
            }
            return entities;
        }
    }
}
