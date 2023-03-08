using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // drag in GameObjects, used for getting variables from scripts on these objects
    public GameObject canvas;
    public GameObject player; // sensitivity
    public PauseMenu pauseMenu; // currentScreen
    
    public Slider xSensitivitySlider;
    
    void Start()
    {
        canvas.SetActive(false);
    }

    public void SetSensitivityX(float sensitivityX)
    {
        
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
