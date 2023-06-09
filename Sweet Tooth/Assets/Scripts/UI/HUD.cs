using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public Image redHealthBar; // instantly changes to health
    public Image pinkHealthBar; // lerps to change with playerHealthDisplay
    public Image sugarRushBar; // changes over time
    public Image crosshair;

    public Tool tool;
    public PlayerAbility playerAbility;
    public PlayerStats playerStats;

    public float playerHealthDisplay; // the displayed version, lerped

    public bool isDamaged = false; // for stopping pink bar progress
    public float healWaitTime; // time until player starts naturally healing
    public float healCurrentTime;
    public float healSpeed;
    public float pinkLerpSpeed; // the lerp speed of the pink health bar
    public float pinkWaitTime; // time until pink bar starts updating
    public float pinkCurrentTime; 
    private float pinkLerpedFillAmount;
    public float redLerpSpeed; 
    // private float redLerpedFillAmount = 0;

    private float deltaTime = 0.0f; // for calculating fps
    public string version;

    void Update()
    {
        // calculate the FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        // update the FPS text
        if (GameDataManager.Data.fpsCounter) {
            fpsText.text = Mathf.RoundToInt(fps).ToString() + " FPS | " + version;
        }
        else
        {
            fpsText.text = "";
        }

        // update ammo text
        if (tool.currentAmmoDisplay == "")
        {
            ammoText.text = "";
        }
        else
        {
            ammoText.text = tool.currentAmmoDisplay + "/" + tool.maxAmmoDisplay;
        }

        // sugar rush bar
        sugarRushBar.fillAmount = playerAbility.abilitySugarRushProgress;

        healCurrentTime += Time.deltaTime;
        if (healCurrentTime >= healWaitTime)
        {
             playerStats.HealPlayer(healSpeed * Mathf.Pow(healCurrentTime, 1.5f) * Time.deltaTime, "naturalRegen");
        }

        // smooth the red bar
        playerHealthDisplay = Mathf.Lerp(playerHealthDisplay, playerStats.playerHealth, Time.deltaTime * redLerpSpeed);
        redHealthBar.fillAmount = playerHealthDisplay / playerStats.playerMaxHealth;
        // redLerpedFillAmount = Mathf.Lerp(redHealthBar.fillAmount, playerHealth/playerMaxHealth, Time.deltaTime * redLerpSpeed);
        // redHealthBar.fillAmount = redLerpedFillAmount;

        pinkCurrentTime += Time.deltaTime;
        if (pinkCurrentTime >= pinkWaitTime)
        {
            // lerp the pink health bar towards the red health bar
            pinkLerpedFillAmount = Mathf.Lerp(pinkHealthBar.fillAmount, redHealthBar.fillAmount, Time.deltaTime * pinkLerpSpeed);
        }
        pinkHealthBar.fillAmount = pinkLerpedFillAmount;
        // update the health text
        healthText.text = Mathf.RoundToInt(playerHealthDisplay).ToString() + "/" + playerStats.playerMaxHealth;
    }

    void Start()
    {
        // hides and locks the cursor when the game starts
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameDataManager.Data.gameVersion = version;
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}

