using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvas;
    public SettingsMenu settingsMenu; // for changing screens
    public bool isPaused = false;
    public string currentScreen = "none"; // which screen is the player seeing

    void Start()
    {
        canvas.SetActive(false);
    }

    void Update()
    {
        // backspace pressed, close game
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.Quit();
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            // pauses the game
            Time.timeScale = 0f;
            canvas.SetActive(true);
            isPaused = true;
            currentScreen = "pauseMenu";
        }
        else
        {
            ResumeButton();
        }
    }

    public void ResumeButton()
    {
        if (isPaused)
        {
           
            settingsMenu.Back(); // closes settings screen if open
            Time.timeScale = 1f; // resumes time
            canvas.SetActive(false); // disables pause menu canvas
            isPaused = false;
            currentScreen = "none";
        }
    }

    public void SettingsButton()
    {
        if (currentScreen == "pauseMenu")
        {
            currentScreen = "settingsMenu";
            canvas.SetActive(false);
            settingsMenu.canvas.SetActive(true);
        }
    }

    public void QuitButton()
    {
        // closes the game without hesitation
        Application.Quit();
    }
}

