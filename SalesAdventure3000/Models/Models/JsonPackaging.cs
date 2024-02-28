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

            player.Name = (string)playerObject["Name"];
            player.Appearance = (string)playerObject["Appearance"];
            player.MaxVitality = (int)playerObject["MaxVitality"];
            player.Vitality = (int)playerObject["Vitality"];
            player.Coolness = (int)playerObject["Coolness"];
            player.Strength = (int)playerObject["Strength"];
            player.Armour = (int)playerObject["Armour"];

            JArray coordinatesArray = (JArray)playerObject["Coordinates"];  // Parse coordinates array
            player.Coordinates = coordinatesArray.ToObject<int[]>();

            JArray backpackArray = (JArray)playerObject["Backpack"];        //Deserialize Backpack items
            foreach (JToken itemToken in backpackArray)
            {
                JObject itemObject = (JObject)itemToken;
                string typeOf = (string)itemObject["TypeOf"];

                if (typeOf == "Engine.Models.Consumable")
                {
                    Consumable consumable = ConsumableFactory.CreateConsumable((int)itemObject["Id"]);
                    consumable.Duration = itemObject["Duration"]?.ToObject<int?>();     // Parse Duration, which can be null
                    consumable.TimerIsOn = (bool)itemObject["TimerIsOn"];
                    consumable.Uses = (int)itemObject["Uses"];
                    player.Backpack.Add(consumable);
                }
                else if (typeOf == "Engine.Models.Equipment")
                {
                    Equipment equipment = EquipmentFactory.CreateEquipment((int)itemObject["Id"]);
                    player.Backpack.Add(equipment);
                }
            }

            JObject equippedItemsObject = (JObject)playerObject["EquippedItems"];            // Deserialize EquippedItems
            foreach (KeyValuePair<string, JToken> kvp in equippedItemsObject)
            {
                string itemName = kvp.Key;
                JObject itemData = (JObject)kvp.Value;
                if (itemData == null)
                    player.EquippedItems.Add((Equipment.Slot)Enum.Parse(typeof(Equipment.Slot), itemName), null);
                else
                {
                    Equipment equipment = (EquipmentFactory.CreateEquipment((int)itemData["Id"]));
                    player.EquippedItems.Add(equipment.Type, equipment);
                }
            }
            return player;
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
                int[] readCoordinates =readCoordinatesArray.ToObject<int[]>();

                if (typeOf == "Engine.Models.Monster")
                {
                    Monster monster = MonsterFactory.CreateMonster((int)itemObject["Id"]);
                    monster.Vitality = (int)itemObject["Vitality"];
                    monster.Coordinates = readCoordinates;
                    entities.Add(monster);
                }
                else if (typeOf == "Engine.Models.Equipment")
                {
                    Equipment equipment = EquipmentFactory.CreateEquipment((int)itemObject["Id"]);
                    equipment.Coordinates = readCoordinates;
                    entities.Add(equipment);
                }
                else if (typeOf == "Engine.Models.Consumable")
                {
                    Consumable consumable = ConsumableFactory.CreateConsumable((int)itemObject["Id"]);
                    consumable.Coordinates = readCoordinates;
                    entities.Add(consumable);
                }
            }
            return entities;
        }
    }
}
