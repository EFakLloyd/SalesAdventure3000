using Engine.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Engine.Models.Entity;

namespace Engine
{
    static public class JsonPackaging
    {
        public static void CreateJson(Session session)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            List<int> equippedItemsList = new();
            List<Dictionary<string, string>> backpackList = new();
            List<Dictionary<string, string>> entityList = new();

            formatEquipmentForJson();
            formatBackpackForJson();
            formatWorldEntitiesForJson();

            // Create JSON objects for each section of data
            JObject playerStatsObject = JObject.FromObject(session.CurrentPlayer.GetData());
            JObject coordinateObject = JObject.FromObject(session.CurrentPlayer.Coordinates);
            JArray messagesArray = JArray.FromObject(session.GameMessages);
            JArray equippedItemsObject = JArray.FromObject(equippedItemsList);
            JArray backpackArray = JArray.FromObject(backpackList);
            JArray entityArray = JArray.FromObject(entityList);

            // Combine all data into a single JSON object
            JObject json = new JObject();
            json["PlayerStats"] = playerStatsObject;
            json["PlayerCoordinates"] = coordinateObject;
            json["EquippedItems"] = equippedItemsObject;
            json["Backpack"] = backpackArray;
            json["Entities"] = entityArray;
            json["Messages"] = messagesArray;

            // Serialize the combined JSON object
            string jsonString = json.ToString(Formatting.Indented);

            // Write the JSON string to the output file
            File.WriteAllText("output.json", jsonString);

            void formatWorldEntitiesForJson()
            {
                foreach (Entity entity in session.CurrentWorld.WorldEntities)
                {
                    entityList.Add(new Dictionary<string, string>
                    {
                        { "Id", entity.Id.ToString() },
                        { "Y", entity.Coordinates.Y.ToString() },
                        { "X", entity.Coordinates.X.ToString() }
                    });
                }
            }
            void formatEquipmentForJson()
            {
                foreach (KeyValuePair<Equipment.Slot, Equipment?> item in session.CurrentPlayer.EquippedItems)
                    equippedItemsList.Add(item.Value.Id);
            }
            void formatBackpackForJson()
            {
                foreach (Item item in session.CurrentPlayer.Backpack)
                {
                    if (item is Consumable consumable)
                    {
                        var itemData = consumable.GetStatsOnSave();
                        backpackList.Add(new Dictionary<string, string>
                        {
                            { "Id", item.Id.ToString() },
                            { "Duration", itemData.duration.ToString() },
                            { "TimerIsOn", itemData.timerIsOn.ToString() },
                            { "Uses", itemData.uses.ToString() }
                        });
                    }
                    else if (item is Equipment equipment)
                        backpackList.Add(new Dictionary<string, string> { { "Id", item.Id.ToString() } });
                }
            }
        }

        public static (Player player, List<Entity> entities, List<string> messages) LoadJson()
        {
            // Read the JSON string from the file
            string jsonString = File.ReadAllText("output.json");

            // Parse the JSON string into a JObject
            JObject json = JObject.Parse(jsonString);

            // Deserialize each section of data from the JObject
            Dictionary<Stat, string> playerStats = json["PlayerStats"].ToObject<Dictionary<Stat, string>>();
            Position playerCoordinates = json["PlayerCoordinates"].ToObject<Position>();
            List<string> loadedMessages = json["Messages"].ToObject<List<string>>();
            List<int> equippedItemsJson = json["EquippedItems"].ToObject<List<int>>();
            List<Dictionary<string, string>> backpackJson = json["Backpack"].ToObject<List<Dictionary<string, string>>>();
            List<Dictionary<string, string>> entitiesJson = json["Entities"].ToObject<List<Dictionary<string, string>>>();  //This part is funky right now, since we never remove posts from WorldEntities

            Dictionary<Equipment.Slot, Equipment?> loadedEquipment = new() { { Equipment.Slot.Head, null }, { Equipment.Slot.Weapon, null }, { Equipment.Slot.Torso, null }, { Equipment.Slot.Bling, null } };
            List<Item> loadedbackpack = new();
            Player loadedPlayer = new();
            List<Entity> loadedEntities = new();

            reconstructEquipment();
            reconstructBackpack();
            reconstructPlayer();
            reconstructEntitites();

            return (loadedPlayer, loadedEntities, loadedMessages);

            void reconstructEquipment()
            {
                foreach (int itemId in equippedItemsJson)
                {
                    Equipment equipment = EquipmentFactory.CreateEquipment(itemId);
                    loadedEquipment[equipment.Type] = equipment;
                }
            }
            void reconstructBackpack()
            {
                foreach (Dictionary<string, string> backpackItem in backpackJson)
                {
                    int.TryParse(backpackItem["Id"], out int itemId);
                    if (itemId >= 2000 && itemId < 3000)
                        loadedbackpack.Add(EquipmentFactory.CreateEquipment(itemId));
                    else if (itemId >= 3000 && itemId < 4000)
                    {
                        Consumable consumable = ConsumableFactory.CreateConsumable(itemId);
                        int? duration = string.IsNullOrEmpty(backpackItem["Duration"]) ? null : Convert.ToInt32(backpackItem["Duration"]);
                        consumable.SetStatsOnLoad(duration, Convert.ToBoolean(backpackItem["TimerIsOn"]), Convert.ToInt32(backpackItem["Uses"]));
                        loadedbackpack.Add(ConsumableFactory.CreateConsumable(itemId));
                    }
                }
            }
            void reconstructPlayer()
            {
                loadedPlayer.SetLoadedValues(
                    playerStats[Stat.Name],
                    Convert.ToInt32(playerStats[Stat.AvatarId]),
                    Convert.ToInt32(playerStats[Stat.MaxVitality]),
                    Convert.ToInt32(playerStats[Stat.Vitality]),
                    Convert.ToInt32(playerStats[Stat.Strength]),
                    Convert.ToInt32(playerStats[Stat.Coolness]),
                    Convert.ToInt32(playerStats[Stat.Armour]),
                    playerCoordinates, loadedEquipment, loadedbackpack);
            }
            void reconstructEntitites()
            {
                foreach (Dictionary<string, string> item in entitiesJson)
                {
                    int.TryParse(item["Id"], out int itemId);
                    if (itemId >= 1000 && itemId < 2000)
                        loadedEntities.Add(MonsterFactory.CreateMonster(itemId));
                    else if (itemId >= 2000 && itemId < 3000)
                        loadedEntities.Add(EquipmentFactory.CreateEquipment(itemId));
                    else if (itemId >= 3000 && itemId < 4000)
                        loadedEntities.Add(ConsumableFactory.CreateConsumable(itemId));
                    loadedEntities.Last().SetCoordinates(new Position(Convert.ToInt32(item["Y"]), Convert.ToInt32(item["X"])));
                }
            }
        }
    }
}
