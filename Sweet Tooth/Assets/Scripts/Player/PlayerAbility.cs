using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerAbility : MonoBehaviour
{
    public PlayerMotor playerMotor;
    public PlayerStats playerStats;
    private PostProcessLayer postProcessLayer;
    public float abilityDuration = 3f; // default ability length in seconds
    public float abilitySugarRushProgress;
    public float abilitySugarRushSpeed; // fill speed
    public bool abilitySugarRush = false;

    private Coroutine activeAbilityCoroutine; // reference to active ability coroutine

    void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
        postProcessLayer = GetComponentInChildren<PostProcessLayer>();
        
    }

    void Update()
    {
        if (abilitySugarRushProgress >= 1)
        {
            abilitySugarRushProgress = 1;
        }
        else
        {
            abilitySugarRushProgress += abilitySugarRushSpeed * Time.deltaTime;
        }
    }


    public void ActivateAbility(string ability)
    {
        if (ability == "sugarRush" && !abilitySugarRush && abilitySugarRushProgress == 1)
        {
            activeAbilityCoroutine = StartCoroutine(SugarRushAbilityCoroutine()); // if ability is not active, start ability
            abilitySugarRushProgress = 0;
        }
    }

    private IEnumerator SugarRushAbilityCoroutine()
    {
        // perform some action when ability starts
        postProcessLayer.enabled = true;
        abilitySugarRush = true;
        float walkSpeedBefore = playerStats.walkSpeed;
        float walkFOVBefore = playerMotor.walkFOV;
        float walkJumpHeightBefore = playerMotor.walkJumpHeight;
        playerStats.walkSpeed = 10;
        playerMotor.walkFOV = 90;
        playerMotor.walkJumpHeight = 2f;
            
        yield return new WaitForSeconds(abilityDuration);
        // perform some action when ability ends
        postProcessLayer.enabled = false;
        abilitySugarRush = false;
        playerStats.walkSpeed = walkSpeedBefore;
        playerMotor.walkFOV = walkFOVBefore;
        playerMotor.walkJumpHeight = walkJumpHeightBefore;
    }
}