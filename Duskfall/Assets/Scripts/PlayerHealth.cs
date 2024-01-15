using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public float armor;
    private float magicResistance;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float physicalDamage, float magicalDamage)
    {
        // Apply armor to reduce physical damage
        float effectivePhysicalDamage = Mathf.Max(0, physicalDamage - armor);

        // Apply magic resistance to reduce magical damage
        float effectiveMagicalDamage = magicalDamage * (1 - magicResistance);

        // Calculate total damage
        float totalDamage = effectivePhysicalDamage + effectiveMagicalDamage;

        currentHealth -= totalDamage;

        // You can add additional logic here, such as checking for death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Perform actions when the player dies
        Debug.Log("Player died");
    }

    public void SetArmor(float newArmor)
    {
        armor = newArmor;
    }

    public void SetMagicResistance(float newMagicResistance)
    {
        magicResistance = newMagicResistance;
    }
}