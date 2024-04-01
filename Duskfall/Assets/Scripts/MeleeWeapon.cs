using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float timeToDespawn = 0.1f;
    public float time;


    public void Start()
    {
        time = timeToDespawn;
    }

    // Update is called once per frame
    void Update()
    {
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
            other.gameObject.GetComponent<EnemyStuff>().health -= 10;
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

                bossEnemy.health -= 50;
            }

            Destroy(gameObject);
        }
    }
}
