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
    [Header("Target")]
    public Vector2 mainTargetCoordinates;
    public Vector2 authorTargetCoordinates;
    public Vector2 mainTargetSize;
    public Vector2 authorTargetSize;

    [Header("Goto")]
    public Vector2 mainGotoCoordinates;
    public Vector2 authorGotoCoordinates;
    public Vector2 mainGotoSize;
    public Vector2 authorGotoSize;

    [Header("Start")]
    public Vector2 mainStartCoordinates;
    public Vector2 authorStartCoordinates;
    public Vector2 mainStartSize;
    public Vector2 authorStartSize;

    [Header("Other")]
    public RectTransform mainBoxRT;
    public RectTransform authorBoxRT;
    public float speed = 1;
    public bool t1 = false;

    void Start()
    {
        UpdateText("");
        // ActivateBox(false);

        mainGotoCoordinates = mainStartCoordinates;
        // authorGotoCoordinates = authorStartCoordinates;
        mainGotoSize = mainStartSize;
        // authorGotoSize = authorStartSize;
 
        mainBoxRT.localPosition = mainStartCoordinates;
        // authorBoxRT.position = authorStartCoordinates;
        mainBoxRT.sizeDelta = mainGotoSize;
        // authorBoxRT.sizeDelta = authorGotoSize;
    }

    void Update()
    {
        mainBoxRT.localPosition = Vector2.Lerp(mainBoxRT.localPosition, mainGotoCoordinates, Time.deltaTime * speed);
        mainBoxRT.sizeDelta = Vector2.Lerp(mainBoxRT.sizeDelta, mainGotoSize, Time.deltaTime * speed);

    }

    public IEnumerator OpenAnimation()
    {
        // targets
        // auth x -140 y 137.5 
        // auth width 200 height 25
        // main x 0 y 70
        // main width 550 height 100

        // move up
        // t1 = true;
        mainGotoCoordinates = mainTargetCoordinates;
        // yield return new WaitForSeconds(0.8f);
        authorGotoCoordinates = authorTargetCoordinates;
        mainGotoSize = mainTargetSize;
        authorGotoSize = authorTargetSize;

        
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
