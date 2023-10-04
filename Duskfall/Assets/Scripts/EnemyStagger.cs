using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStagger : MonoBehaviour
{
    public float staggerDuration = 1.0f; // Duration of the stagger effect.

    private bool isStaggered = false;
    private float staggerEndTime;

    void Update()
    {
        // Check if the stagger effect has ended.
        if (isStaggered && Time.time >= staggerEndTime)
        {
            isStaggered = false;

            // Implement logic to resume enemy behavior after staggering.
        }
    }

    public void Stagger()
    {
        if (!isStaggered)
        {
            // Apply the stagger effect.
            isStaggered = true;
            staggerEndTime = Time.time + staggerDuration;

            // Implement logic to temporarily disable enemy actions during the stagger.
            // For example, you can stop enemy movement and attacks.
        }
    }
}
