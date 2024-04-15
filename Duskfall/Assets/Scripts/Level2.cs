using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    public GameObject playerPrefab;
    public Vector2 playerSpawnPosition = new Vector2(-7f, -0.9f); // Adjust spawn position as needed

    private void Start()
    {
        // Instantiate the player prefab at the specified position in Scene 2
        Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
    }
}
