using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRange = 10f; // Range of the detection ray.
    public LayerMask enemyLayer;      // Layer containing enemy GameObjects.

    private GameObject detectedEnemy; // Reference to the detected enemy.

    void Update()
    {
        // Create a ray from the player's position forward.
        Ray ray = new Ray(transform.position, transform.forward);

        // Declare a RaycastHit to store information about the hit.
        RaycastHit hit;

        // Check if the ray hits something within the detection range.
        if (Physics.Raycast(ray, out hit, detectionRange, enemyLayer))
        {
            // Check if the hit object has an "Enemy" tag.
            if (hit.collider.CompareTag("Enemy"))
            {
                // Store a reference to the detected enemy GameObject.
                detectedEnemy = hit.collider.gameObject;
            }
        }
        else
        {
            // Reset the detected enemy reference if no enemy is in sight.
            detectedEnemy = null;
        }
    }

    public void DetectEnemy()
    {
        if (detectedEnemy != null)
        {
            // The enemy has been detected, and you can do something with it.
            // For example, you can attack the detected enemy or interact with it.
            Debug.Log("Enemy detected!");
        }
        else
        {
            // No enemy is in sight.
            Debug.Log("No enemy detected.");
        }
    }
}
