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

    [Header("Scripts")]
    public HUD HUD;
    public GameOverMenu gameOverMenu;
    public PeanutBrittleShotty peanutBrittleShotty;
    public CaneStriker caneStriker;

    void Start()
    {
        Debug.LogError("YARRRRRR" + GameDataManager.Data.comingFromSave);
        HealPlayer(10000f, "startGame");
        // if player loads scene by clicking Load Game, load their save in on first frame
        if (GameDataManager.Data.comingFromSave == true)
        {
            GameDataManager.LoadData();
            LoadDataVariables();
        }
        else
        {
            GameDataManager.ClearSave();
        }
    }

    void Update()
    {
        SaveDataVariables();
    }

    public void SaveDataVariables()
    {
        // GameDataManager.Data.playerLocation = transform.position; // saves player location
        GameDataManager.Data.gameVersion = HUD.version;
        // tool unlocks
        GameDataManager.Data.isPeanutButterShottyUnlocked = peanutBrittleShotty.isUnlocked;
        GameDataManager.Data.isCaneStrikerUnlocked = caneStriker.isUnlocked;
    }

    public void LoadDataVariables()
    {
        // transform.position = GameDataManager.Data.playerLocation;
        peanutBrittleShotty.isUnlocked = GameDataManager.Data.isPeanutButterShottyUnlocked;
        caneStriker.isUnlocked = GameDataManager.Data.isCaneStrikerUnlocked;
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
