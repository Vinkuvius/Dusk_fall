using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBallSpell : MonoBehaviour
{
    public float Speed = 40f;
    public float timeToDespawn = 2f;
    public float time;


    public void Start()
    {
        // Shortcut for timeToDespawn
        time = timeToDespawn;
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed * 2;

        // States that when the timer of "timeToDespawn" reaches zero the projectile despawns
        if (time < 0f)
        {
            Destroy(gameObject);
            return;
        }
        time -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyStuff>().health -= 7.5f;
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Crow"))
        {
            other.gameObject.GetComponent<Crow>().currentHealth -= 5;
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            BossEnemy bossEnemy = other.gameObject.GetComponent<BossEnemy>();

            if (bossEnemy != null)
            {
                // Access the hurtSound property from the BossEnemy component
                AudioClip bossHurtSound = bossEnemy.hurtSound;

                // Check if the bossHurtSound is not null before playing
                if (bossHurtSound != null)
                {
                    bossEnemy.PlaySound(bossHurtSound); // Play the hurt sound
                }

                bossEnemy.health -= 45.5f;
            }

            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Boss2"))
        {
            BossLvl2 Boss2 = other.gameObject.GetComponent<BossLvl2>();

            if (Boss2 != null)
            {
                // Access the hurtSound property from the BossEnemy component
                AudioClip bossHurtSound = Boss2.hurtSound;

                // Check if the bossHurtSound is not null before playing
                if (bossHurtSound != null)
                {
                    Boss2.PlaySound(bossHurtSound); // Play the hurt sound
                }

                Boss2.health -= 45.5f;
            }

            Destroy(gameObject);
        }
    }
}
