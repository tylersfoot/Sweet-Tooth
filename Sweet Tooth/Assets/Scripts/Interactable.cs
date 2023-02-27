using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage; // message displayed when looking at interactable
    
    // function called from our player
    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {
        // no code here, template function, overridden by subclasses
    }
}
