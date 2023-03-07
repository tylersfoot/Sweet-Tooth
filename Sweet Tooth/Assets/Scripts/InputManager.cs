using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motorScript;
    private PlayerLook lookScript;
    private PlayerAbility abilityScript;
    public Tool toolScript;
    public PauseMenu pauseMenuScript;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motorScript = GetComponent<PlayerMotor>();
        lookScript = GetComponent<PlayerLook>();
        abilityScript = GetComponent<PlayerAbility>();
        // finds "Tools" GameObject by looking for its tag and grabs Tool.cs and other scripts
        toolScript = GameObject.FindGameObjectWithTag("ToolsTag").GetComponent<Tool>();
        pauseMenuScript = GameObject.FindGameObjectWithTag("PauseMenuTag").GetComponent<PauseMenu>();
        // jump
        onFoot.Jump.performed += ctx => motorScript.Jump();

        // toggle crouching when pressing/releasing key
        onFoot.Crouch.started += ctx => motorScript.Crouch(true);
        onFoot.Crouch.canceled += ctx => motorScript.Crouch(false);
        
        // toggle sprint when pressing/releasing key
        onFoot.Sprint.started += ctx => motorScript.Sprint(true);
        onFoot.Sprint.canceled += ctx => motorScript.Sprint(false);

        // activate ability
        onFoot.Ability.started += ctx => {
            if (!pauseMenuScript.isPaused) {
                abilityScript.ActivateAbility("sugarRush");
            }
        };

        // use tool
        onFoot.UseToolPrimary.started += ctx => {
            if (!pauseMenuScript.isPaused) {
                toolScript.Use(true);
            }
        };
        onFoot.UseToolPrimary.canceled += ctx => {
            if (!pauseMenuScript.isPaused) {
                toolScript.Use(false);
            }
        };
        // pause game
        onFoot.Pause.started += ctx => pauseMenuScript.Pause();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // tell the playermotor to move using the value from our movement action
        motorScript.ProcessMove(onFoot.Movement.ReadValue<Vector2>());

        
    }
    void LateUpdate()
    {
        // tell the playermotor to look using the value from our look action
        lookScript.ProcessLook(onFoot.Look.ReadValue<Vector2>());
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
