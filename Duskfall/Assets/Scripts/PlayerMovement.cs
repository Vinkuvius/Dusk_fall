using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeedMultiplier = 2f;
    public float jumpForce = 10f;
    public float dodgeForce = 15f;
    public float dodgeCooldown = 1.5f;

    private bool canDodge = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is on the ground
        bool isGrounded = IsGrounded();

        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0f);

        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            moveDirection *= sprintSpeedMultiplier;
        }

        // Dodging
        if (Input.GetKeyDown(KeyCode.Space) && canDodge && isGrounded)
        {
            StartCoroutine(DodgeCooldown());
            rb.velocity = new Vector2(moveDirection.x * dodgeForce, rb.velocity.y);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Apply movement
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

    }
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f);
        return hit.collider != null && hit.collider.CompareTag("Ground");
    }

    IEnumerator DodgeCooldown()
    {
        canDodge = false;
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }
}