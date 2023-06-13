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
        case "snowCamel":
            deathDisplay = "The snow camel trampled you under its icy hooves.";
            break;
        case "mosquilate":
            deathDisplay = "The mosquilate drained you dry and left you lifeless.";
            break;
        case "caramelClops":
            deathDisplay = "The caramel clops stomped on you, leaving a sticky and sweet demise.";
            break;
        case "suckerpunch":
            deathDisplay = "The suckerpunch knocked you out cold... permanently.";
            break;
        case "damageCube":
            deathDisplay = "You pressed the cube too many times, huh? Curiosity killed the cat... and you.";
            break;
        case "mrKahooBadoo":
            deathDisplay = "Mr. Kahoo Badoo's dance moves were too much for you to handle.";
            break;
        case "tylersfoot":
            deathDisplay = "You were smited by tyler's foot. You should have known better.";
            break;
        case "Dr. Bixler":
            deathDisplay = "Dr. Bixler's experiments caused atoms in your brain to split, creating a nuclear explosion and killing millions around you.";
            break;
        default:
            // when there was no death reason
            deathDisplay = "You died from... something? We're... not really sure.";
            break;
        }

        reasonText.text = deathDisplay;
    }

    public void RespawnButton()
    {
        // restart scene for now
        GameDataManager.Data.comingFromSave = true;
        GameDataManager.SaveData();
        Debug.Log("Saved Data!");
        SceneManager.LoadScene("Game");
    }

    public void MainMenuButton()
    {
        // load main menu
        GameDataManager.SaveData();
        Debug.Log("Saved Data!");
        SceneManager.LoadScene("MainMenu");
    }
}

