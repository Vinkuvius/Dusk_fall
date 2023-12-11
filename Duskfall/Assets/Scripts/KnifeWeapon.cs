using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeWeapon : MonoBehaviour
{
    public int damagePerAttack = 100;
    public float attackCooldown = 1f;

    private bool canAttack = true;

    // You may have a Shop script that sets this value when the knife is purchased.
    private bool isKnifePurchased = false;

    void Update()
    {
        if (isKnifePurchased)
        {
            if (Input.GetMouseButtonDown(0) && canAttack)
            {
                Attack();
                StartCoroutine(Cooldown());
            }
        }
    }

    void Attack()
    {
        // Check if there's an enemy in front of the player and apply damage.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // Check if the hit object has an "Enemy" tag.
            if (hit.collider.CompareTag("Enemy"))
            {
                // Get the enemy's health component.
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    // Apply damage to the enemy.
                    enemyHealth.TakeDamage(damagePerAttack);
                }
            }
        }
    }

    System.Collections.IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    // Called when the knife is purchased at the shop.
    public void PurchaseKnife()
    {
        isKnifePurchased = true;
    }
}

