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
            if (pauseMenu.isPaused)
            {
                pauseMenu.ResumeButton();
            }
            else
            {
                OnOpen();
            }
        }
    }

    public void OnOpen()
    {
        Time.timeScale = 0f;
        UpdateInventoryText();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.isPaused = true;
        canvas.SetActive(true);
        pauseMenu.currentScreen = "inventoryMenu";
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
        Snow Camel Glands: {GameDataManager.Data.inv["snowCamelGland"]}
        Mosquilate Sacs: {GameDataManager.Data.inv["mosquilateSac"]}
        Caramel Hoofs: {GameDataManager.Data.inv["caramelHoof"]}
        Hard-Right Lollies: {GameDataManager.Data.inv["hardRightLolli"]}
        ";

        inventoryDisplay.text = text;
    }
}
