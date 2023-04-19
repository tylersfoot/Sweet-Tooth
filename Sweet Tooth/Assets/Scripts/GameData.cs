using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float exampleVariable;
    public Vector3 playerLocation;

    // inventory
    public Dictionary<string, float> inv = new Dictionary<string, float>();
}