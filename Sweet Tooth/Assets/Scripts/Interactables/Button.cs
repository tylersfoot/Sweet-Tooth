using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // this function is where we design the interaction using code
    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
