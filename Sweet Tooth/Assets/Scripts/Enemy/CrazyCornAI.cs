using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrazyCornAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    public float sightRange = 10f; // distance the enemy can see the player
    public float attackRange = 5f; // distance the enemy can attack the player
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    public float health;
    public float damage;
    public float flashDuration;
    public Renderer[] renderers;
    private float timer;
    public float delaypatrol = 5.0f; // delay for the time between potrols
    public float delayattack = 1f;
    
    private float randomZ;
    private float randomX;

    public PlayerStats playerStats;

    void Start()
    {
        // target = player
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        renderers = GetComponentsInChildren<Renderer>();
       
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        // checks for player in sight range
        timer += Time.deltaTime;
        delayattack = delayattack + Time.deltaTime;
        //keeps track of the game time

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        // walkpoint arrived
        if (distanceToWalkPoint.magnitude < 3f) {
            walkPointSet = false;
        }

        if (distance <= attackRange && delayattack > 2f) 
        {
            AttackPlayer();
        }
        else if (distance <= sightRange)
        {
            ChasePlayer();
        }
        else if (timer > delaypatrol)
        {
            Patrol();
        }
        else
        {
            // do nothing
        }
    }

    void ChasePlayer()
    {
        // approaches player position
        agent.SetDestination(target.transform.position);
    }

    void AttackPlayer()
    {
        delayattack = 0f; // reset timer
        // stops approaching, sets target position to itself
        agent.SetDestination(transform.position);

        // attack player
        playerStats.DamagePlayer(damage, "crazyCorn");
        // TODO: add attack animation
    }

    void Patrol()
    {
        // if no walkpoint, searches for new one
        if (!walkPointSet) {
            SearchWalkPoint();
        }

        if (walkPointSet) {
            timer = 0;
            agent.SetDestination(walkPoint);
        }
    }

    void SearchWalkPoint()
    {
        // generate random point within patrol range
        Vector3 randomDirection = Random.insideUnitSphere * walkPointRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkPointRange, NavMesh.AllAreas);

        // set walkPoint if found
        if (NavMesh.SamplePosition(randomDirection, out hit, walkPointRange, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
        // else
        // {
        //     StartCoroutine(FlashRed(flashDuration));
        // }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    // IEnumerator FlashRed(float duration)
    // {
    //     // change material colors to red
    //     foreach (Renderer r in renderers)
    //     {
    //         Material[] materials = r.materials;
    //         for (int i = 0; i < materials.Length; i++)
    //         {
    //             materials[i].color = Color.red;
    //         }
    //     }

    //     // wait for duration
    //     yield return new WaitForSeconds(duration);

    //     // change material colors back to their original colors
    //     foreach (Renderer r in renderers)
    //     {
    //         Material[] materials = r.materials;
    //         for (int i = 0; i < materials.Length; i++)
    //         {
    //             materials[i].color = Color.white;
    //         }
    //     }
    // }
}

