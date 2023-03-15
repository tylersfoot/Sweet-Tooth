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
    public BGM BGM;
    public SoundManager soundManager;

    public AudioSource crazyCornAudio;
    
    public Slider xSensitivitySlider;
    public TextMeshProUGUI xSensitivityDisplay;
    public Slider ySensitivitySlider;
    public TextMeshProUGUI ySensitivityDisplay;
    public Slider BGMSlider;
    public TextMeshProUGUI BGMDisplay;
    public Slider SoundEffectSlider;
    public TextMeshProUGUI SoundEffectDisplay;

    void Start()
    {
        canvas.SetActive(false);

        xSensitivityDisplay.text = "X Sensitivity: " + playerLook.xSensitivity.ToString("n2");
        ySensitivityDisplay.text = "Y Sensitivity: " + playerLook.ySensitivity.ToString("n2");
        BGMDisplay.text = "Music Volume: " + (BGM.volume*100).ToString("n0") + "%";
        SoundEffectDisplay.text = "Effects Volume: " + (soundManager.volume*100).ToString("n0") + "%";

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

    public void SetBGMVolume(float volume)
    {
        BGM.volume = volume;
        BGMDisplay.text = "Music Volume: " + (volume*100).ToString("n0") + "%";
    }

    public void SetEffectsVolume(float volume)
    {
        soundManager.volume = volume;
        if (crazyCornAudio != null)
        {
            crazyCornAudio.volume = volume;
        }
        
        SoundEffectDisplay.text = "Effects Volume: " + (volume*100).ToString("n0") + "%";
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
