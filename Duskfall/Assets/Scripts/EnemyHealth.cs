using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 2500f; // Maximum health of the enemy
    private float currentHealth; // Current health of the enemy

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
    }

    // Function to handle enemy taking damage
    public void TakeDamage(float damage)
    {
        // Reduce current health by the damage amount
        currentHealth -= damage;

        // Check if the enemy has run out of health
        if (currentHealth <= 0f)
        {
            Die(); // Call the Die function when the enemy's health reaches zero or below
        }
    }

    // Function to handle enemy's death
    private void Die()
    {
        // You can add any death animations or effects here
        // For example, you might play an animation or spawn a particle effect

        // Destroy the enemy GameObject when it dies
        Destroy(gameObject);
    }
}
