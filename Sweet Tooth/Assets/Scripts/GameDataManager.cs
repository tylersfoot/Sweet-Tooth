// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GameDataManager : MonoBehaviour
// {
//     string saveFile; // save file path
//     public static GameData gameData = new GameData(); // static gamedata class reference

//     void Awake()
//     {
//         // update the path once the persistent path exists
//         saveFile = Application.persistentDataPath + "/gamedata.json";

//         if (!File.Exists(saveFile))
//         {
//             // create the save file if it doesn't exist
//             File.Create(saveFile).Dispose();
//         }

//         WriteFile();
//     }

//     public void ReadFile()
//     {
//         if (File.Exists(saveFile))
//         {
//             // read the entire file and save its contents
//             string fileContents = File.ReadAllText(saveFile);
            
//             // deserialize the JSON data into a pattern matching the GameData class
//             gameData = JsonUtility.FromJson<GameData>(fileContents);
//         }
//     }

//     public void WriteFile()
//     {
//         // serialize the object into JSON and save string
//         string jsonString = JsonUtility.ToJson(gameData);

//         // write JSON to file
//         File.WriteAllText(saveFile, jsonString);
//     }
// }
