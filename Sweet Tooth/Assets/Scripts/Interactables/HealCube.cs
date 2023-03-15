using UnityEngine;

public class HealCube : Interactable
{
    public PlayerStats playerStats;
    private int amount = 20;

    protected override void Interact()
    {
        playerStats.HealPlayer(amount, "healCube");
    }
}
