using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI authorText;
    public GameObject mainBox;
    public GameObject authorBox;
    public GameObject canvas;

    // animation stuff
    [Header("Target")] // the end coordinates
    public Vector2 mainTargetCoordinates;
    public Vector2 authorTargetCoordinates;
    public Vector2 mainTargetSize;
    public Vector2 authorTargetSize;

    [Header("Goto")] // the coordinates that the objects go to (this is changed)
    public Vector2 mainGotoCoordinates;
    public Vector2 authorGotoCoordinates;
    public Vector2 mainGotoSize;
    public Vector2 authorGotoSize;

    [Header("Start")] // starting coordinates, offscreen
    public Vector2 mainStartCoordinates;
    public Vector2 authorStartCoordinates;
    public Vector2 mainStartSize;
    public Vector2 authorStartSize;

    [Header("Other")]
    public RectTransform mainBoxRT;
    public RectTransform authorBoxRT;
    public float speed;
    public float typeSpeed;
    public InputManager inputManager;
    public GameObject hudCanvas;
    public bool isOpen;
    public bool chickenMcFlicken;

    void Start()
    {
        UpdateText("");
        // ActivateBox(false);

        mainGotoCoordinates = mainStartCoordinates;
        authorGotoCoordinates = authorStartCoordinates;
        mainGotoSize = mainStartSize;
        authorGotoSize = authorStartSize;
 
        mainBoxRT.localPosition = mainStartCoordinates;
        authorBoxRT.localPosition = authorStartCoordinates;
        mainBoxRT.sizeDelta = mainGotoSize;
        authorBoxRT.sizeDelta = authorGotoSize;
    }

    void Update()
    {
        mainBoxRT.localPosition = Vector2.Lerp(mainBoxRT.localPosition, mainGotoCoordinates, Time.deltaTime * speed);
        mainBoxRT.sizeDelta = Vector2.Lerp(mainBoxRT.sizeDelta, mainGotoSize, Time.deltaTime * speed);
        authorBoxRT.localPosition = Vector2.Lerp(authorBoxRT.localPosition, authorGotoCoordinates, Time.deltaTime * speed);
        authorBoxRT.sizeDelta = Vector2.Lerp(authorBoxRT.sizeDelta, authorGotoSize, Time.deltaTime * speed);

    }

    public IEnumerator OpenAnimation()
    {
        hudCanvas.SetActive(false);
        mainGotoCoordinates = mainTargetCoordinates; // move up
        authorGotoCoordinates.y = authorTargetCoordinates.y-50; // move up with an offset so its not visible
        yield return new WaitForSeconds(0.3f);
        mainGotoSize.x = mainTargetSize.x; // widen out
        authorGotoCoordinates.x = authorTargetCoordinates.x; // move left
        yield return new WaitForSeconds(0.2f);
        authorGotoCoordinates.y = authorTargetCoordinates.y; // pop out the top
        yield return new WaitForSeconds(0.3f);
        authorGotoSize.x = authorTargetSize.x; // widen out author box
    }

    public IEnumerator CloseAnimation()
    {
        hudCanvas.SetActive(true);
        authorGotoSize.x = authorStartSize.x; // unwiden out author box
        yield return new WaitForSeconds(0.3f);
        authorGotoCoordinates.y = authorTargetCoordinates.y-50; // make the top come back in
        yield return new WaitForSeconds(0.2f);
        authorGotoCoordinates.x = authorStartCoordinates.x; // move right
        mainGotoSize.x = mainStartSize.x; // unwiden out
        yield return new WaitForSeconds(0.3f);
        authorGotoCoordinates.y = authorStartCoordinates.y; // move down
        mainGotoCoordinates = mainStartCoordinates; // move down 
    }

    public void UpdateText(string text)
    {
        mainText.text = text;
    }

    public void UpdateAuthorText(string text)
    {
        authorText.text = text;
    }

    public void ActivateBox(bool active)
    {
        canvas.SetActive(active);
    }
}
