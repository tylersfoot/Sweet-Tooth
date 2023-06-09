using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // defaults are located in GameDataManager.cs -> ClearSave()

    public string gameVersion;
    public bool isPeanutButterShottyUnlocked;
    public bool isCaneStrikerUnlocked;
    public bool comingFromSave; // did the player click new game or load save

    // inventory
    public Dictionary<string, float> inv = new Dictionary<string, float>
    {
        {"crazyCornChunk", 0},
        {"gummyBearHead", 0},
        {"mintyFowlLeg", 0},
        {"peanutButterToadLeg", 0},
        {"snowCamelGland", 0},
        {"mosquilateSac", 0},
        {"caramelHoof", 0},
        {"hardRightLolli", 0}
    };
    public List<InventoryItem> inventory = new List<InventoryItem>();

    // ammo
    // maybe convert to a dict+list later?
    // public float bubblegumBlasterAmmo;
    // public float peanutBrittleShottyAmmo;
    // public float caneStrikerAmmo;

    // settings
    public float mouseSmoothing;
    public float xSensitivity;
    public float ySensitivity;
    public bool highQuality;
    public bool isSilly;
    public bool fpsCounter;
    public float musicVolume;
    public float soundsVolume;

}