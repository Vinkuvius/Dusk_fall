using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject BossEnemy;
    public VictoryScreen Image;

    public void CheckWinCondition()
    {

        // Win condition meets, does the necessary conditions
        Debug.Log("You have defeated the demons that had invaded earth! Well done!");
        Image.ActivateVictoryScreen();
        Time.timeScale = 0f;

    }
}
