using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject BossEnemy;
    public VictoryScreen Image;

    public void CheckWinCondition()
    {

        // Win condition möts, gör de nödvädiga åtgärderna
        Debug.Log("You have defeated the demons that had invaded earth! Well done!");
        // Add your win code here, such as showing a victory screen, playing a sound, etc.
        Image.ActivateVictoryScreen();
        Time.timeScale = 0f;

    }
}
