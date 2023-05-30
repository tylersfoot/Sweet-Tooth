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
            if (GameDataManager.Data.inv.ContainsKey(item)) {
                GameDataManager.Data.inv[item] += 1;
            }
            // remove gameobject
            Destroy(gameObject);
        }
    }
}
