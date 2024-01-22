using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStuff : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 5f;
    public float attackDistance = 2f;
    public float rotationSpeed = 5f;

    private bool isAttacking = false;
    private float timeSinceLastAttack = 0f;
    private bool hasSeenPlayer = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        RotateTowardsPlayer();

        if (isAttacking)
        {
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= 1.5f)  // Adjust as needed
            {
                isAttacking = false;
                timeSinceLastAttack = 0f;
            }
        }

        if (!hasSeenPlayer && distanceToPlayer <= 10f)  // Adjust the distance as needed
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

    System.Collections.IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        isAttacking = true;

        // Deal damage during the attack
        DealDamage(225f, 0f);  // Adjust damage values as needed

        yield return new WaitForSeconds(1.5f);  // Adjust as needed
    }

    void DealDamage(float physicalDamage, float magicalDamage)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(physicalDamage, magicalDamage);
        }
    }
}

//public class EnemyStuff : MonoBehaviour
//{
//    private Transform player;
//    private NavMeshAgent navAgent;
//    private Animator animator;

//    public float chaseDistance = 10f;
//    public float attackDistance = 2f;
//    public float rushDistance = 10f;
//    public float longRangeDistance = 20f;
//    public float rotationSpeed = 5f;
//    public float comboAttackDelay = 1.5f;
//    public float comboResetTime = 3f;
//    public float magicCooldown = 5f;

//    private bool isChasing = false;
//    private bool isAttacking = false;
//    private float timeSinceLastAttack = 0f;
//    private float timeSinceLastMagic = 0f;
//    private bool hasSeenPlayer = false;

//    private int comboStage = 0;
//    private int currentComboChain = 0;

//    private float enemyHP = 1500f;
//    public float cautiousThreshold = 30f;

//    private float enemyMP = 250f;
//    private float magicCost = 2f;
//    private float magic2Cost = 5f;
//    private float maxHP = 1500f;

//    private float normalAttackDamage = 225f;
//    private float comboAttackDamage = 675f;
//    private float rushAttackDamage = 300f;
//    private float magic1Damage = 195f;
//    private float magic2Damage = 450f;

//    private bool isChasingPlayer = false;

//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player").transform;
//        navAgent = GetComponent<NavMeshAgent>();
//        animator = GetComponent<Animator>();
//    }

//    void Update()
//    {
//        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

//        if (distanceToPlayer <= rushDistance && enemyHP > cautiousThreshold && CanSeePlayer())
//        {
//            isChasing = true;
//            isChasingPlayer = true;
//            navAgent.SetDestination(player.position);
//            animator.SetBool("isWalking", true);

//            if (distanceToPlayer <= attackDistance && !isAttacking)
//            {
//                if (comboStage == 0)
//                {
//                    StartCoroutine(ComboAttack1());
//                }
//                else if (comboStage == 1)
//                {
//                    StartCoroutine(ComboAttack2());
//                }
//            }
//        }
//        else if (isChasing)
//        {
//            isChasing = false;
//            isChasingPlayer = false;
//            navAgent.velocity = Vector2.zero;
//            animator.SetBool("isWalking", false);
//        }

//        if (isChasingPlayer && distanceToPlayer > 50f)
//        {
//            isChasing = false;
//            isChasingPlayer = false;
//            navAgent.velocity = Vector2.zero;
//            animator.SetBool("isWalking", false);
//        }

//        if (distanceToPlayer > longRangeDistance && !isAttacking && timeSinceLastMagic >= magicCooldown && hasSeenPlayer)
//        {
//            if (enemyMP >= magicCost)
//            {
//                if (enemyHP < maxHP * 0.5f && Random.value < 0.7f)
//                {
//                    PerformMagic2();
//                }
//                else
//                {
//                    PerformMagic1();
//                }
//            }
//        }
//        else
//        {
//            timeSinceLastMagic += Time.deltaTime;
//        }

//        RotateTowardsPlayer();

//        if (isAttacking)
//        {
//            timeSinceLastAttack += Time.deltaTime;
//            if (timeSinceLastAttack >= comboResetTime)
//            {
//                ResetCombo();
//            }
//        }

//        if (!hasSeenPlayer && distanceToPlayer <= chaseDistance)
//        {
//            hasSeenPlayer = true;
//        }
//    }

