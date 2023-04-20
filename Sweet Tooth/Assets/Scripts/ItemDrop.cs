using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public string item;

    private void OnTriggerEnter(Collider other)
    {
        // check if the player touches the object
        if (other.CompareTag("Player"))
        {
            // add to inventory
            // switch (item)
            // {
            // case "crazyCornChunk":
            //     GameDataManager.Data.inv["crazyCornChunk"] += 1;
            //     break;
            // case "gummyBearHead":
            //     GameDataManager.Data.inv["gummyBearHead"] += 1;
            //     break;
            // case "mintyFowlLeg":
            //     GameDataManager.Data.inv["mintyFowlLeg"] += 1;
            //     break;
            // case "peanutButterToadLeg":
            //     GameDataManager.Data.inv["peanutButterToadLeg"] += 1;
            //     break;
            // default:
            //     break;
            // }
            Dictionary<string, string> itemKeys = new Dictionary<string, string>() {
                { "crazyCornChunk", "crazyCornChunk" },
                { "gummyBearHead", "gummyBearHead" },
                { "mintyFowlLeg", "mintyFowlLeg" },
                { "peanutButterToadLeg", "peanutButterToadLeg" }
            };

            if (itemKeys.ContainsKey(item)) {
                GameDataManager.Data.inv[itemKeys[item]] += 1;
            }

            // remove gameobject
            Destroy(gameObject);
        }
    }
}
