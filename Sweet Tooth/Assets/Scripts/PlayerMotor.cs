using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public new Camera camera;
    private bool isGrounded;
    public bool lerpCrouch;
    public bool crouching;
    public bool sprinting;
    public float crouchTimer;
    public float speed = 5f; // current speed
    public float walkSpeed = 5f; // walking speed
    public float crouchSpeed = 2f; // crouching speed
    public float sprintSpeed = 8f; // sprinting speed
    private float fovVelocity = 0f;
    public float walkFOV = 60f; // walking FOV
    public float sprintFOV = 80f; // sprinting FOV
    public float fovDampTime = 0.1f; // time to reach target FOV
    private float currentFOV; // current FOV
    public float gravity = -9.8f; // gravity acceleration
    public float jumpHeight = 1.5f; // jump height

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
        // Debug.Log(playerVelocity.y);
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
            // increase speed and smoothly change camera FOV
            currentFOV = Camera.main.fieldOfView;
            speed = sprintSpeed;
        }
        else
        {
            // decrease speed and smoothly change camera FOV
            currentFOV = Camera.main.fieldOfView;
            speed = walkSpeed;
        }
    }
    
    void LateUpdate()
    {
        if (sprinting)
        {
            // smoothly interpolate FOV towards target FOV
            float targetFOV = sprintFOV;
            currentFOV = Mathf.SmoothDamp(currentFOV, targetFOV, ref fovVelocity, fovDampTime);
            Camera.main.fieldOfView = currentFOV;
        }
        else
        {
            // smoothly interpolate FOV towards target FOV
            float targetFOV = walkFOV;
            currentFOV = Mathf.SmoothDamp(currentFOV, targetFOV, ref fovVelocity, fovDampTime);
            Camera.main.fieldOfView = currentFOV;
        }
    }
}
