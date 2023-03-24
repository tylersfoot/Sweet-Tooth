using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerAbility ability;
    public Tool tool;
    public PauseMenu pauseMenu;
    public PlayerInteract playerInteract;
    public DialogueBox dialogueBox;

    public bool interactKeyPressed;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        ability = GetComponent<PlayerAbility>();
    
        // jump
        onFoot.Jump.started += ctx => {
            if (!pauseMenu.isPaused) {
                motor.Jump();
            }
        };

        // toggle crouching when pressing/releasing key
        onFoot.Crouch.started += ctx => {
            if (!pauseMenu.isPaused) {
                motor.Crouch(true);
            }
        };
        onFoot.Crouch.canceled += ctx => {
            if (!pauseMenu.isPaused) {
                motor.Crouch(false);
            }
        };
        
        // toggle sprint when pressing/releasing key
        onFoot.Sprint.started += ctx => {
            if (!pauseMenu.isPaused) {
                motor.Sprint(true);
            }
        };
        onFoot.Sprint.canceled += ctx => {
            if (!pauseMenu.isPaused) {
                motor.Sprint(false);
            }
        };
        
        onFoot.Interact.started += ctx => {
            if (!pauseMenu.isPaused) {
                interactKeyPressed = true;
            }
        };

        onFoot.Interact.canceled += ctx => {
            if (!pauseMenu.isPaused) {
                interactKeyPressed = false;
            }
        };

        // activate ability
        onFoot.Ability.started += ctx => {
            if (!pauseMenu.isPaused) {
                ability.ActivateAbility("sugarRush");
            }
        };

        // use tool
        onFoot.UseToolPrimary.started += ctx => {
            if (!pauseMenu.isPaused) {
                tool.Use(true);
            }
        };
        onFoot.UseToolPrimary.canceled += ctx => {
            if (!pauseMenu.isPaused) {
                tool.Use(false);
            }
        };

        onFoot.Reload.started += ctx => {
            if (!pauseMenu.isPaused) {
                tool.Reload();
            }
        };

        // switch tools using scroll wheel
        onFoot.Scroll.performed += ctx => {
            if (!pauseMenu.isPaused) {
                float scrollValue = ctx.ReadValue<float>();
                if (scrollValue > 0) {
                    // scroll up - switch to the next tool in order
                    tool.SwitchTool(tool.activeToolIndex + 1);
                } else if (scrollValue < 0) {
                    // scroll down - switch to the previous tool in order
                    tool.SwitchTool(tool.activeToolIndex - 1);
                }
            }
        };

        // switch tools using keyboard
        onFoot.ToolOne.started += ctx => {
            if (!pauseMenu.isPaused) {
                tool.SwitchTool(1);
            }
        };
        onFoot.ToolTwo.started += ctx => {
            if (!pauseMenu.isPaused) {
                tool.SwitchTool(2);
            }
        };
        onFoot.ToolThree.started += ctx => {
            if (!pauseMenu.isPaused) {
                tool.SwitchTool(3);
            }
        };
        // pause game
        onFoot.Pause.started += ctx => pauseMenu.Pause();
    }

    void FixedUpdate()
    {
        // tell the playermotor to move using the value from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());

        
    }
    void LateUpdate()
    {
        // tell the playermotor to look using the value from our look action
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
