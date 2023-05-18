using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private new Camera camera;
    public float interactDistance = 3f;
    public float crosshairDistance = 5f;
    public LayerMask interactMask;
    public LayerMask enemiesMask;
    public HUD playerHUD;
    public InputManager inputManager;
    public bool allowInteraction;
    public bool doEatAttack;
    public float eatAttackDamage;

    void Start()
    {
        camera = GetComponent<PlayerLook>().camera;
    }

    void Update()
    {
        playerHUD.UpdateText(string.Empty);
        // create a ray at center of camera shooting outwards
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.blue);
        Debug.DrawRay(ray.origin, ray.direction * crosshairDistance, Color.red);

        RaycastHit hitInfo; // variable to store collision information

        // interaction raycast
        if(Physics.Raycast(ray, out hitInfo, interactDistance, interactMask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                // if ray hits interactable object, grab interactable script
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerHUD.UpdateText(interactable.promptMessage); // show prompt

                if (inputManager.interactKeyPressed && allowInteraction)
                {
                    // call BaseInteract() on the object's script
                    interactable.BaseInteract();
                    inputManager.interactKeyPressed = false;
                }
            }
        }

        // crosshair raycast
        if(Physics.Raycast(ray, out hitInfo, crosshairDistance, enemiesMask))
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                // if ray hits enemy, change the crosshair to red
                playerHUD.crosshair.color = Color.red;
            }
        }
        else
        {
            playerHUD.crosshair.color = Color.white;
        }

        // eat attack raycast
        if(Physics.Raycast(ray, out hitInfo, crosshairDistance, enemiesMask))
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                // if ray hits enemy, and doEatAttack is true, eat the enemy
                if (doEatAttack)
                {
                    hitInfo.collider.SendMessage("Eaten", eatAttackDamage);
                    doEatAttack = false;
                }
            }
        }
    
    }
}
