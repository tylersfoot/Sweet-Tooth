using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public GameObject canvas;

    void Start()
    {
        UpdateText("");
        ActivateBox(false);
    }

    public void UpdateText(string promptMessage)
    {
        mainText.text = promptMessage;
    }

    public void ActivateBox(bool active)
    {
        canvas.SetActive(active);
    }
}
