using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreForGolem : MonoBehaviour
{
    public enum CoreColor { Green, Purple, Blue, Red, Yellow }
    public CoreColor coreColor;

    private static bool[] isColorUsed = new bool[System.Enum.GetValues(typeof(CoreColor)).Length];

    private void Start()
    {
        // Ensure only one core of each color drops
        if (isColorUsed[(int)coreColor])
        {
            Destroy(gameObject);
        }
        else
        {
            isColorUsed[(int)coreColor] = true;
        }
    }
}
