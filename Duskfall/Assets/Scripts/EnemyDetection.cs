using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private GameObject player;
    public float detectionRadius = 5f;
    public LayerMask playerLayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        DetectPlayer();
    }

   public void DetectPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer))
        {
            // Player detected
            Debug.Log("Player detected!");

            // Add your code here for actions when the player is detected
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the detection radius in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}