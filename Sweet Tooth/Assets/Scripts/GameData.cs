using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float exampleVariable;
    public Vector3 playerLocation;

    // inventory
    public Dictionary<string, float> inv = new Dictionary<string, float>
    {
        { "crazyCornChunk", 0 },
        { "gummyBearHead", 0 },
        { "mintyFowlLeg", 0 },
        { "peanutButterToadLeg", 0 }
    };
}