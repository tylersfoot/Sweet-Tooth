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
    public Music music;
    public SoundManager soundManager;
    public GameObject globalVolume;

    public AudioSource crazyCornAudio;
    
    public Slider xSensitivitySlider;
    public TextMeshProUGUI xSensitivityDisplay;
    public Slider ySensitivitySlider;
    public TextMeshProUGUI ySensitivityDisplay;
    public Slider mouseSmoothingSlider;
    public TextMeshProUGUI mouseSmoothingDisplay;
    public Slider MusicSlider;
    public TextMeshProUGUI MusicDisplay;
    public Slider SoundsSlider;
    public TextMeshProUGUI SoundsDisplay;

    void Start()
    {
        canvas.SetActive(false);
        SetXSensitivity(GameDataManager.Data.xSensitivity);
        SetYSensitivity(GameDataManager.Data.ySensitivity);
        SetMouseSmoothing(50/GameDataManager.Data.mouseSmoothing);
        toggleHighQuality(GameDataManager.Data.highQuality);
        toggleSillyMode(GameDataManager.Data.isSilly);
        toggleFPSCounter(GameDataManager.Data.fpsCounter);
        SetMusicVolume(GameDataManager.Data.musicVolume);
        SetSoundsVolume(GameDataManager.Data.soundsVolume);
    }

    public void SetXSensitivity(float xSens)
    {
        playerLook.changeSensitivity("x", xSens);
        xSensitivitySlider.value = xSens;
        xSensitivityDisplay.text = "X Sensitivity: " + (GameDataManager.Data.xSensitivity).ToString("n0");
    }

    public void SetYSensitivity(float ySens)
    {
        playerLook.changeSensitivity("y", ySens);
        ySensitivitySlider.value = ySens;
        ySensitivityDisplay.text = "Y Sensitivity: " + (GameDataManager.Data.ySensitivity).ToString("n0");
    }

    public void SetMouseSmoothing(float mouseSmoothing)
    {
        // ranges from 0-10 to 10-1
        mouseSmoothing += 0.01f; // to prevent divide by 0
        GameDataManager.Data.mouseSmoothing = (1/mouseSmoothing)*50;
        mouseSmoothing -= 0.01f;
        mouseSmoothingSlider.value = (mouseSmoothing);
        mouseSmoothingDisplay.text = "Mouse Smoothing: " + (mouseSmoothing).ToString("n2");
    }

    public void toggleHighQuality(bool highQuality)
    {
        GameDataManager.Data.highQuality = highQuality;
        globalVolume.SetActive(GameDataManager.Data.highQuality);
    }

    public void toggleSillyMode(bool isSilly)
    {
        GameDataManager.Data.isSilly = isSilly;
        sillyMode.makeSilly(GameDataManager.Data.isSilly);
    }

    public void toggleFPSCounter(bool fpsCounter)
    {
        GameDataManager.Data.fpsCounter = fpsCounter;
        // settings or something
    }

    public void SetMusicVolume(float volume)
    {
        GameDataManager.Data.musicVolume = volume;
        MusicDisplay.text = "Music Volume: " + (GameDataManager.Data.musicVolume*100).ToString("n0") + "%";
    }

    public void SetSoundsVolume(float volume)
    {
        GameDataManager.Data.soundsVolume = volume;
        if (crazyCornAudio != null)
        {
            crazyCornAudio.volume = GameDataManager.Data.soundsVolume;
        }
        
        SoundsDisplay.text = "Sounds Volume: " + (GameDataManager.Data.soundsVolume*100).ToString("n0") + "%";
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
