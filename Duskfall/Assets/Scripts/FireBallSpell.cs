using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Update is called once per frame
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
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<BossEnemy>().health -= 3.25f;
        }

        Destroy(gameObject);
    }
}