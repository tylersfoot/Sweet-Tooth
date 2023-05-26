using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // public Vector3 playerLocation;
    public string gameVersion;
    public bool isPeanutButterShottyUnlocked;
    public bool isCaneStrikerUnlocked;
    public bool comingFromSave; // did the player click new game or load save

    // inventory
    public Dictionary<string, float> inv = new Dictionary<string, float>
    {
        { "crazyCornChunk", 0 },
        { "gummyBearHead", 0 },
        { "mintyFowlLeg", 0 },
        { "peanutButterToadLeg", 0 },
        { "snowCamelGland", 0 },
        { "mosquilateSac", 0 },
        { "caramelHoof", 0 },
        { "hardRightLolli", 0 }
    };

    public List<InventoryItem> inventory = new List<InventoryItem>();
}