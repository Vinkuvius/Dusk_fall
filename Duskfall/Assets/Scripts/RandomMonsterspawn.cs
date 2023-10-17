using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMonsterspawn : MonoBehaviour
{
    [System.Serializable]
    public class Monster
    {
        public GameObject monsterPrefab;
        public int maxSpawnCount;
    }

    public Monster[] monsterTypes;
    public float spawnAreaRadius = 20f; // Radius of the spawn area
    public int maxSpawnedMonsters = 20; // Maximum total spawned monsters

    private int spawnedMonsterCount = 0;

    private void Update()
    {
        if (spawnedMonsterCount < maxSpawnedMonsters)
        {
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        if (monsterTypes.Length == 0)
        {
            return;
        }

        int randomMonsterIndex = Random.Range(0, monsterTypes.Length);
        Monster selectedMonster = monsterTypes[randomMonsterIndex];

        if (selectedMonster.maxSpawnCount > 0)
        {
            GameObject newMonster = Instantiate(selectedMonster.monsterPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            spawnedMonsterCount++;
            selectedMonster.maxSpawnCount--;

            // You can add additional setup for the spawned monster here.
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnAreaRadius;
        return new Vector3(transform.position.x + randomCircle.x, transform.position.y, transform.position.z + randomCircle.y);
    }
}
