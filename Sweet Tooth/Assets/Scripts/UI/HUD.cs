using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    public TextMeshProUGUI fpsText;

    public float playerHealth = 100; // player's health

    private float deltaTime = 0.0f; // for calculating fps
    public string version;

    void Update()
    {
        // calculate the FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // update the FPS text
        fpsText.text = Mathf.RoundToInt(fps).ToString() + " FPS | " + version;
    }

    // Start is called before the first frame update
    void Start()
    {
        // hides and locks the cursor when the game starts
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}

