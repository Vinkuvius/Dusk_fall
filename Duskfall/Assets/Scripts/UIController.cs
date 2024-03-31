using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    private void Start()
    {
        // refererar till PlayerHealth och tar ut component OnHealthChanged
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.OnHealthChanged += UpdateHealthText;
    }

    //Koden som visualiserar PlayerHealth
    private void UpdateHealthText(int health)
    {
        healthText.text = "Health: " + health.ToString() + "/100";
    }
}
