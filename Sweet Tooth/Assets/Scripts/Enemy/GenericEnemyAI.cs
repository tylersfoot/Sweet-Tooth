using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class GenericEnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    public float sightRange; // distance the enemy can see the player
    public float attackRange; // distance the enemy can attack the player
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    public float health;
    public float damage;
    public float flashDuration;
    private Renderer[] renderers;
    private float timer;
    public float delaypatrol; // time between patrols
    public float delayattack; // time between attacks
    public GameObject itemPrefab; // item that will drop
    private bool isDead;
    public string enemyName; // name of the enemy, ex: "crazyCorn"
    
    private float randomZ;
    private float randomX;

    private MobSpawner mobSpawner; // reference to the spawner script
    private PlayerStats playerStats;
    private GameObject player; // reference to player

    void Start()
    {
        // called when enemy is spawned
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
        target = player.transform;
        renderers = GetComponentsInChildren<Renderer>();
        mobSpawner = GetComponentInParent<MobSpawner>();
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
        playerStats.DamagePlayer(damage, enemyName);
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
        
        // check that the randomly generated point is within patrolRadius distance from the MobSpawner object
        float distanceToSpawner = Vector3.Distance(randomDirection, mobSpawner.transform.position);
        if (distanceToSpawner > mobSpawner.patrolRadius)
        {
            randomDirection = (randomDirection - mobSpawner.transform.position).normalized * mobSpawner.patrolRadius + mobSpawner.transform.position;
        }

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
        if (!isDead) {
            isDead = true;
            GameObject itemDrop = Instantiate(
                itemPrefab,
                transform.position,
                transform.rotation
            );
            itemDrop.SetActive(true);
            mobSpawner.currentAmount -= 1;
            Destroy(gameObject);
        }
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

