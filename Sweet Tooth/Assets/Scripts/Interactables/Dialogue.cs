using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Interactable
{
    public string[] text; // the different text boxes
    public int currentParagraph; // which text box are you on, ex: 0 is the first paragraph, 1 is the second
    public string textDisplay;
    public string author;

    public float distance;

    private bool isThisDialogueOpen;
    public DialogueBox dialogueBox;
    public GameObject player;
    private Coroutine startDialogueCoroutine;
    private Coroutine endDialogueCoroutine;
    public PlayerInteract playerInteract;
    public InputManager inputManager;

    void Update()
    {
        if (dialogueBox.status != "closed" && Vector3.Distance(transform.position, player.transform.position) > distance && isThisDialogueOpen)
        {
            endDialogueCoroutine = StartCoroutine(EndDialogue());
        }
    }

    IEnumerator StartDialogue()
    {
        playerInteract.allowInteraction = false; // can't interact with anything else while in dialogue
        dialogueBox.UpdateAuthorText("");
        dialogueBox.ActivateBox(true);
        isThisDialogueOpen = true;
        StartCoroutine(dialogueBox.OpenAnimation());
        yield return new WaitForSeconds(0.10f * dialogueBox.speed);

        for (int i = 0; i <= author.Length; i++) // loops and types out the author name
        {
            yield return new WaitForSeconds(dialogueBox.typeSpeed);
            dialogueBox.UpdateAuthorText(author.Substring(0, i));
        }

        yield return new WaitForSeconds(0.025f * dialogueBox.speed);

        for (int x = 0; x < text.Length; x++) // loops through each paragraph
        {
            for (int i = 0; i <= text[x].Length; i++) // loops and types out the text
            {
                if (inputManager.interactKeyPressed) // if interact key pressed, skip typing
                {
                    i = text[x].Length;
                    inputManager.interactKeyPressed = false; // prevents double skip
                }
                yield return new WaitForSeconds(dialogueBox.typeSpeed);
                dialogueBox.UpdateText(text[x].Substring(0, i));
            }

            // waits before changing paragraphs
            // yield return new WaitForSeconds(0.25f * dialogueBox.speed);
            float accumulatedTime = 0;
            while (!inputManager.interactKeyPressed && accumulatedTime < (0.25f * dialogueBox.speed))
            {
                accumulatedTime += Time.deltaTime;
                yield return null;
            }
            inputManager.interactKeyPressed = false; // prevents double skip
        } 
        endDialogueCoroutine = StartCoroutine(EndDialogue());
    }

    IEnumerator EndDialogue()
    {
        dialogueBox.isOpen = false;
        isThisDialogueOpen = false;
        playerInteract.allowInteraction = true;
        currentParagraph = 0;
        if (startDialogueCoroutine != null)
        {   
            StopCoroutine(startDialogueCoroutine);
        }
        yield return new WaitForSeconds(0.1f);
        dialogueBox.UpdateText("");
        dialogueBox.UpdateAuthorText("");
        Debug.LogWarning("NOOO WHAT THE FUCK DID YOU DO");

        StartCoroutine(dialogueBox.CloseAnimation());    
    }

    protected override void Interact()
    {
        if (dialogueBox.status == "closed")
        {
            startDialogueCoroutine = StartCoroutine(StartDialogue());
        }
    }
}