//    void RotateTowardsPlayer()
//    {
//        Vector2 direction = (player.position - transform.position).normalized;
//        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
//    }

//    IEnumerator ComboAttack1()
//    {
//        comboStage = 1;
//        animator.SetTrigger("Attack1");
//        isAttacking = true;
//        currentComboChain++;
//        if (currentComboChain == 2)
//        {
//            comboStage = 0;
//            currentComboChain = 0;
//        }
//        yield return new WaitForSeconds(comboAttackDelay);
//        isAttacking = false;
//        timeSinceLastAttack = 0f;
//    }

//    IEnumerator ComboAttack2()
//    {
//        comboStage = 2;
//        animator.SetTrigger("Attack2");
//        isAttacking = true;
//        currentComboChain++;
//        if (currentComboChain == 2)
//        {
//            comboStage = 0;
//            currentComboChain = 0;
//        }
//        yield return new WaitForSeconds(comboAttackDelay);
//        isAttacking = false;
//        timeSinceLastAttack = 0f;
//    }

//    void PerformMagic1()
//    {
//        comboStage = 0;
//        animator.SetTrigger("Magic1");
//        isAttacking = true;
//        timeSinceLastMagic = 0f;
//        enemyMP -= magicCost;
//    }

//    void PerformMagic2()
//    {
//        comboStage = 0;
//        animator.SetTrigger("Magic2");
//        isAttacking = true;
//        timeSinceLastMagic = 0f;
//        enemyMP -= magic2Cost;
//    }

//    //IEnumerator ComboAttack1()
//    //{
//    //    comboStage = 1;
//    //    animator.SetTrigger("Attack1");
//    //    isAttacking = true;
//    //    currentComboChain++;

//    //    // Deal damage during the attack
//    //    DealDamage(normalAttackDamage);

//    //    if (currentComboChain == 2)
//    //    {
//    //        comboStage = 0;
//    //        currentComboChain = 0;
//    //    }

//    //    yield return new WaitForSeconds(comboAttackDelay);
//    //    isAttacking = false;
//    //    timeSinceLastAttack = 0f;
//    //}

//    //IEnumerator ComboAttack2()
//    //{
//    //    comboStage = 2;
//    //    animator.SetTrigger("Attack2");
//    //    isAttacking = true;
//    //    currentComboChain++;

//    //    // Deal damage during the attack
//    //    DealDamage(comboAttackDamage);

//    //    if (currentComboChain == 2)
//    //    {
//    //        comboStage = 0;
//    //        currentComboChain = 0;
//    //    }

//    //    yield return new WaitForSeconds(comboAttackDelay);
//    //    isAttacking = false;
//    //    timeSinceLastAttack = 0f;
//    //}

//    //void PerformMagic1()
//    //{
//    //    comboStage = 0;
//    //    animator.SetTrigger("Magic1");
//    //    isAttacking = true;

//    //    // Deal damage during the magic attack
//    //    DealDamage(magic1Damage);

//    //    timeSinceLastMagic = 0f;
//    //    enemyMP -= magicCost;
//    //}

//    //void PerformMagic2()
//    //{
//    //    comboStage = 0;
//    //    animator.SetTrigger("Magic2");
//    //    isAttacking = true;

//    //    // Deal damage during the magic attack
//    //    DealDamage(magic2Damage);

//    //    timeSinceLastMagic = 0f;
//    //    enemyMP -= magic2Cost;
//    //}

//    void DealDamage(float physicalDamage, float magicalDamage)
//    {
//        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

//        if (playerHealth != null)
//        {
//            playerHealth.TakeDamage(physicalDamage, magicalDamage);
//        }
//    }

//    // Example for physical attack
//    //DealDamage(normalAttackDamage, 0f);

//    // Example for magical attack
//    //DealDamage(0f, magic1Damage);


//    void ResetCombo()
//    {
//        comboStage = 0;
//        currentComboChain = 0;
//        timeSinceLastAttack = 0f;
//    }

//    bool CanSeePlayer()
//    {
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, chaseDistance);
//        if (hit.collider != null && hit.collider.CompareTag("Ground"))
//        {
//            return false;
//        }
//        return true;
//    }


//}

