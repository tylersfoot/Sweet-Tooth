using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    public float sightRange = 10f;
   
    // states
    public float attackRange;
    public bool playerInAttackRange;
    public bool playerInSightRange;

    void Start() {
        // target = player
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        // if player distance is 10 metres away {
        //     playerInSightRange = true
        // }
    // checks for player in sight range
       if (playerInSightRange && !playerInAttackRange)
       {
        chasePlayer();
       }
    }

    void chasePlayer() {
        // approaches player position
        agent.SetDestination(player.transform.position);
    }
}

