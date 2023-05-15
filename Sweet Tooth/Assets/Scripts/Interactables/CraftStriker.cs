using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftStriker : Interactable
{
    public CaneStriker caneStriker;

    protected override void Interact()
    {
        if (caneStriker.isUnlocked == false)
        {
            if (GameDataManager.Data.inv["mintyFowlLeg"] >= 15 && GameDataManager.Data.inv["snowCamelPart"] >= 3)
            {
                GameDataManager.Data.inv["mintyFowlLeg"] -= 15;
                GameDataManager.Data.inv["snowCamelPart"] -= 3;
                caneStriker.isUnlocked = true;
            }
        }
    }
}
