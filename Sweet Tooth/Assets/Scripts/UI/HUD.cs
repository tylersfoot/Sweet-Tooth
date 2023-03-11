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
    public Image redHealthBar; // instantly changes to health
    public Image pinkHealthBar; // lerps to change with playerHealthDisplay

    public float playerMaxHealth = 100; // max health
    public float playerHealth = 100; // player's health
    public float playerHealthDisplay = 100; // the displayed version, lerped

    public float lerpSpeed = 5f; // the lerp speed of the health bars
    private bool isDamaged = false;
    private Coroutine pinkBarLerpCoroutine;

    private float deltaTime = 0.0f; // for calculating fps
    public string version;

    void Update()
    {
        // calculate the FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // update the FPS text
        fpsText.text = Mathf.RoundToInt(fps).ToString() + " FPS | " + version;

        // lerp the pink health bar towards the red health bar
        // float lerpedFillAmount = Mathf.Lerp(pinkHealthBar.fillAmount, redHealthBar.fillAmount, Time.deltaTime * 5f);
        // pinkHealthBar.fillAmount = lerpedFillAmount;

        // update the playerHealthDisplay variable
        // playerHealthDisplay = Mathf.Lerp(playerHealthDisplay, playerHealth, Time.deltaTime * 5f);
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

        // set the red bar fill amount exactly to the current health
        redHealthBar.fillAmount = playerHealth/playerMaxHealth;

        // update the pink bar fill amount using Lerp
        StartCoroutine(UpdatePinkHealthBar());
    }

    public void HealPlayer(float amount, string source)
    {
        Debug.Log("Healed player " + amount + " HP");
        playerHealth += amount;

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }

        // set the pink bar fill amount exactly to the current health
        pinkHealthBar.fillAmount = playerHealth/playerMaxHealth;

        // update the red bar fill amount using Lerp
        StartCoroutine(UpdateRedHealthBar());
    }

    private IEnumerator UpdateRedHealthBar()
    {
        float t = 0f;
        float startFill = redHealthBar.fillAmount;
        float endFill = playerHealth/playerMaxHealth;

        while (t < 1f)
        {
            t += Time.deltaTime * lerpSpeed;
            redHealthBar.fillAmount = Mathf.Lerp(startFill, endFill, t);
            yield return null;
        }
    }

    private IEnumerator UpdatePinkHealthBar()
    {
        float t = 0f;
        float startFill = pinkHealthBar.fillAmount;
        float endFill = playerHealth/playerMaxHealth;

        while (t < 1f)
        {
            t += Time.deltaTime * lerpSpeed;
            pinkHealthBar.fillAmount = Mathf.Lerp(startFill, endFill, t);
            yield return null;
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

