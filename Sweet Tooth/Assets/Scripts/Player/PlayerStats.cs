using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // player stat variables
    public float playerMaxHealth;
    public float playerHealth;
    public float walkSpeed;

    // other scripts
    public HUD HUD;

    void Start()
    {
        HealPlayer(100f, "startGame");
    }

    public void DamagePlayer(float amount, string source)
    {
        playerHealth -= amount;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            PlayerDeath(source);
        }

        HUD.pinkCurrentTime = 0f; // reset pink bar timer
        HUD.healCurrentTime = 0f; // reset natural healing
    }

    public void HealPlayer(float amount, string source)
    {
        playerHealth += amount;

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }

    public void PlayerDeath(string source)
    {
        Debug.Log("Player died from " + source);
    }
}
