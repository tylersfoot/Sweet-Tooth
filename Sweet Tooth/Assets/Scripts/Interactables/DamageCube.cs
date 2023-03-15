using UnityEngine;

public class DamageCube : Interactable
{
    public PlayerStats playerStats;
    private int amount = 20;

    protected override void Interact()
    {
        playerStats.DamagePlayer(amount, "damageCube");
    }
}
