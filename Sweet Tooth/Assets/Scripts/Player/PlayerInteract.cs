using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private new Camera camera;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    public HUD playerHUD;
    public InputManager inputManager;
    public bool allowInteraction;

    void Start()
    {
        camera = GetComponent<PlayerLook>().camera;
        
    }

    void Update()
    {
        playerHUD.UpdateText(string.Empty);
        // create a ray at center of camera shooting outwards
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);   
        RaycastHit hitInfo; // variable to store collision information

        if(Physics.Raycast(ray, out hitInfo, distance, mask))
        {

            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerHUD.UpdateText(interactable.promptMessage);

                if (inputManager.interactKeyPressed && allowInteraction)
                {
                    interactable.BaseInteract();
                    inputManager.interactKeyPressed = false;
                }

            }

        }
    
    }
}
