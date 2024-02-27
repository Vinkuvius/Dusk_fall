using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 30f;
    private Rigidbody2D rb;
    public float dodgeForce = 15f;
    float dodgeDirection = 0f;
    public LayerMask groundLayer;
    public Transform groundcheck;
    bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (horizontalInput > 0) // Moving right
        {
            dodgeDirection = 8f; // Dodge right
        }
        else if (horizontalInput < 0) // Moving left
        {
            dodgeDirection = -8f; // Dodge left
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(dodgeDirection * dodgeForce, rb.velocity.y);
        }

        isGrounded = Physics2D.OverlapCapsule(groundcheck.position, new Vector2(1.8f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);

    }

}