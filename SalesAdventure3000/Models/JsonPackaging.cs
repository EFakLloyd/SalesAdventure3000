using Engine.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Engine.Models.Entity;
using static Engine.Models.Equipment;

namespace Engine
{
    internal static class JsonPackaging
    {
        public static void CreateJson(Session session)      //Retrieves and packages data that is needed to recreate the session.
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented                            //For readability.
            };

            List<int> equippedItemsList = new();                                //To hold player equipment.
            List<Dictionary<string, string>> backpackDict = new();              //To hold player backpack.
            List<Dictionary<string, string>> consumablesDict = new();    //To hold consumables on countdown.
            List<Dictionary<string, string>> entityList = new();                //To hold entities in world.

            formatEquipmentForJson();       //Helper methods that retrieves and packages data for collections above, no more than is needed to recreate the session.
            formatBackpackForJson();   
            formatConsumablesOnTimer();
            formatWorldEntitiesForJson();
            
            JObject playerStatsObject = JObject.FromObject(session.CurrentPlayer.GetData());    //We create JSON objects for each section of data.
            JObject coordinateObject = JObject.FromObject(session.CurrentPlayer.Coordinates);
            JArray messagesArray = JArray.FromObject(session.GameMessages);
            JArray equippedItemsObject = JArray.FromObject(equippedItemsList);                  //Our special collections.
            JArray backpackArray = JArray.FromObject(backpackDict);
            JArray consumablesArray = JArray.FromObject(consumablesDict);
            JArray entityArray = JArray.FromObject(entityList);

            JObject json = new JObject();                                   //Combines all data into a single JSON object.
            json["PlayerStats"] = playerStatsObject;
            json["PlayerCoordinates"] = coordinateObject;
            json["EquippedItems"] = equippedItemsObject;
            json["Backpack"] = backpackArray;
            json["Consumables"] = consumablesArray;
            json["Entities"] = entityArray;
            json["Messages"] = messagesArray;

            string jsonString = json.ToString(Formatting.Indented);         //Serialize the combined JSON object.         

            File.WriteAllText("output.json", jsonString);

            #region Helper Methods
            void formatWorldEntitiesForJson()
            {
                foreach (Entity entity in session.CurrentWorld.WorldEntities)
                {
                    entityList.Add(new Dictionary<string, string>       //Id and coordinates are needed to recreate an entity.
                    {
                        { "Id", entity.Id.ToString() },
                        { "Y", entity.Coordinates.Y.ToString() },
                        { "X", entity.Coordinates.X.ToString() }
                    });
                }
            }
            void formatEquipmentForJson()
            {
                foreach (KeyValuePair<Slot, Equipment?> item in session.CurrentPlayer.EquippedItems)
                {
                    if (item.Value != null)
                        equippedItemsList.Add(item.Value.Id);
                }
            }
            void formatBackpackForJson()
            {
                foreach (Item item in session.CurrentPlayer.Backpack)
                {
                    if (item is Consumable consumable)
                    {
                        var itemData = consumable.GetStatsOnSave();
                        backpackDict.Add(new Dictionary<string, string>
                        {
                            { "Id", item.Id.ToString() },                   //Duration, TimerIsOn, and Uses of a Consumable may need to be adjusted from standard values.
                            { "Duration", itemData.duration.ToString() },
                            { "TimerIsOn", itemData.timerIsOn.ToString() },
                            { "Uses", itemData.uses.ToString() }
                        });
                    }
                    else if (item is Equipment equipment)
                        backpackDict.Add(new Dictionary<string, string> { { "Id", item.Id.ToString() } });
                }
            }
            void formatConsumablesOnTimer()
            {
                foreach (Consumable consumable in session.ConsumablesOnTimer)
                {
                    var itemData = consumable.GetStatsOnSave();
                    consumablesDict.Add(new Dictionary<string, string>
                        {
                            { "Id", consumable.Id.ToString() },
                            { "Duration", itemData.duration.ToString() },
                            { "TimerIsOn", itemData.timerIsOn.ToString() },
                            { "Uses", itemData.uses.ToString() }
                        });
                }
            }
            #endregion
        }

        public static (Player player, List<Consumable> consumables, List<Entity> entities, List<string> messages) LoadJson()  //Loads data from JSON and recreates objects.
        {
            string jsonString = File.ReadAllText("output.json");

            JObject json = JObject.Parse(jsonString);               //Parse string into JSON object.

            Dictionary<Stat, string> playerStats = json["PlayerStats"].ToObject<Dictionary<Stat, string>>();        //Each section is deserialized.
            Position playerCoordinates = json["PlayerCoordinates"].ToObject<Position>();
            List<string> loadedMessages = json["Messages"].ToObject<List<string>>();
            List<int> equippedItemsJson = json["EquippedItems"].ToObject<List<int>>();
            List<Dictionary<string, string>> backpackJson = json["Backpack"].ToObject<List<Dictionary<string, string>>>();
            List<Dictionary<string, string>> consumablesJson = json["Consumables"].ToObject<List<Dictionary<string, string>>>();
            List<Dictionary<string, string>> entitiesJson = json["Entities"].ToObject<List<Dictionary<string, string>>>();

            Dictionary<Slot, Equipment?> loadedEquipment = new() { { Slot.Head, null }, { Slot.Weapon, null }, { Slot.Torso, null }, { Slot.Bling, null } }; //Collections and instances are created to hold loaded data.
            List<Item> loadedbackpack = new();
            Player loadedPlayer = new();
            List<Consumable> loadedConsumables = new();
            List<Entity> loadedEntities = new();

            reconstructEquipment();     //Helper methods fill the collections and instances above.
            reconstructBackpack();
            reconstructPlayer();
            reconstructConsumables();
            reconstructEntitites();

            return (loadedPlayer, loadedConsumables, loadedEntities, loadedMessages);  //Returns reconstructed parts of the session.

            #region Helper Methods
            void reconstructEquipment()
            {
                foreach (int itemId in equippedItemsJson)
                {
                    Equipment equipment = EquipmentFactory.CreateEquipment(itemId);
                    loadedEquipment[equipment.Type] = equipment;
                }
            }
            void reconstructBackpack()      //Backpack contains both equipment and consumables. We identify the type via Id. Consumable stats are adjusted.
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
            void reconstructConsumables()
            {
                foreach (Dictionary<string, string> loadedConsumable in consumablesJson)
                {
                    int.TryParse(loadedConsumable["Id"], out int itemId);
                    Consumable consumable = ConsumableFactory.CreateConsumable(itemId);
                    int? duration = string.IsNullOrEmpty(loadedConsumable["Duration"]) ? null : Convert.ToInt32(loadedConsumable["Duration"]);
                    consumable.SetStatsOnLoad(duration, Convert.ToBoolean(loadedConsumable["TimerIsOn"]), Convert.ToInt32(loadedConsumable["Uses"]));
                    loadedConsumables.Add(ConsumableFactory.CreateConsumable(itemId));
                }
            }
            void reconstructEntitites()     //We identify entity type via Id. Reconstruct in the factories.
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
            #endregion
        }
    }
}
