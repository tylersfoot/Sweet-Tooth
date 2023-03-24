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

    IEnumerator StartDialogue()
    {
        isOpen = true;
        // dialogueBox.UpdateText("fuck you");
        dialogueBox.UpdateAuthorText("");
        dialogueBox.ActivateBox(true);
        StartCoroutine(dialogueBox.OpenAnimation());
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i <= author.Length; i++) // loops and types out the author name
        {
            yield return new WaitForSeconds(dialogueBox.typeSpeed);
            dialogueBox.UpdateAuthorText(author.Substring(0, i));
        }
        yield return new WaitForSeconds(0.2f);

        for (int x = 0; x < text.Length; x++)
        {
            for (int i = 0; i <= text[x].Length; i++)
            {
                yield return new WaitForSeconds(dialogueBox.typeSpeed);
                dialogueBox.UpdateText(text[x].Substring(0, i));
            }
            yield return new WaitForSeconds(2f); // change this to wait for input and delete below section

            for (int i = text[x].Length - 1; i >= 0; i--)
            {
                yield return new WaitForSeconds(dialogueBox.typeSpeed * 10000 * (i / text[x].Length));
                dialogueBox.UpdateText(text[x].Substring(0, i));
            }
            yield return new WaitForSeconds(0.4f);
        }
        
    }

    void EndDialogue()
    {
        isOpen = false;
        dialogueBox.UpdateText("ok fine im sorry come back");
        currentParagraph = 0;
    }
    
    protected override void Interact()
    {
        StartCoroutine(StartDialogue());
    }
}
