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
    public SillyMode sillyMode; // sillyMode
    public BGM BGM;
    public SoundManager soundManager;
    public GameObject globalVolume;

    public AudioSource crazyCornAudio;
    
    public Slider xSensitivitySlider;
    public TextMeshProUGUI xSensitivityDisplay;
    public Slider ySensitivitySlider;
    public TextMeshProUGUI ySensitivityDisplay;
    public Slider mouseSmoothingSlider;
    public TextMeshProUGUI mouseSmoothingDisplay;
    public Slider BGMSlider;
    public TextMeshProUGUI BGMDisplay;
    public Slider SoundEffectSlider;
    public TextMeshProUGUI SoundEffectDisplay;

    void Start()
    {
        canvas.SetActive(false);
        SetXSensitivity(playerLook.xSensitivity);
        SetYSensitivity(playerLook.ySensitivity);
        SetMouseSmoothing(50/playerLook.mouseSmoothing);
        BGMDisplay.text = "Music Volume: " + (BGM.volume*100).ToString("n0") + "%";
        SoundEffectDisplay.text = "Effects Volume: " + (soundManager.volume*100).ToString("n0") + "%";
    }

    public void SetXSensitivity(float xSens)
    {
        playerLook.changeSensitivity("x", xSens);
        xSensitivitySlider.value = xSens;
        xSensitivityDisplay.text = "X Sensitivity: " + (playerLook.xSensitivity).ToString("n0");
    }

    public void SetYSensitivity(float ySens)
    {
        playerLook.changeSensitivity("y", ySens);
        ySensitivitySlider.value = ySens;
        ySensitivityDisplay.text = "Y Sensitivity: " + (playerLook.ySensitivity).ToString("n0");
    }

    public void SetMouseSmoothing(float mouseSmoothing)
    {
        // ranges from 0-10 to 10-1
        mouseSmoothing += 0.01f; // to prevent divide by 0
        playerLook.mouseSmoothing = (1/mouseSmoothing)*50;
        mouseSmoothing -= 0.01f;
        mouseSmoothingSlider.value = (mouseSmoothing);
        mouseSmoothingDisplay.text = "Mouse Smoothing: " + (mouseSmoothing).ToString("n2");
    }

    public void toggleHighQuality(bool highQuality)
    {
        if (highQuality)
        {
            globalVolume.SetActive(true);
        }
        else
        {
            globalVolume.SetActive(false);
        }
    }

    public void toggleSillyMode(bool isSilly)
    {
        sillyMode.makeSilly(isSilly);
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
