using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject bossPrefab;
    public float bossSpawnDistance = 5f;
    private bool bossSpawned = false;

    void Update()
    {
        // Check the distance between player and spawner
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // When player is close enough, runs the SpawnBoss script
        if (!bossSpawned && distanceToPlayer < bossSpawnDistance)
        {
            SpawnBoss();
        }
    }

    // Spawns boss at spawner location
    void SpawnBoss()
    {
        Instantiate(bossPrefab, transform.position, Quaternion.identity);
        bossSpawned = true;
    }
}