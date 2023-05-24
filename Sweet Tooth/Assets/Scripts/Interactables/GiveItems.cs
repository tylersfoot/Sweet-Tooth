using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GiveItems : Interactable
{
    protected override void Interact()
    {
        foreach (var itemKey in GameDataManager.Data.inv.Keys.ToList())
        {
            GameDataManager.Data.inv[itemKey] += 1f;
            GameDataManager.Data.inv[itemKey] *= 10;
        }
    }
}
