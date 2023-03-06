using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
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

