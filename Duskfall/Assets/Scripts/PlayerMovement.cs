using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform character;
    public float moveSpeed = 5f;
    public float sprintSpeed = 30f;
    private Rigidbody2D rb;
    public float dodgeAcceleration = 25f;
    public float dodgeDeceleration = 10f;
    private float currentDodgeVelocity = 0f;
    private float dodgeDirection = 0f;
    public float dodgeDistance = 5f;
    private float remainingDodgeDistance = 0f;
    public LayerMask groundLayer;
    public Transform groundcheck;
    bool isGrounded;
    public bool isDodging;


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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isDodging = false;
            isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            isDodging = false;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isDodging = true;
            isGrounded = false;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            isDodging = true;
            isGrounded = false;
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

        // Dodge movement
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDodging)
        {
            isDodging = true;
            dodgeDirection = horizontalInput;
            currentDodgeVelocity = dodgeDirection * dodgeAcceleration;
            remainingDodgeDistance = dodgeDistance;
            Invoke("StopDodging", 0.5f); // Stop dodging after 0.5 seconds
        }

        // Update Rigidbody velocity with dodge velocity
        float lookDir = Mathf.Sign(moveVelocity.x);
        var scale = transform.localScale;
        scale.x = lookDir;
        transform.localScale = scale;

        rb.velocity = new Vector2(moveVelocity.x + currentDodgeVelocity, rb.velocity.y);

        // Decelerate dodge movement when not dodging
        if (!isDodging && isGrounded)
        {
            if (currentDodgeVelocity > 0)
            {
                currentDodgeVelocity -= dodgeDeceleration * Time.deltaTime;
                if (currentDodgeVelocity < 0)
                {
                    currentDodgeVelocity = 0f;
                }
            }
            else if (currentDodgeVelocity < 0)
            {
                currentDodgeVelocity += dodgeDeceleration * Time.deltaTime;
                if (currentDodgeVelocity > 0)
                {
                    currentDodgeVelocity = 0f;
                }
            }
        }

        // Update remaining dodge distance
        if (isDodging)
        {
            remainingDodgeDistance -= Mathf.Abs(currentDodgeVelocity) * Time.deltaTime;
            if (remainingDodgeDistance <= 0)
            {
                StopDodging();
            }
        }

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    animator.Play("Jump", 0, 0);
        //}



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
            if (isGrounded)
            {
                animator.SetBool("Character1Idle", true);
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
        }
    }

    void StopDodging()
    {
        isDodging = false;
        remainingDodgeDistance = 0f;
        currentDodgeVelocity = 0f; // Remove all momentum
    }
}
