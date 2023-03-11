using UnityEngine;

public class DamageCube : Interactable
{
    public HUD HUDScript;
    private int amount = 20;

    protected override void Interact()
    {
        HUDScript.DamagePlayer(amount, "damageCube");
    }
}
