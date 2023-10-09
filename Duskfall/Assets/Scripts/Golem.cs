using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : MonoBehaviour
{
    public float maxHealth = 10000f; // Maximum health of the golem
    public GameObject golemCorePrefab; // Prefab of the Golem's Core item
    public GameObject projectilePrefab; // Prefab of the golem's projectile
    public float projectileSpeed = 10f; // Speed of the projectile

    private float currentHealth; // Current health of the golem
    private Transform dropPoint; // Transform of the drop item spawn point

    private int comboHits = 0; // Counter for combo hits
    private int comboThreshold = 25; // Total hits required for stagger
    private bool isStaggered = false; // Flag to track if the golem is staggered
    private float staggerTimer = 0f; // Timer for stagger duration
    public float staggerDurationMin = 7f; // Minimum stagger duration
    public float staggerDurationMax = 8f; // Maximum stagger duration

    private bool isBelowHalfHealth = false; // Flag to track if the golem is below half health
    public float halfHealthThreshold = 0.5f; // Health threshold for activating attack2

    private bool isWithinAttack2Range = false; // Flag to track if the player is within attack2 range
    public float attack2Range = 30f; // Range for activating attack2

    private Transform player; // Transform of the player
    private NavMeshAgent navAgent; // Golem's NavMeshAgent2D component
    private bool isChasingPlayer = false; // Flag to track if the golem is chasing the player
    public float moveSpeed = 0.75f; // Golem's movement speed

    private Vector3 originalPosition; // Store the original position of the golem
    private bool isReturningToPosition = false; // Flag to track if the golem is returning to its original position
    private float wanderRange = 70f; // Range within which the golem will wander
    private float wanderTimer = 0f; // Timer for wandering duration
    private float wanderDuration = 5f; // Duration of wandering in seconds

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health

        // Find the drop point as a child of the golem
        dropPoint = transform.Find("DropPoint");

        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the NavMeshAgent2D component
        navAgent = GetComponent<NavMeshAgent>();

        // Set the initial speed of the NavMeshAgent2D
        navAgent.speed = moveSpeed;

        originalPosition = transform.position;
    }

    // Function to handle golem taking damage
    public void TakeDamage(float damage)
    {
        // Check if the golem is staggered
        if (isStaggered)
        {
            damage *= 2f; // Double the damage when staggered
        }

        if (gameObject.CompareTag("Magic"))
        {
            damage *= 0.1f; // Reduce the damage to 10% for "Magic" attacks
        }
        // Reduce current health by the damage amount
        currentHealth -= damage;

        // Check if the golem has run out of health
        if (currentHealth <= 0f)
        {
            Die(); // Call the Die function when the golem's health reaches zero or below
        }

        // Increment the combo hit counter
        comboHits++;

        // Check if the combo threshold is reached
        if (comboHits >= comboThreshold)
        {
            Stagger(); // Stagger the golem when the threshold is reached
        }

        // Check if the golem is below half health
        if (!isBelowHalfHealth && currentHealth <= maxHealth * halfHealthThreshold)
        {
            isBelowHalfHealth = true;

            // Check if the player is within attack2 range
            if (isWithinAttack2Range)
            {
                Attack2(); // Activate attack2 if both conditions are met
            }
        }
    }

    // Function to handle golem's death
    private void Die()
    {
        // You can add any death animations or effects here
        // For example, you might play an animation

        // Spawn the Golem's Core item at the drop point
        if (golemCorePrefab != null && dropPoint != null)
        {
            Instantiate(golemCorePrefab, dropPoint.position, Quaternion.identity);
        }

        // Destroy the golem GameObject when it dies
        Destroy(gameObject);
    }

    // Function to stagger the golem
    private void Stagger()
    {
        // You can add visual effects or animations to indicate the staggered state
        // For example, you might play a stagger animation

        isStaggered = true; // Set the staggered flag to true

        // Set a random stagger duration between staggerDurationMin and staggerDurationMax
        staggerTimer = Random.Range(staggerDurationMin, staggerDurationMax);
    }

    // Function to activate attack2
    private void Attack2()
    {
        // Check if the golem is staggered or below half health
        if (isStaggered || !isBelowHalfHealth)
        {
            return; // Attack2 can only be activated when not staggered and below half health
        }

        // Check if the player is within attack2 range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attack2Range)
        {
            // Perform attack2
            TakeDamage(999f); // Deal 999 damage with attack2
        }
    }

    // Function to check if the player is within attack2 range and update chasing state
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within attack2 range
        isWithinAttack2Range = distanceToPlayer <= attack2Range;

        // Check if the player is out of sight (behind a wall) or too far away
        if (isChasingPlayer && (distanceToPlayer > 100f || !CanSeePlayer()))
        {
            isChasingPlayer = false;
            navAgent.velocity = Vector2.zero;
            navAgent.isStopped = true;

            // Start returning to the original position
            isReturningToPosition = true;
            wanderTimer = 0f;
        }

        // If the player is not being chased and is within projectile range, shoot a projectile
        if (!isChasingPlayer && distanceToPlayer >= 60f && distanceToPlayer <= 100f)
        {
            ShootProjectile();
        }

        // If the player is within attack2 range and not being chased, start chasing
        if (isWithinAttack2Range && !isChasingPlayer)
        {
            isChasingPlayer = true;
            navAgent.isStopped = false;
        }

        // Handle returning to original position and wandering
        if (isReturningToPosition)
        {
            float distanceToOriginalPosition = Vector3.Distance(transform.position, originalPosition);

            // Check if the golem has returned to its original position
            if (distanceToOriginalPosition <= 1f)
            {
                isReturningToPosition = false; // Stop returning to the original position
            }
            else
            {
                // Return to the original position
                navAgent.SetDestination(originalPosition);
            }
        }
        else if (!isChasingPlayer)
        {
            // Handle wandering when not chasing the player
            wanderTimer += Time.deltaTime;

            if (wanderTimer >= wanderDuration)
            {
                // Choose a random destination within the wander range
                Vector3 randomDestination = originalPosition + Random.insideUnitSphere * wanderRange;

                // Ensure the destination stays within the wander range
                randomDestination.y = originalPosition.y;

                // Set the NavMeshAgent2D destination to the random destination
                navAgent.SetDestination(randomDestination);

                wanderTimer = 0f; // Reset the wander timer
            }
        }

        // If the player is not being chased and is within projectile range, shoot a projectile
        if (!isChasingPlayer && distanceToPlayer >= 60f && distanceToPlayer <= 100f)
        {
            ShootProjectile();
        }

        // If the player is within attack2 range and not being chased, start chasing
        if (isWithinAttack2Range && !isChasingPlayer)
        {
            isChasingPlayer = true;
            navAgent.isStopped = false;
        }

        // Update stagger timer
        if (isStaggered)
        {
            staggerTimer -= Time.deltaTime;

            if (staggerTimer <= 0f)
            {
                isStaggered = false; // Stagger duration is over
            }
        }

        // Update NavMeshAgent2D destination if chasing
        if (isChasingPlayer)
        {
            navAgent.SetDestination(player.position);
        }
    }

    // Function to check if the player is within line of sight
    private bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, 100f);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            return false; // Can't see the player because there's a wall in the way
        }
        return true; // Player is in line of sight
    }

    // Function to shoot a projectile
    private void ShootProjectile()
    {
        // Create a projectile and set its speed and direction
        if (projectilePrefab != null)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = directionToPlayer * projectileSpeed;
        }
    }
}


