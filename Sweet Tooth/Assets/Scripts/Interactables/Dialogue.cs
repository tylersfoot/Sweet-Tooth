using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Interactable
{
    private string[] text; // the text that will be read
    public string[] text1; // the different text boxes
    public string[] text2; // alt texts if needed
    public string[] text3;
    // public float currentStage; // which stage is the NPC at? for handling multiple texts
    public string author;

    public float distance; // distance from player until dialogue stops

    private bool isThisDialogueOpen;
    public DialogueBox dialogueBox;
    public GameObject player;
    private Coroutine startDialogueCoroutine;
    private Coroutine endDialogueCoroutine;
    private PlayerInteract playerInteract;
    private InputManager inputManager;

    void Start()
    {
        playerInteract = player.GetComponent<PlayerInteract>();
        inputManager = player.GetComponent<InputManager>();
    }

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
        // checks which NPC it is and does certain things
        switch (author)
        {
        case "Dereck":
            if (GameDataManager.Data.isPeanutButterShottyUnlocked)
            {
                // if gun is unlocked, skip to last dialogue
                text = text3;
            }
            else
            {
                // if you have materials, read crafting script
                if (GameDataManager.Data.inv["mosquilateSac"] >= 20 && GameDataManager.Data.inv["peanutButterToadLeg"] >= 5)
                {
                    GameDataManager.Data.inv["mosquilateSac"] -= 20;
                    GameDataManager.Data.inv["peanutButterToadLeg"] -= 5;
                    GameDataManager.Data.isPeanutButterShottyUnlocked = true;
                    text = text2;
                }
                else // if you don't, read initial script again
                {
                    text = text1;
                }
            }
            break;
        case "The Duck":
            if (GameDataManager.Data.isCaneStrikerUnlocked)
            {
                // if gun is unlocked, skip to last dialogue
                text = text3;
            }
            else
            {
                // if you have materials, read crafting script
                if (GameDataManager.Data.inv["mintyFowlLeg"] >= 15 && GameDataManager.Data.inv["snowCamelGland"] >= 3)
                {
                    GameDataManager.Data.inv["mintyFowlLeg"] -= 15;
                    GameDataManager.Data.inv["snowCamelGland"] -= 3;
                    GameDataManager.Data.isCaneStrikerUnlocked = true;
                    text = text2;
                }
                else // if you don't, read initial script again
                {
                    text = text1;
                }
            }
            break;
        default:
            text = text1;
            break;
        }
        if (dialogueBox.status == "closed")
        {
            startDialogueCoroutine = StartCoroutine(StartDialogue());
        }
    }
}
