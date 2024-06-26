using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isJumping;
    public float jump;
    public float fall;

    private void Start()
    {
        fall = -jump *4;
    }

    // Update is called once per frame
    void Update()
    {
        //Jumping ability
        if (Input.GetKeyDown(KeyCode.W) && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.y, jump));
        }

        if (Input.GetKeyDown(KeyCode.S) && isJumping == true)
        {
            rb.AddForce(new Vector2 (rb.velocity.y, fall));
        }
    }

    //Making sure jumping works as intended
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            isJumping = true;
        }
    }   
}