using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{

    public GameObject Player;
    public LoseScreen Image;

    public void CheckLoseCondition()
    {

        // Lose condition is met, does the necessary conditions
        Debug.Log("The demonic beings on Tellus fractured you.");
        Image.ActivateLoseScreen();
        Image.AcivateLoseMenu();
        //Time.timeScale = 0f;
    }

}