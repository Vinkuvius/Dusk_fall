using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{

    public GameObject Player;
    public LoseScreen Image;

    public void CheckLoseCondition()
    {

        // Lose condition is met, does the necessary �tg�rderna
        Debug.Log("The demonic beings on Tellus fractured you.");
        Image.ActivateLoseScreen();
        //Time.timeScale = 0f;

    }

}