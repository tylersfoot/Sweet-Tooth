using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftShotty : Interactable
{
    public PeanutBrittleShotty peanutBrittleShotty;

    protected override void Interact()
    {
        if (peanutBrittleShotty.isUnlocked == false)
        {
            if (GameDataManager.Data.inv["mosquitoPart"] >= 20 && GameDataManager.Data.inv["peanutButterToadLeg"] >= 5)
            {
                GameDataManager.Data.inv["mosquitoPart"] -= 20;
                GameDataManager.Data.inv["peanutButterToadLeg"] -= 5;
                peanutBrittleShotty.isUnlocked = true;
            }
        }
    }
}
