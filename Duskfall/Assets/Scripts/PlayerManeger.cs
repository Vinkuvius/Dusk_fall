using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManeger : MonoBehaviour
{
    public static PlayerManeger instance;

    private void Awake()
    {
        // Ensure only one instance of PlayerManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}
