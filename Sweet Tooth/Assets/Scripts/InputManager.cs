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
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        ability = GetComponent<PlayerAbility>();
    
        // jump
        onFoot.Jump.performed += ctx => motor.Jump();

        // toggle crouching when pressing/releasing key
        onFoot.Crouch.started += ctx => motor.Crouch(true);
        onFoot.Crouch.canceled += ctx => motor.Crouch(false);
        
        // toggle sprint when pressing/releasing key
        onFoot.Sprint.started += ctx => motor.Sprint(true);
        onFoot.Sprint.canceled += ctx => motor.Sprint(false);

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
        // pause game
        onFoot.Pause.started += ctx => pauseMenu.Pause();
    }

    // Update is called once per frame
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
