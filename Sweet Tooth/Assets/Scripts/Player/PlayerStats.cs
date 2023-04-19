using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    public float playerMaxHealth; // max player health
    public float playerHealth; // current player health
    public float walkSpeed; // max walking speed, base factor
    public bool isDead; // is the player dead?

    [Header("Inventory")]
    public Dictionary<string, int> inv = new Dictionary<string, int>();

    [Header("Scripts")]
    public HUD HUD;
    public GameOverMenu gameOverMenu;

    void Start()
    {
        HealPlayer(100f, "startGame");
        inv.Add("candyCornChunk", 0);
        inv.Add("gummyBearHead", 0);
        inv.Add("mintyFowlLeg", 0);
        inv.Add("peanutButterToadLeg", 0);
    }

    void Update()
    {
        SaveDataVariables();
    }

    public void SaveDataVariables()
    {
        GameDataManager.Data.playerLocation = transform.position; // saves player location
    }

    public void LoadDataVariables()
    {
        transform.position = GameDataManager.Data.playerLocation;
    }

    public void DamagePlayer(float amount, string source)
    {
        playerHealth -= amount;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            if (!isDead)
            {
                PlayerDeath(source);
            }
        }

        HUD.pinkCurrentTime = 0f; // reset pink bar timer
        HUD.healCurrentTime = 0f; // reset natural healing
    }

    public void HealPlayer(float amount, string source)
    {
        if (!isDead)
        {
            playerHealth += amount;
        }

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }

    public void PlayerDeath(string source)
    {
        isDead = true;
        gameOverMenu.Death(source);
    }
}
