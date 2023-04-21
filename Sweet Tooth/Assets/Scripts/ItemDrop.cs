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
