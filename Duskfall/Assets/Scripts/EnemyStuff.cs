using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStuff : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navAgent;
    private Animator animator;

    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    public float rushDistance = 10f;
    public float longRangeDistance = 20f;
    public float rotationSpeed = 5f;
    public float comboAttackDelay = 1.5f;
    public float comboResetTime = 3f;
    public float magicCooldown = 5f;
    

    private bool isChasing = false;
    private bool isAttacking = false;
    private float timeSinceLastAttack = 0f;
    private float timeSinceLastMagic = 0f;
    private bool hasSeenPlayer = false;

    private int comboStage = 0;
    private int currentComboChain = 0;

    private float enemyHP = 1500f;
    public float cautiousThreshold = 30f;

    private float enemyMP = 250f;
    private float magicCost = 2f;
    private float magic2Cost = 5f; // MP cost for Magic2
    private float maxHP = 1500f; // Max HP of the enemy

    private float normalAttackDamage = 225f;
    private float comboAttackDamage = 675f;
    private float rushAttackDamage = 300f;
    private float magic1Damage = 195f;
    private float magic2Damage = 450f;

    private bool isChasingPlayer = false; // Added to track if the enemy is chasing the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= rushDistance && enemyHP > cautiousThreshold)
        {
            isChasing = true;
            isChasingPlayer = true; // Enemy is chasing the player
            navAgent.SetDestination(player.position);
            animator.SetBool("isWalking", true);

            if (distanceToPlayer <= attackDistance && !isAttacking)
            {
                if (comboStage == 0)
                {
                    StartCoroutine(ComboAttack1());
                }
                else if (comboStage == 1)
                {
                    StartCoroutine(ComboAttack2());
                }
            }
        }
        else if (isChasing)
        {
            isChasing = false;
            isChasingPlayer = false; // Enemy is no longer chasing the player
            navAgent.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }

        // Check if the enemy should stop chasing the player when the player is 50 meters away
        if (isChasingPlayer && distanceToPlayer > 50f)
        {
            isChasing = false;
            isChasingPlayer = false;
            navAgent.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }

        if (distanceToPlayer > longRangeDistance && !isAttacking && timeSinceLastMagic >= magicCooldown && hasSeenPlayer)
        {
            // Check if there's enough MP for Magic1 or Magic2
            if (enemyMP >= magicCost)
            {
                if (enemyHP < maxHP * 0.5f && Random.value < 0.7f) // Higher chance to use Magic2 when HP is below half
                {
                    PerformMagic2();
                }
                else
                {
                    PerformMagic1();
                }
            }
        }
        else
        {
            timeSinceLastMagic += Time.deltaTime;
        }

        RotateTowardsPlayer();

        if (isAttacking)
        {
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= comboResetTime)
            {
                ResetCombo();
            }
        }

        if (!hasSeenPlayer && distanceToPlayer <= chaseDistance)
        {
            hasSeenPlayer = true;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    IEnumerator ComboAttack1()
    {
        comboStage = 1;
        animator.SetTrigger("Attack1");
        isAttacking = true;
        currentComboChain++;
        if (currentComboChain == 2)
        {
            comboStage = 0;
            currentComboChain = 0;
        }
        yield return new WaitForSeconds(comboAttackDelay);
        isAttacking = false;
        timeSinceLastAttack = 0f;
    }

    IEnumerator ComboAttack2()
    {
        comboStage = 2;
        animator.SetTrigger("Attack2");
        isAttacking = true;
        currentComboChain++;
        if (currentComboChain == 2)
        {
            comboStage = 0;
            currentComboChain = 0;
        }
        yield return new WaitForSeconds(comboAttackDelay);
        isAttacking = false;
        timeSinceLastAttack = 0f;
    }

    void PerformMagic1()
    {
        comboStage = 0;
        animator.SetTrigger("Magic1");
        isAttacking = true;
        timeSinceLastMagic = 0f;
        enemyMP -= magicCost; // Deduct MP for using Magic1
    }

    void PerformMagic2()
    {
        comboStage = 0;
        animator.SetTrigger("Magic2");
        isAttacking = true;
        timeSinceLastMagic = 0f;
        enemyMP -= magic2Cost; // Deduct MP for using Magic2
    }

    void DealDamage(int damage)
    {
        // You can implement your damage logic here, like reducing player's health.
        // For example, you might have a PlayerHealth script on the player object.

        // Example code (assuming PlayerHealth is a script on the player):
         PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
         if (playerHealth != null)
         {
             playerHealth.TakeDamage(damage);
         }
    }

    void ResetCombo()
    {
        comboStage = 0;
        currentComboChain = 0;
        timeSinceLastAttack = 0f;
    }

    // Add a method to check if the enemy can see the player through objects with the "2DCollision" tag
    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, chaseDistance);
        if (hit.collider != null && hit.collider.CompareTag("2DCollision"))
        {
            return false; // Can't see through objects with the "2DCollision" tag
        }
        return true;
    }
}
