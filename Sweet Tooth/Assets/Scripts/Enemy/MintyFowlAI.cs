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
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public LayerMask Terrain;
    public float health;
    public float flashDuration;
    public Renderer[] renderers;

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
        if (distance <= sightRange)
        {
            chasePlayer();
        }
        if (distance <= attackRange) 
        {
            attackPlayer();
        }
        if (distance >= 10f)
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
        // stops approaching
        agent.SetDestination(transform.position);

        // I need the code for animation
        playerStats.DamagePlayer(2, "crazyCorn");
        
    }
    void patrol()
    {
        if (!walkPointSet) {

            SearchWalkPoint();
        }

        if (walkPointSet) {

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

        if(Physics.Raycast(walkPoint, -transform.up, 2f, Terrain)) {
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
