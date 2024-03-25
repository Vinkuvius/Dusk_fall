using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
public class PlayerMovement : MonoBehaviour
{
    public Transform character;
    public float moveSpeed = 5f;
    public float sprintSpeed = 30f;
    private Rigidbody2D rb;
    public float dodgeForce = 15f;
    float dodgeDirection = 0f;
    public LayerMask groundLayer;
    public Transform groundcheck;
    bool isGrounded;
    public bool isDoding;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = character.GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("Character1Idle", true); // Set the "Character1Idle" parameter to true
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

        // Set animation based on movement
        if (animator != null)
        {
            // Check if player is moving horizontally
            if (Mathf.Abs(horizontalInput) > 0)
            {
                animator.SetBool("IsRunning", true); // Set the "IsRunning" parameter to true to trigger the run animation
                animator.SetBool("Character1Idle", false); // Set the "Character1Idle" parameter to false
            }
            else
            {
                animator.SetBool("IsRunning", false); // Set the "IsRunning" parameter to false
                animator.SetBool("Character1Idle", true); // Set the "Character1Idle" parameter to true
            }

            // Check if player is falling or jumping
            if (!isGrounded)
            {
                if (rb.velocity.y < 0)
                {
                    animator.SetBool("IsFalling", true); // Set the "IsFalling" parameter to true to trigger the fall animation
                }
            }
            else
            {
                animator.SetBool("IsFalling", false); // Set the "IsFalling" parameter to false

                // Check for jumping animation
                if (Input.GetKeyDown(KeyCode.W))
                {
                    animator.SetBool("IsJumping", true); // Set the "IsJumping" parameter to true to trigger the jump animation
                }
                else
                {
                    animator.SetBool("IsJumping", false); // Set the "IsJumping" parameter to false
                }
            }

            // Dodge
            if (horizontalInput > 0) // Moving right
            {
                dodgeDirection = 8f; // Dodge right
            }
            else if (horizontalInput < 0) // Moving left
            {
                dodgeDirection = -8f; // Dodge left
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == false)
            {
                rb.velocity = new Vector2(dodgeDirection * dodgeForce, rb.velocity.y);
            }
        }

    }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isDoding = false;
                isGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isDoding = true;
                isGrounded = false;
            }
        }
}