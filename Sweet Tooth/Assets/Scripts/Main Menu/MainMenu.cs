using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public float speed;

    public Image loadSaveBar;
    public float loadSaveFillTarget;
    public float loadSaveFill;

    public Image newGameBar;
    public float newGameFillTarget;
    public float newGameFill;

    public Image controlsBar;
    public float controlsFillTarget;
    public float controlsFill;

    public Image creditsBar;
    public float creditsFillTarget;
    public float creditsFill;

    public Image quitBar;
    public float quitFillTarget;
    public float quitFill;

    void Update()
    {
        // lerps the bar when you hover over an option
        loadSaveFill = Mathf.Lerp(loadSaveFill, loadSaveFillTarget, speed * Time.deltaTime);
        loadSaveBar.fillAmount = loadSaveFill;

        newGameFill = Mathf.Lerp(newGameFill, newGameFillTarget, speed * Time.deltaTime);
        newGameBar.fillAmount = newGameFill;

        controlsFill = Mathf.Lerp(controlsFill, controlsFillTarget, speed * Time.deltaTime);
        controlsBar.fillAmount = controlsFill;

        creditsFill = Mathf.Lerp(creditsFill, creditsFillTarget, speed * Time.deltaTime);
        creditsBar.fillAmount = creditsFill;
        
        quitFill = Mathf.Lerp(quitFill, quitFillTarget, speed * Time.deltaTime);
        quitBar.fillAmount = quitFill;
    }
 
    public void LoadSaveButton()
    {

    }

    public void NewGameButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ControlsButton()
    {

    }

    public void CreditsButton()
    {

    }

    public void QuitButton()
    {
        // closes game
        Application.Quit();
    }

    public void LoadSaveHoverEnter()
    {
        loadSaveFillTarget = 1;
    }
    public void LoadSaveHoverExit()
    {
        loadSaveFillTarget = 0;
    }

    public void NewGameHoverEnter()
    {
        newGameFillTarget = 1;
    }
    public void NewGameHoverExit()
    {
        newGameFillTarget = 0;
    }

    public void ControlsHoverEnter()
    {
        controlsFillTarget = 1;
    }
    public void ControlsHoverExit()
    {
        controlsFillTarget = 0;
    }

    public void CreditsHoverEnter()
    {
        creditsFillTarget = 1;
    }
    public void CreditsHoverExit()
    {
        creditsFillTarget = 0;
    }

    public void QuitHoverEnter()
    {
        quitFillTarget = 1;
    }
    public void QuitHoverExit()
    {
        quitFillTarget = 0;
    }
}
