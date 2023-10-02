using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public float comboResetTime = 2f;
    public float knockbackForce = 5f;
    public float attackRange = 2f; // Maximum attack range.
    public LayerMask enemyLayer;   // Layer for enemy GameObjects.
    public Transform enemy;

    private int currentComboCount = 0;
    private float lastAttackTime = 0f;
    private bool isComboActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
                    ExecuteAttack(currentComboCount);
                    KnockbackEnemy();
                    ResetCombo();
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
        // Perform different attacks based on the combo count.
        // For simplicity, we'll just log messages here.
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
    bool IsEnemyInRange()
    {
        if (enemy != null)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            return distance <= attackRange;
        }
        return false;
    }

    // Other functions remain the same...
}
