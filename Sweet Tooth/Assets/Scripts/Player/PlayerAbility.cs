using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerAbility : MonoBehaviour
{
    public PlayerMotor playerMotor;
    private PostProcessLayer postProcessLayer;
    public float abilityDuration = 3f; // default ability length in seconds
    public bool abilitySugarRush = false;

    private Coroutine activeAbilityCoroutine; // reference to active ability coroutine

    void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
        postProcessLayer = GetComponentInChildren<PostProcessLayer>();
        
    }

    public void ActivateAbility(string ability)
    {
        if (ability == "sugarRush" && !abilitySugarRush)
        {
            activeAbilityCoroutine = StartCoroutine(SugarRushAbilityCoroutine()); // if ability is not active, start ability
        }
    }

    private IEnumerator SugarRushAbilityCoroutine()
    {
        // perform some action when ability starts
        postProcessLayer.enabled = true;
        abilitySugarRush = true;
        float walkSpeedBefore = playerMotor.walkSpeed;
        float walkFOVBefore = playerMotor.walkFOV;
        float walkJumpHeightBefore = playerMotor.walkJumpHeight;
        playerMotor.walkSpeed = 10;
        playerMotor.walkFOV = 90;
        playerMotor.walkJumpHeight = 2f;
            
        yield return new WaitForSeconds(abilityDuration);
        // perform some action when ability ends
        postProcessLayer.enabled = false;
        abilitySugarRush = false;
        playerMotor.walkSpeed = walkSpeedBefore;
        playerMotor.walkFOV = walkFOVBefore;
        playerMotor.walkJumpHeight = walkJumpHeightBefore;
    }
}