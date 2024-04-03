using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = gameObject.GetComponent<PlayerHealth>().maxHealth;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = gameObject.GetComponent<PlayerHealth>().currentHealth;
    }
}
