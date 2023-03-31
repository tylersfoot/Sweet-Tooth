using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MintyFowlAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    public float sightRange = 10f; // distance the enemy can see the player
    public float attackRange = 5f; // distance the enemy can attack the player
    public float patrolRange = 10f; // distance the enemy patrols
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public LayerMask Terrain;
    public float health;
    public float damage;
    public float flashDuration;
    public Renderer[] renderers;
    float timer;
    public float delaypatrol = 5.0f; // delay for the time between potrols
    public float delayattack = 1f;

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
        timer = Time.deltaTime;
        delayattack = delayattack + Time.deltaTime;
        //keeps track of the game time
        if (distance <= sightRange)
        {
            chasePlayer();
        }
        if (distance <= attackRange && delayattack > 2f) 
        {
            attackPlayer();
        }
        if (distance >= patrolRange && timer > delaypatrol)
        {
            patrol();
        }

        // Debug.Log(health);
    }

    void chasePlayer()
    {
        // approaches player position
        agent.SetDestination(target.transform.position);
    }

    void attackPlayer()
    {
        delayattack = 0f;
        // stops approaching
        agent.SetDestination(transform.position);

        // I need the code for animation
        playerStats.DamagePlayer(2, "mintyFowl");
        
    }
    void patrol()
    {
        //if no walkpoint searches for new one
        if (!walkPointSet) {

            SearchWalkPoint();
        }

        if (walkPointSet) {
            delaypatrol = Time.deltaTime + 5f;
        agent.SetDestination(walkPoint);
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Walkpoint arrived

        if (distanceToWalkPoint.magnitude < 1f) {

            walkPointSet = false;

        }

    }

    void SearchWalkPoint()
    {

        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 4f, Terrain)) {
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

