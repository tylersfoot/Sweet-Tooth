using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI authorText;
    public GameObject canvas;

    void Start()
    {
        UpdateText("");
        ActivateBox(false);
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
