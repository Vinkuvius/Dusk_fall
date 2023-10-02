using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public float spellSpeed = 10f; // Speed of the fireball.
    public float spellDuration = 2f; // How long the fireball lasts.

    void Start()
    {
        // Add force to propel the fireball forward.
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * spellSpeed;

        // Destroy the fireball after a set duration.
        Destroy(gameObject, spellDuration);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check for collisions with enemies or objects.
        if (other.CompareTag("Enemy"))
        {
            // Deal damage or apply other effects to the enemy.
            // You can access the enemy's script and modify its health, for example.

            // Destroy the fireball on impact.
            Destroy(gameObject);
        }
    }
}
