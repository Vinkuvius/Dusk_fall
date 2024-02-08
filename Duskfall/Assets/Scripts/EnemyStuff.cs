using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStuff : MonoBehaviour
{
    public LayerMask GroundLayer;

    public float moveSpeed = 3f; // Hastigheten som fienden rör sig på
    public float health = 30; // Fiende HP
    public float rayDistance;

    public int damage = 20;

    public bool isGround;

    private Transform player; // Refererar till player's transform
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Hittar player 
    }

    void Update()
    {

        // Rör sig mot player
        if (Vector3.Distance(transform.position, player.position) <= 15)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            player.position,
            moveSpeed * Time.deltaTime);
        }
        // Kod som gör att fienden dör och försvinner från spelet
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private bool IsGrounded()
    {
        //Kod som kontrollerar om man är på marken och gör så man stannar på marken
        var groundCheck = Physics2D.Raycast(transform.position,
            Vector2.down, rayDistance, GroundLayer);


        return groundCheck.collider != null &&
            groundCheck.collider.CompareTag("Ground");
    }
}
