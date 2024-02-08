using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float dodgeForce = 20f;
    public LayerMask groundLayer;
    public Transform groundcheck;
    bool isGrounded;
    private Rigidbody2D rb;

    [SerializeField] int JumpPower;
    
    

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

        // Dodging
        float dodgeDirection = 0f;

        if (horizontalInput > 0)
        {
            dodgeDirection = 1f; //Right dodging
        }
        else if (horizontalInput < 0)
        {
            dodgeDirection = -1f; //Left dodging
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(dodgeDirection * dodgeForce, rb.velocity.x);
        }

        isGrounded = Physics2D.OverlapCapsule(groundcheck.position, new Vector2(1.8f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        
        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }

    } 
}