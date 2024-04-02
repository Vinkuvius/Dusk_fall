using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStuff : MonoBehaviour
{
    public LayerMask GroundLayer;

    public float moveSpeed = 3f; // Movementspeed of enemy
    public float health = 50f; // Enemy Health points
    public float rayDistance;
    

    public int damage = 20;

    public bool isGround;

    private Transform player; // Referencing to player's transform
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Hittar player
    }

    void Update()
    {

        // Moves towards player
        if (Vector3.Distance(transform.position, player.position) <= 15)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            player.position,
            moveSpeed * Time.deltaTime);
        }
        // Code that makes it so the enemy dies and gets removed from the game
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
        //  Code that checks if something is on the ground and makes it so it stays on the ground
        var groundCheck = Physics2D.Raycast(transform.position,
            Vector2.down, rayDistance, GroundLayer);


        return groundCheck.collider != null &&
            groundCheck.collider.CompareTag("Ground");
    }
}
