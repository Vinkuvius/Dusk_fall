using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWalker : MonoBehaviour
{
    public float wallWalkSpeed = 2.0f;

    private bool isWallWalking = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isWallWalking)
        {
            // Apply wall walking movement.
            Vector3 wallWalkDirection = transform.right; // Change this based on your model.
            rb.velocity = wallWalkDirection * wallWalkSpeed;
        }
    }

    public void StartWallWalk()
    {
        isWallWalking = true;
        // Flip the model to match the wall orientation.
        transform.Rotate(Vector3.up, 180); // Rotate 180 degrees around the Y-axis.
    }

    public void StopWallWalk()
    {
        isWallWalking = false;
        // Reset the model's rotation to normal.
        transform.rotation = Quaternion.identity; // Set rotation to identity (no rotation).
    }

}
