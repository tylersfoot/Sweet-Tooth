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
    public InventoryMenu inventoryMenu;
    public PlayerInteract playerInteract;
    public DialogueBox dialogueBox;

    public bool interactKeyPressed;
    public bool stopInput; // stop inputs all together
    public bool stopMove; // stop movement, including sprint/crouch/jump
    public bool stopLook; // stop looking around

    void Update()
    {
        if (pauseMenu.isPaused || pauseMenu.currentScreen == "gameOverMenu")
        {
            stopInput = true;
            stopMove = true;
            stopLook = true;
        }
        else if (dialogueBox.isOpen)
        {
            // stopInput = false;
            // stopMove = false;
            // stopLook = false;
        }
        else
        {
            stopInput = false;
            stopMove = false;
            stopLook = false;
        }

        // tell the playermotor to move using the value from our movement action
        if (!stopMove)
        {
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        }
    }

    void LateUpdate()
    {
        // tell the playermotor to look using the value from our look action
        if (!stopLook)
        {
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }
    }

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        ability = GetComponent<PlayerAbility>();

        onFoot.Test.started += ctx => {
            pauseMenu.LoadButton();
        };
    
        // jump
        onFoot.Jump.started += ctx => {
            if (!stopInput) {
                motor.Jump();
            }
        };

        // toggle crouching when pressing/releasing key
        onFoot.Crouch.started += ctx => {
            if (!stopInput) {
                motor.Crouch(true);
            }
        };
        onFoot.Crouch.canceled += ctx => {
            if (!stopInput) {
                motor.Crouch(false);
            }
        };
        
        // toggle sprint when pressing/releasing key
        onFoot.Sprint.started += ctx => {
            if (!stopInput) {
                motor.Sprint(true);
            }
        };
        onFoot.Sprint.canceled += ctx => {
            if (!stopInput) {
                motor.Sprint(false);
            }
        };
        
        onFoot.Interact.started += ctx => {
            if (!stopInput) {
                interactKeyPressed = true;
            }
        };

        onFoot.Interact.canceled += ctx => {
            if (!stopInput) {
                interactKeyPressed = false;
            }
        };

        // activate ability
        onFoot.Ability.started += ctx => {
            if (!stopInput) {
                ability.ActivateAbility("sugarRush");
            }
        };

        // use tool
        onFoot.UseToolPrimary.started += ctx => {
            if (!stopInput) {
                tool.Use(true);
            }
        };
        onFoot.UseToolPrimary.canceled += ctx => {
            if (!stopInput) {
                tool.Use(false);
            }
        };

        onFoot.Reload.started += ctx => {
            if (!stopInput) {
                tool.Reload();
            }
        };

        // switch tools using scroll wheel
        onFoot.Scroll.performed += ctx => {
            if (!stopInput) {
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
            if (!stopInput) {
                tool.SwitchTool(1);
            }
        };
        onFoot.ToolTwo.started += ctx => {
            if (!stopInput) {
                tool.SwitchTool(2);
            }
        };
        onFoot.ToolThree.started += ctx => {
            if (!stopInput) {
                tool.SwitchTool(3);
            }
        };

        // pause game
        onFoot.Pause.started += ctx => pauseMenu.Pause();

        // open inventory
        onFoot.Inventory.started += ctx => inventoryMenu.OpenInventory();
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
