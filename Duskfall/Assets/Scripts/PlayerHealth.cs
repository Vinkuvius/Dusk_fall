using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event System.Action<int> OnHealthChanged;

    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;

    private void Start()
    {
        // Makes so that currentHealth is the same as maxHealth at the start
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        if (currentHealth >= 100)
        {
            currentHealth = 100;
        }

        OnHealthChanged?.Invoke(currentHealth);
    }
    public void restoreHealth()
    {
        currentHealth += 20;
    }

}