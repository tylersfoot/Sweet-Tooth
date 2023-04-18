using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataManager : MonoBehaviour
{
    // private static readonly string SAVE_FILE_PATH = Application.persistentDataPath + "/gamedata.json";
    // only for testing purposes, change later to above code; 
    // above goes in AppData, below in the game directory
    private static readonly string SAVE_FILE_PATH = Path.Combine(Application.dataPath, "gamedata.json");

    public static GameData Data { get; private set; }

    public static void LoadData()
    {
        if (File.Exists(SAVE_FILE_PATH))
        {
            string json = File.ReadAllText(SAVE_FILE_PATH);
            Data = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Data = new GameData();
            SaveData();
        }
    }

    public static void SaveData()
    {
        string json = JsonUtility.ToJson(Data);
        File.WriteAllText(SAVE_FILE_PATH, json);
    }
}
