using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    // TODO later: if enemy outside radius, patrol to radius again

    public float spawnRadius; // radius the enemies can spawn
    public float patrolRadius; // area the enemy can patrol in
    public float maxAmount; // how many enemies can be out at a time
    public float currentAmount; // how many enemies are out right now
    public float spawnDelay; // time between attempting to spawn another enemy
    public float spawnDelayRange; // random range to add to spawn delay
    public GameObject enemyPrefab; // enemy prefab to spawn

    void Start()
    {
        // for (int i = 0; i < maxAmount; i++)
        // {
        //     SpawnEnemy();
        // }

        // start the spawn coroutine
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            // wait for the spawn delay with random range
            yield return new WaitForSeconds(spawnDelay + Random.Range(0f, spawnDelayRange));

            // if there are less than max amount of enemies out
            if (currentAmount < maxAmount)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        currentAmount += 1;
        // get a random point in the spawn radius
        Vector3 spawnPoint = Random.insideUnitSphere * spawnRadius + transform.position;
        // set the spawn point to the height of the terrain
        spawnPoint.y = Terrain.activeTerrain.SampleHeight(spawnPoint);

        // spawn the enemy prefab at the spawn point
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, transform);
        enemyObject.SetActive(true);

    }

    void Update()
    {
        // delay = spawnDelay + Random.Range(0, spawnDelayRange);
    }

    void OnDrawGizmos()
    {
            // Draw a sphere in the spawn radius
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}
