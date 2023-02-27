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
    private bool sprintKeyDown;
    public float crouchHeight = 1f; // crouch height
    public float standHeight = 2f; // standing height
    public float crouchHeightVelocity = 0f;
    public float crouchHeightSmoothTime = 0.1f;
    public float speed = 5f; // current speed
    public float targetSpeed = 5f; // target speed
    public float walkSpeed = 5f; // walking speed
    public float crouchSpeedFactor = 0.6f; // crouching speed factor
    public float sprintSpeedFactor = 1.4f; // sprinting speed factor
    private float fovVelocity = 0f;
    public float walkFOV = 60f; // walking FOV
    public float sprintFOVFactor = 1.33f; // sprinting FOV factor
    public float fovDampTime = 0.1f; // time to reach target FOV
    private float currentFOV; // current FOV
    public float gravity = -30f; // gravity acceleration
    public float jumpHeight = 1.5f; // jump height
    public float targetJumpHeight = 1.5f;
    public float walkJumpHeight = 1.5f; // target jump height
    public float crouchJumpHeightFactor = 0.8f;
    public float sprintJumpHeightFactor = 1.2f;
    private Vector3 targetScale = Vector3.one; // target player scale

    // called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // called once per frame
    void Update()
    {
        // changes speed and jump height based on state
        targetSpeed = walkSpeed;
        targetJumpHeight = walkJumpHeight;
        if (crouching)
        {
            targetSpeed = walkSpeed * crouchSpeedFactor;
            targetJumpHeight = walkJumpHeight * crouchJumpHeightFactor;
        }
        if (sprinting)
        {
            targetSpeed = walkSpeed * sprintSpeedFactor;
            targetJumpHeight = walkJumpHeight * sprintJumpHeightFactor;
        }
        speed = targetSpeed;
        jumpHeight = targetJumpHeight;
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            float targetHeight = crouching ? crouchHeight : standHeight; // sets the target height
            controller.height = Mathf.SmoothDamp(controller.height, targetHeight, ref crouchHeightVelocity, crouchHeightSmoothTime);
            targetScale.y = controller.height / standHeight;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 10f);
            if (Mathf.Abs(controller.height - targetHeight) < 0.01f)
            {
                lerpCrouch = false;
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

    public void Crouch(bool key)
    {  
        // stop sprinting if crouch
        if (sprinting)
        {
            Sprint(false);
            sprintKeyDown = true;
        }
        crouching = !crouching;
        if (crouching)
        {
            targetScale.y = crouchHeight / standHeight;
        }
        else
        {
            if (sprintKeyDown)
            {
                Sprint(true);
            }
            targetScale.y = 1f;
        }
        lerpCrouch = true;
    }

    public void Sprint(bool key)
    {
        sprintKeyDown = key;
        // dont sprint if crouching
        if (!crouching && key)
        {
            sprinting = true;
        }
        else
        {
            sprinting = false;
        }
        // smoothly change camera FOV
        currentFOV = Camera.main.fieldOfView;
    }
    
    void LateUpdate()
    {
        float targetFOV = walkFOV;
        if (sprinting)
        {
            targetFOV = walkFOV * sprintFOVFactor;
        }
        // smoothly interpolate FOV towards target FOV
        currentFOV = Mathf.SmoothDamp(currentFOV, targetFOV, ref fovVelocity, fovDampTime);
        Camera.main.fieldOfView = currentFOV;
    }
}
