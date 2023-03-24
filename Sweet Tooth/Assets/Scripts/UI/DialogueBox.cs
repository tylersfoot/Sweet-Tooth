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
    public float speed;
    public float typeSpeed;

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
        mainGotoCoordinates = mainTargetCoordinates; // move up
        authorGotoCoordinates.y = authorTargetCoordinates.y-50; // move up with an offset so its not visible
        yield return new WaitForSeconds(0.4f);
        mainGotoSize.x = mainTargetSize.x; // widen out
        authorGotoCoordinates.x = authorTargetCoordinates.x; // widen out
        yield return new WaitForSeconds(0.3f);
        authorGotoCoordinates.y = authorTargetCoordinates.y; // pop out the top
        yield return new WaitForSeconds(0.4f);
        authorGotoSize.x = authorTargetSize.x; // widen out author box
        
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
