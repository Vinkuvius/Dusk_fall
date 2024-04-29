using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossLvl2 : MonoBehaviour
{
    public LayerMask GroundLayer;
    public float moveSpeed = 15f;
    public float jumpForce = 20f;
    public float attackRange = 100f;
    public float SeeDIstance = 200f;
    public int damage = 10;
    public AudioClip IceGolemWalk;
    public AudioClip hurtSound;
    public AudioClip defeatSound;

    private Transform player;
    public float health = 350f;
    private AudioSource audioSource;
    public bool isGround;
    private float chargeDelayTimer = 0f;
    public float chargeDelayDuration = 2f;
    private float currentMoveSpeed;
    private const float maxChargeDistance = 10f;
    private float distanceMovedDuringCharge = 0f;

    public House house;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();

        currentMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (health <= 0)
        {
            PlaySound(defeatSound);
            Invoke("DestroyBoss", 3.0f);
            house.gameObject.SetActive(true);
        }

        if (Vector3.Distance(transform.position, player.position) <= SeeDIstance)
        {
            MoveTowardsPlayer();
        }

        if (chargeDelayTimer <= 0f)
        {
            // Check if the player is within attack range
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                // Start charging towards the player or jumping if player is above
                if (player.position.y > transform.position.y)
                {
                    JumpTowardsPlayer();
                }
                else
                {
                    ChargeTowardsPlayer();
                }
            }
        }
        else
        {
            // Decrement the delay timer
            chargeDelayTimer -= Time.deltaTime;
        }

        if (isMoving())
        {
            PlaySound(IceGolemWalk);
        }
    }

    void MoveTowardsPlayer()
    {
        // Move towards the player at normal speed
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void JumpTowardsPlayer()
    {
        // Add force to jump towards the player
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);

        chargeDelayTimer = chargeDelayDuration;
    }

    void ChargeTowardsPlayer()
    {
        currentMoveSpeed = moveSpeed * 100f; // Adjusted multiplier for extremely fast dash

        // Calculate the direction from the boss to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Move the boss towards the player's direction at increased speed
        transform.Translate(directionToPlayer * currentMoveSpeed * Time.deltaTime);

        distanceMovedDuringCharge += currentMoveSpeed * Time.deltaTime;

        if (distanceMovedDuringCharge >= maxChargeDistance)
        {
            // Reset the movement speed to normal after the rush
            currentMoveSpeed = moveSpeed;
            distanceMovedDuringCharge = 0f;
            chargeDelayTimer = chargeDelayDuration;
        }

        if (isMoving())
        {
            PlaySound(IceGolemWalk);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(60);
        }
    }

    public bool isMoving()
    {
        return GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void DestroyBoss()
    {
        Destroy(gameObject);
    }
}
