using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[UnityEngine.Scripting.Preserve]
public static class GameDataManager
{
    private static readonly string SAVE_FILE_PATH = Application.persistentDataPath + "/gamedata.json";

    public static GameData Data { get; private set; } = new GameData();

    public static void LoadData()
    {
        if (File.Exists(SAVE_FILE_PATH))
        {        
            string json = File.ReadAllText(SAVE_FILE_PATH);
            Data = JsonUtility.FromJson<GameData>(json);

            // Convert inventory list to dictionary
            Data.inv = new Dictionary<string, float>();
            foreach (var item in Data.inventory)
            {
                Data.inv[item.itemName] = item.itemAmount;
            }
        }
        else
        {
            Data = new GameData();
            SaveData();
        }

        // get the Player GameObject by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // get the PlayerStats component
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.LoadDataVariables();
            }
        }
    }

    public static void SaveData()
    {
        // Convert inventory dictionary to list
        Data.inventory = new List<InventoryItem>();
        foreach (var kvp in Data.inv)
        {
            InventoryItem item = new InventoryItem
            {
                itemName = kvp.Key,
                itemAmount = kvp.Value
            };
            Data.inventory.Add(item);
        }

        // Serialize and save the data
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(SAVE_FILE_PATH, json);
    }

    public static void ClearSave()
    {
        // THIS IS DEFAULT VALUES FOR EVERYTHING.
        // Data.playerLocation = new Vector3(0, 2, -70);
        Data.gameVersion = "Unknown";
        Data.isPeanutButterShottyUnlocked = false;
        Data.isCaneStrikerUnlocked = false;
        foreach (var key in Data.inv.Keys.ToList())
        {
            Data.inv[key] = 0;
        }

        Data.mouseSmoothing = 100f;
        Data.xSensitivity = 20f;
        Data.ySensitivity = 20f;
        Data.highQuality = false;
        Data.isSilly = false;
        Data.fpsCounter = false;
        Data.musicVolume = 0.05f;
        Data.soundsVolume = 0.05f;
        
        SaveData();
    }
}
