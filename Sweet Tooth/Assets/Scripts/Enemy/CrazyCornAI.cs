using UnityEngine;
using UnityEngine.AI;

public class CrazyCornAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    public float sightRange = 10f; // distance the enemy can see the player
    public float attackRange = 1f; // distance the enemy can attack the player
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public LayerMask Terrain;

    void Start() {
        // target = player
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    void Update() {
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
    }

    void chasePlayer() {
        // approaches player position
        agent.SetDestination(target.transform.position);
    }

    void attackPlayer() {
        // stops approaching
        agent.SetDestination(transform.position);

        // I need the code for the damage here and animation
    }
    void patrol() {
        
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

    void SearchWalkPoint() {

        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, Terrain)) {
        walkPointSet = true;
        }

    }
}

