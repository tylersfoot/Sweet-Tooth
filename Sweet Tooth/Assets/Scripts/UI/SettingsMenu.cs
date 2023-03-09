using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    // drag in GameObjects, used for getting variables from scripts on these objects
    public GameObject canvas;
    public PauseMenu pauseMenu; // currentScreen
    public PlayerLook playerLook; // xSensitivity, ySensitivity
    
    public Slider xSensitivitySlider;
    public TextMeshProUGUI xSensitivityDisplay;
    public Slider ySensitivitySlider;
    public TextMeshProUGUI ySensitivityDisplay;

    void Start()
    {
        canvas.SetActive(false);
        xSensitivityDisplay.text = "X Sensitivity: " + playerLook.xSensitivity.ToString("n2");
        ySensitivityDisplay.text = "Y Sensitivity: " + playerLook.ySensitivity.ToString("n2");
    }
    void Update()
    {
        // debugging
        // Debug.Log(playerLook.xSensitivity + " ... " + playerLook.screenSizeFactor);
    }

    public void SetXSensitivity(float xSens)
    {
        playerLook.xSensitivity = xSens * playerLook.screenSizeFactor;
        xSensitivityDisplay.text = "X Sensitivity: " + (xSens * playerLook.screenSizeFactor).ToString("n2");
    }

    public void SetYSensitivity(float ySens)
    {
        playerLook.ySensitivity = ySens * playerLook.screenSizeFactor;
        ySensitivityDisplay.text = "Y Sensitivity: " + (ySens * playerLook.screenSizeFactor).ToString("n2");
    }
    
    public void Back()
    {
        if (pauseMenu.currentScreen == "settingsMenu")
        {
            pauseMenu.currentScreen = "pauseMenu";
            canvas.SetActive(false);
            pauseMenu.canvas.SetActive(true);
        }
    }
}
