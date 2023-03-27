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
    private Coroutine startDialogueCoroutine;
    private Coroutine endDialogueCoroutine;
    public PlayerInteract playerInteract;
    public InputManager inputManager;

    void Update()
    {
        if (isOpen && Vector3.Distance(transform.position, player.transform.position) > distance)
        {
            endDialogueCoroutine = StartCoroutine(EndDialogue());
        }
    }

    IEnumerator StartDialogue()
    {
        isOpen = true;
        playerInteract.allowInteraction = false; // can't interact with anything else while in dialogue
        dialogueBox.UpdateAuthorText("");
        dialogueBox.ActivateBox(true);
        StartCoroutine(dialogueBox.OpenAnimation());
        yield return new WaitForSeconds(0.19f * dialogueBox.speed);

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
        isOpen = false;
        playerInteract.allowInteraction = true;
        currentParagraph = 0;
        if (startDialogueCoroutine != null)
        {   
            StopCoroutine(startDialogueCoroutine);
        }
        yield return new WaitForSeconds(0.1f);
        dialogueBox.UpdateText("");
        dialogueBox.UpdateAuthorText("");

        StartCoroutine(dialogueBox.CloseAnimation());    
    }

    protected override void Interact()
    {
        startDialogueCoroutine = StartCoroutine(StartDialogue());
    }
}
