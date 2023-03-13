using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public Image redHealthBar; // instantly changes to health
    public Image pinkHealthBar; // lerps to change with playerHealthDisplay
    public Image sugarRushBar; // changes over time

    public Tool tool;
    public PlayerAbility playerAbility;

    public float playerMaxHealth; // max health
    public float playerHealth; // player's health
    public float playerHealthDisplay; // the displayed version, lerped

    public bool isDamaged = false; // for stopping pink bar progress
    public float pinkLerpSpeed; // the lerp speed of the pink health bar
    public float pinkWaitTime; // time until pink bar starts updating
    private float pinkCurrentTime; 
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
        fpsText.text = Mathf.RoundToInt(fps).ToString() + " FPS | " + version;

        // update ammo text
        ammoText.text = tool.ammoDisplay;

        // sugar rush bar
        sugarRushBar.fillAmount = playerAbility.abilitySugarRushProgress;

        // smooth the red bar
        playerHealthDisplay = Mathf.Lerp(playerHealthDisplay, playerHealth, Time.deltaTime * redLerpSpeed);
        redHealthBar.fillAmount = playerHealthDisplay / playerMaxHealth;
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
        healthText.text = Mathf.RoundToInt(playerHealthDisplay).ToString() + "/" + playerMaxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        // hides and locks the cursor when the game starts
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        HealPlayer(100f, "startGame");
    }

    public void DamagePlayer(float amount, string source)
    {
        Debug.Log("Damaged player " + amount + " HP");
        playerHealth -= amount;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            PlayerDeath(source);
        }

        pinkCurrentTime = 0f; // reset pink bar timer
    }

    public void HealPlayer(float amount, string source)
    {
        Debug.Log("Healed player " + amount + " HP");
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

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}

