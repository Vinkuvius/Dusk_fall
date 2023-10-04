using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongerEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int attackDamage = 20;
    public float dropRate = 0.3f; // Chance to drop the Golem Core.

    // ... Other variables ...

    private bool isStaggered = false;
    private int comboCount = 0;

    void Update()
    {
        // Implement enemy AI and behavior here.
        // For example, check for player's attacks and combo count.
    }

    public void TakeDamage(int damage)
    {
        // Handle enemy taking damage.
        // Check if the hit lands on the Golem Core (e.g., using raycasting).
        // If it's the final hit in the combo for the fifth time, stagger the enemy.
        // Adjust enemy health and behavior accordingly.
    }

    void DropItem()
    {
        // Handle item drop logic here.
        // Generate a random number and check if it's within the drop rate.
        // If it is, drop the "Golem Core" item.
    }
}
