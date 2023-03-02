using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // attacking
    public float timeAttack;
    bool alreadyAttacked;

    // states
    public float attackRange, sightRange;
    bool playerInAttackRange, playerInSightRange;

    private void Awake() {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        // checks for player in sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
       
       if (!playerInSightRange && !playerInAttackRange) Patrolling();
       if (playerInSightRange && !playerInAttackRange) chasePlayer();
       if (playerInSightRange && playerInAttackRange) attackPlayer();
    }

    private void Patrolling() {

    }

    private void chasePlayer() {
        // approaches player position
        agent.SetDestination(player.position);

    }

    private void attackPlayer() {
        // stops enemy when attacking

        // agent.setDestination(transform.position);
        // transform.lookAt(player);

        if (!alreadyAttacked) {
            alreadyAttacked = true; 
        }
    }

    private void alreadyAttackedPlayer() {

    }
}