using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Interactable
{
    public string[] text;
    public bool isOpen = false;
    public float distance;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

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
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = Vector3.zero; // Set position of sphere to the origin
        sphere.transform.localScale = new Vector3(3, 3, 3); // Set scale of sphere
    }

    void EndDialogue()
    {
        isOpen = false;
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = Vector3.zero; // Set position of sphere to the origin
        sphere.transform.localScale = new Vector3(10, 10, 10); // Set scale of sphere
    }
    
    protected override void Interact()
    {
        StartDialogue();
    }
}
