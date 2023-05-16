using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour
{
    public TextMeshProUGUI inventoryDisplay;
    public PauseMenu pauseMenu;
    public GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
    }

    public void OpenInventory()
    {
        if (pauseMenu.currentScreen != "gameOverMenu")
        {
            if (!pauseMenu.isPaused)
            {
                // pauses the game and opens inventory
                Time.timeScale = 0f;
                canvas.SetActive(true);
                pauseMenu.isPaused = true;
                pauseMenu.currentScreen = "inventoryMenu";
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pauseMenu.ResumeButton();
            }
        }

        UpdateInventoryText();
    }

    public void Back()
    {
        if (pauseMenu.currentScreen == "inventoryMenu")
        {
            pauseMenu.currentScreen = "pauseMenu";
            canvas.SetActive(false);
            pauseMenu.canvas.SetActive(true);
        }
    }

    void UpdateInventoryText()
    {
        string text = $@"
        Crazy Corn Chunks: {GameDataManager.Data.inv["crazyCornChunk"]}
        Gummy Bear Heads: {GameDataManager.Data.inv["gummyBearHead"]}
        Minty Fowl Legs: {GameDataManager.Data.inv["mintyFowlLeg"]}
        Peanut Butter Toad Legs: {GameDataManager.Data.inv["peanutButterToadLeg"]}
        Snow Camel Parts: {GameDataManager.Data.inv["snowCamelPart"]}
        Mosquito Parts: {GameDataManager.Data.inv["mosquitoPart"]}
        ";

        inventoryDisplay.text = text;
    }
}
