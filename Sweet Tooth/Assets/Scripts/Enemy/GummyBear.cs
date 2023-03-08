using UnityEngine;
using UnityEngine.AI;

public class GummyBearAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    public float sightRange = 10f; // distance the enemy can see the player
    public float attackRange = 1f; // distance the enemy can attack the player
    public Transform[] moveSpots;
    private int randomSpot;
    public float speed;

    void Start() {
        // target = player
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        randomSpot = Random.Range(0, moveSpots.Length);
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
        //stops approaching
        agent.SetDestination(transform.position);

        //I need the code for the damage here and animation
    }
    void patrol() {
        //
        agent.transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
    }
}

