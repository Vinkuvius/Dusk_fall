using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public int baseAttackDamage = 10;
    public float comboResetTime = 2f;
    public float knockbackForce = 5f;
    public float attackRange = 2f; // Maximum attack range.
    public LayerMask enemyLayer;   // Layer for enemy GameObjects.
    public Transform enemy;

    private EnemyHealth enemyHealth;  // Reference to the enemy's health component.
    private int currentComboCount = 0;
    private float lastAttackTime = 0f;
    private bool isComboActive = false;

    GameObject enemyGameObject; // Declare a variable to store the reference to the enemy GameObject.

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Detect enemy within range.
            

                if (!isComboActive || Time.time - lastAttackTime > comboResetTime)
            {
                currentComboCount = 1;
            }
            else
            {
                currentComboCount++;
            }

            if (currentComboCount >= 4)
            {
                // Check if the enemy is within attack range.
                if (IsEnemyInRange())
                {
                    // Get the enemy's health component.
                    enemyHealth = GetEnemyHealth();
                    ExecuteAttack(currentComboCount);
                    KnockbackEnemy();
                    ResetCombo();
                    if (enemyHealth != null)
                    {
                        int damage = CalculateDamage(currentComboCount);
                        enemyHealth.TakeDamage(damage);
                    }
                }
            }
            else
            {
                ExecuteAttack(currentComboCount);
            }

            lastAttackTime = Time.time;
            isComboActive = true;
        }

        if (Time.time - lastAttackTime > comboResetTime)
        {
            ResetCombo();
        }
    }


    void ExecuteAttack(int comboCount)
    {
        // Determine the damage of the attack based on combo count or type of spell.
        int damage = CalculateDamage(comboCount);
        // Perform different attacks based on the combo count.
        // Check if the enemy is in range and has health.
        if (IsEnemyInRange())
        {
            // Apply damage to the enemy's health.
            enemyHealth.TakeDamage(damage);
        }

        switch (comboCount)
        {
            case 1:
                Debug.Log("First attack!");
                break;
            case 2:
                Debug.Log("Second attack!");
                break;
            case 3:
                Debug.Log("Third attack!");
                break;
            default:
                break;
        }
        // Check if it's the final hit in the combo for the fifth time.
        if (comboCount >= 5)
        {
            // Stagger the enemy.
            if (comboCount == 5)
            {
                // Access the enemy's EnemyStagger script.
                EnemyStagger enemyStaggerScript = enemyGameObject.GetComponent<EnemyStagger>();

                // Appy stagger effect
                if (enemyStaggerScript != null)
                {
                    enemyStaggerScript.Stagger();
                }
            }
        }

    }

    public void AttackEnemy(GameObject enemy)
    {
        //Attack logic
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            int damage = CalculateDamage(currentComboCount);
            enemyHealth.TakeDamage(damage);
        }
    }
    void KnockbackEnemy()
    {
        // Apply knockback force to the enemy's rigidbody.
        if (enemy != null)
        {
            Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                Vector3 knockbackDirection = (enemy.position - transform.position).normalized;
                enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
    void ResetCombo()
    {
        currentComboCount = 0;
        isComboActive = false;
    }
    int CalculateDamage(int comboCount)
    {
        // Calculate damage based on combo count, spell type, or any other relevant factors.
        return baseAttackDamage * comboCount;
    }
    bool IsEnemyInRange()
    {
        if (enemy != null)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            return distance <= attackRange;
        }
        return false;
    }
    EnemyHealth GetEnemyHealth()
    {
        //get the enemy's health component.
         RaycastHit hit;
                 if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
                 {
                     return hit.collider.GetComponent<EnemyHealth>();
                 }
        return null;
    }
}