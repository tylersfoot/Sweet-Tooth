using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Interactable
{
    public string[] text; // the different text boxes
    public int currentParagraph; // which text box are you on, ex: 0 is the first paragraph, 1 is the second
    public string textDisplay;
    public string author;

    public bool isOpen = false;
    public float distance;

    public DialogueBox dialogueBox;
    public GameObject player;

    void Update()
    {
        if (isOpen && Vector3.Distance(transform.position, player.transform.position) > distance)
        {
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        isOpen = true;
        dialogueBox.UpdateText("fuck you");
        dialogueBox.UpdateAuthorText(author);
        dialogueBox.ActivateBox(true);
        Debug.Log(text.Length);
    }

    void EndDialogue()
    {
        isOpen = false;
        dialogueBox.UpdateText("ok fine im sorry come back");
        currentParagraph = 0;
    }
    
    protected override void Interact()
    {
        StartDialogue();
    }
}
