using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvas;
    public bool isPaused = false;

    void Start()
    {
        DisableCanvas();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Quit();
        }
    }

    void DisableCanvas()
    {
        canvas.SetActive(false);
    }

    public void Pause()
    {
        if (!isPaused)
        {
            // pauses the game
            Time.timeScale = 0f;
            canvas.SetActive(true);
            isPaused = true;
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
            // resumes the game
            Time.timeScale = 1f;
            canvas.SetActive(false);
            isPaused = false;
        }
    }

    public void SettingsButton()
    {
        Debug.Log("Settings menu opened!");
    }

    public void QuitButton()
    {
        // what happens when quit button is pressed
        Quit();
    }

    void Quit()
    {
        // closes the game without hesitation
        Application.Quit();
    }
}

