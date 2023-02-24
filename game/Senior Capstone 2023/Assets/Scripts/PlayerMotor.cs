using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public Camera camera;
    private bool isGrounded;
    public bool lerpCrouch;
    public bool crouching;
    public bool sprinting;
    public float crouchTimer;
    public float speed = 5f;
    public float walkSpeed = 5f;
    public float crouchSpeed = 2f;
    public float sprintSpeed = 8f;
    public float walkFOV = 60f;
    public float sprintFOV = 80f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    // recieve the inputs for our InputManager.cs and apply them to our character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }
    
    public void Jump()
    {
        if(isGrounded)
        {
           playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        if (crouching)
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        // toggle sprinting
        sprinting = !sprinting;
        if (sprinting)
        {
            // increase speed and camera FOV
            Camera.main.fieldOfView = sprintFOV;
            speed = sprintSpeed;
        }
        else
        {
            // decrease speed and camera FOV
            Camera.main.fieldOfView = walkFOV;
            speed = walkSpeed;
        }
    }
}
