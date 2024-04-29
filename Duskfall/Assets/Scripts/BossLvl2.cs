using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossLvl2 : MonoBehaviour
{
    public LayerMask GroundLayer;
    public float moveSpeed = 15f;
    public float attackRange = 100f;
    public float SeeDIstance = 200f;
    public int damage = 10;
    public AudioClip IceGolemWalk;
    public AudioClip hurtSound;
    public AudioClip defeatSound;

    private Transform player;
    public float health = 350f;
    private AudioSource audioSource;
    public float rayDistance;
    public bool isGround;
    private float chargeDelayTimer = 0f;
    public float chargeDelayDuration = 2f;
    private float currentMoveSpeed;
    private const float maxChargeDistance = 10f;
    private float distanceMovedDuringCharge = 0f;

    public float timer1 = 0.2f;
    public float closingTimer1;
    public float offset;

    private Vector2 lastPosition;
    public float moveThreshold = 0.1f;

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
                // Start charging towards the player
                ChargeTowardsPlayer();
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
    void ChargeTowardsPlayer()
    {
        // Increase the movement speed for charging
        currentMoveSpeed = moveSpeed * 50f;


        // Move towards the player at increased speed
        transform.position = Vector3.MoveTowards(transform.position, player.position, currentMoveSpeed * Time.deltaTime);

        distanceMovedDuringCharge += currentMoveSpeed * Time.deltaTime;

        if (distanceMovedDuringCharge >= maxChargeDistance)
        {
            // Reset the movement speed to normal after the rush
            currentMoveSpeed = moveSpeed;
            distanceMovedDuringCharge = 0f;
            chargeDelayTimer = chargeDelayDuration;
        }

        // Check if boss is close enough to attack
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            Debug.Log("Boss Charging at Player");
        }

        // Check if boss has reached the player
        if (Vector3.Distance(transform.position, player.position) <= 0.1f)
        {
            // Reset the movement speed to normal after the rush
            currentMoveSpeed = moveSpeed;
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
        Vector2 currentPosition = transform.position;
        float distanceMoved = Vector2.Distance(currentPosition, lastPosition);
        lastPosition = currentPosition;
        return distanceMoved > moveThreshold;
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
