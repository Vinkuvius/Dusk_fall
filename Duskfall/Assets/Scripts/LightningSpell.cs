using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : MonoBehaviour
{
    public float spellDamage = 10f; // Damage caused by the lightning.
    public float spellRange = 10f; // Maximum range of the lightning.

    void Start()
    {
        // Cast a ray forward to detect objects or enemies.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, spellRange))
        {
            // Check if the ray hit an enemy or object.
            if (hit.collider.CompareTag("Enemy"))
            {
                // Deal damage or apply other effects to the enemy.
                // You can access the enemy's script and modify its health, for example.
            }
        }

        // Destroy the lightning spell.
        Destroy(gameObject);
    }
}
