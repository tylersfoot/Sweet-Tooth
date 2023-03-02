using UnityEngine;
    using UnityEngine.AI;




public class EnemyAi : Monobehavior
{
    public NavMeshAgent agent;


    public Transform player;


    public LayerMaskl whatIsGround, whatIsPlayer;


    //Patrolling (for later)
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //Attacking
    public float timeAttack;
    bool alreadyAttacked;


    //States
    public float attackRange, sightRange;
    bool playerInAttackRange, playerInSightRange;


    private Void Awake() {
        player = GameObject.Find("PlayerObj").transform:
        agent = GetComponent<NavMeshAgent>();


    }


    private void Update() {
        //Checks for player in sight range
        playerInSightRange = Physics.CheckSphere(transfrom.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transfrom.position, sightRange, whatIsPlayer);
       
       if (!playerInSightRange && !playerInAttackRange) Patrolling();
       if (playerInSightRange && !playerInAttackRange) chasePlayer();
       if (playerInSightRange && playerInAttackRange) attackPlayer();


    }


    private void Patrolling() {




    }


    private void chasePlayer() {

agent.SetDestination(player.position);


    }


    private void attackPlayer() {




    }
}



