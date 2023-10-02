using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeSpell : MonoBehaviour
{
    public float spellDamage = 15f; // Damage caused by the ice spike.

    void OnTriggerEnter(Collider other)
    {
        // Check for collisions with enemies or objects.
        if (other.CompareTag("Enemy"))
        {
            // Deal damage or apply other effects to the enemy.
            // You can access the enemy's script and modify its health, for example.

            // Destroy the ice spike on impact.
            Destroy(gameObject);
        }
    }
}
