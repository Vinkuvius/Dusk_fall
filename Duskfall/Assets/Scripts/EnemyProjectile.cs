using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 10;
    public float moveSpeed = 0.1f;
    public float timeToDespawn = 1.5f;
    public Rigidbody2D body;
    public Vector2 fireDirection;

    public float timee;

    private void Start()
    {
        timee = timeToDespawn;
    }

    private void Update()
    {
        if (timee < 0f)
        {
            Destroy(gameObject);
            return;
        }
        timee -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") == true)
        {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
    }

    private void FixedUpdate()
    {
        Vector2 Movement = fireDirection * moveSpeed * Time.deltaTime;
        body.position += Movement;
    }
}