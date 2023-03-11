using UnityEngine;

public class HealCube : Interactable
{
    public HUD HUDScript;
    private int amount = 20;

    protected override void Interact()
    {
        HUDScript.HealPlayer(amount, "healCube");
    }
}
