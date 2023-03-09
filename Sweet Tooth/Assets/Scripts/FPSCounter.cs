using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float deltaTime = 0.0f;
    public string version = "v2023.3.9.1";

    void Update()
    {
        // calculate the FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // update the FPS text
        fpsText.text = Mathf.RoundToInt(fps).ToString() + " FPS | " + version;

        // scale the font size based on the screen height
        // ! not needed since the canvas now scales
        // float screenHeight = Screen.height;
        // float baseScreenHeight = 1080.0f;
        // float scaleFactor = screenHeight / baseScreenHeight;
        // float fontSize = 100.0f * scaleFactor;
        // fpsText.fontSize = fontSize;
    }
}
