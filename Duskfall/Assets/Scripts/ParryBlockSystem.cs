using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryBlockSystem : MonoBehaviour
{
    public float parryDuration = 0.5f; // Duration for a successful parry (in seconds).
    public float blockDuration = 1.0f; // Duration for blocking (in seconds).
    private bool isParrying = false;
    private float parryStartTime = 0f;

    void Update()
    {
        // Check for "Shift" key press.
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            StartParry();
        }

        // Check for "Shift" key release.
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            if (isParrying)
            {
                EndParry();
            }
            else
            {
                // Handle block logic if not parrying.
                StartBlock();
            }
        }

        // Check for ongoing parry duration.
        if (isParrying && Time.time - parryStartTime > parryDuration)
        {
            EndParry();
        }
    }

    void StartParry()
    {
        isParrying = true;
        parryStartTime = Time.time;
        // Implement parry behavior here (e.g., animation, effects).
        Debug.Log("Parry started!");
    }

    void EndParry()
    {
        isParrying = false;
        // Implement parry end behavior here (e.g., reset animations, effects).
        Debug.Log("Parry ended!");
    }

    void StartBlock()
    {
        // Implement block behavior here (e.g., reduced damage, animations).
        Debug.Log("Block started!");
    }
}
