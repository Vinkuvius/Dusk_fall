using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 10f; // Duration of one in-game day in seconds.
    private float currentTime = 0f; // Current time of day.

    void Update()
    {
        // Update time of day based on your game's logic.
        currentTime += Time.deltaTime;

        // Check if it's dusk.
        if (currentTime >= dayDuration / 2)
        {
            // Spawn stronger enemies, adjust XP, and drop rates.
            SpawnStrongerEnemies();
        }

        // Reset time at the end of the day.
        if (currentTime >= dayDuration)
        {
            currentTime = 0f;
        }
    }

    void SpawnStrongerEnemies()
    {
        // Spawn stronger enemies at dusk.
        // You can instantiate enemy GameObjects here with enhanced stats.
    }
}
