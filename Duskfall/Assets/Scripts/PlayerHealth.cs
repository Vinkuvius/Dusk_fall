using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public event System.Action<int> OnHealthChanged;

    public float timer = 0.2f;
    public float closingTimer;
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public LoseCondition Lose;
    public Slider slider;

    private void Start()
    {
        // Makes so that currentHealth is the same as maxHealth at the start
        currentHealth = maxHealth;
        slider.value = currentHealth;
    }
    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > closingTimer)
        {
            timer = 0f;
            {
                RegenHealth();
            }
        }
        slider.value = currentHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Lose.CheckLoseCondition();
            gameObject.SetActive(false);
        }
        if (currentHealth >= 100)
        {
            currentHealth = 100;
        }

        OnHealthChanged?.Invoke(currentHealth);
    }

    public void RegenHealth()
    {
        if (currentHealth < 100) 
        {
            currentHealth += 1;
            OnHealthChanged.Invoke(currentHealth);
        }
    }
}