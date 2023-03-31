using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI reasonText; // displaying what killed the player
    public PauseMenu pauseMenu; // currentScreen
    public PlayerStats playerStats; // for checking if player is dead
    public string deathReason; // for storing death reason
    public string deathDisplay; // for displaying death reason

    void Start()
    {
        canvas.SetActive(false);
    }

    public void Death(string reason)
    {
        canvas.SetActive(true);
        deathReason = reason;
        pauseMenu.currentScreen = "gameOverMenu";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // display death reason (get creative)
        switch (deathReason)
        {
        case "damageCube":
            deathDisplay = "You pressed the cube too many times, huh? Curiosity killed the cat... and you.";
            break;
        case "crazyCorn":
            deathDisplay = "The crazy corn ate you. Good job.";
            break;
        case "gummyBear":
            deathDisplay = "The gummy bear hugged you to death!";
            break;
        case "mintyFowl":
            deathDisplay = "The minty fowl's breath was so fresh, it gave you a brain freeze... and death.";
            break;
        case "peanutButterToad":
            deathDisplay = "The peanut butter toad's tongue was too sticky, and you couldn't get away.";
            break;
        default:
            deathDisplay = "You died from... something? We're... not really sure.";
            break;
        }

        reasonText.text = deathDisplay;
    }

    public void RespawnButton()
    {
        // restart scene for now
        SceneManager.LoadScene("Game");
    }

    public void MainMenuButton()
    {
        // load main menu
        SceneManager.LoadScene("MainMenu");
    }
}

