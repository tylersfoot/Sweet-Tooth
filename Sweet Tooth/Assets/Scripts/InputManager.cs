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
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motorScript = GetComponent<PlayerMotor>();
        lookScript = GetComponent<PlayerLook>();
        abilityScript = GetComponent<PlayerAbility>();
        // finds "Tools" GameObject by looking for its tag and grabs Tool.cs
        toolScript = GameObject.FindGameObjectWithTag("ToolsTag").GetComponent<Tool>();

        onFoot.Jump.performed += ctx => motorScript.Jump();

        // toggle crouching when pressing/releasing key
        onFoot.Crouch.performed += ctx => motorScript.Crouch(true);
        onFoot.Crouch.canceled += ctx => motorScript.Crouch(false);
        
        // toggle sprint when pressing/releasing key
        onFoot.Sprint.started += ctx => motorScript.Sprint(true);
        onFoot.Sprint.canceled += ctx => motorScript.Sprint(false);

        // activate ability
        onFoot.Ability.started += ctx => abilityScript.ActivateAbility("sugarRush");

        // use tool
        onFoot.UseToolPrimary.started += ctx => toolScript.Use();
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
