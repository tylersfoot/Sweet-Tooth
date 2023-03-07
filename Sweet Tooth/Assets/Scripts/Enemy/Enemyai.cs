using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    public float sightRange = 10f;
   
   

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
    }

    void chasePlayer() {
        // approaches player position
        agent.SetDestination(target.transform.position);
    }
}

