using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 10f;
    public float dodgeForce = 15f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float groundRaycastLength = 0.2f;
    private bool canDropThrough = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Jumping
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Jumping");
            Jump();
        }
    }
    void Update()
    {
        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // Sprinting
        float currentMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        Vector2 moveVelocity = moveDirection * currentMoveSpeed;
        rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);

        // Jumping
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Jumping");
            Jump();
        }

        // Dodging
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float dodgeDirection = transform.localScale.x; // Use the player's facing direction
            rb.velocity = new Vector2(dodgeDirection * dodgeForce, rb.velocity.y);
        }

        // Drop through platforms with the "DropThrough" tag
        if (Input.GetKeyDown(KeyCode.S) && !canDropThrough)
        {
            canDropThrough = true;
            Invoke("ResetDropThrough", 0.5f); // Reset after 0.5 seconds to avoid immediate re-trigger
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("DropThrough"))
                {
                    collider.enabled = false; // Disable the collider temporarily
                    Invoke("EnableCollider", 0.2f); // Enable it after a short delay
                    break;
                }
            }
        }
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundRaycastLength, groundLayer);
    }

    void ResetDropThrough()
    {
        canDropThrough = false;
    }

    void EnableCollider()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("DropThrough"))
            {
                collider.enabled = true;
                break;
            }
        }
    }
}