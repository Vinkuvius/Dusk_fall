using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public Transform bulletShootPoint1;
    public Transform bulletShootPoint2;
    public Transform bulletShootPoint3;
    public LayerMask GroundLayer;
    public float moveSpeed = 0.5f; // Hastigheten som fienden rör sig på
    public float attackRange = 5f; // Attackavståndet
    public int damage = 10;

    private Transform player; // Refererar till player's transform
    public float health = 300; // Fiende HP
    public float rayDistance;
    public bool isGround;

    public float timer1 = 0.2f;
    public float closingTimer1;
    public float timer2 = 0.4f;
    public float closingTimer2;
    public float timer3 = 0.6f;
    public float closingTimer3;
    public float offset;
    //public static Weapon Instance;
    public EnemyProjectile Projectile;
    public WinCondition Win;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Finds player
    }


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            // Boss is dead, calls the win condition script
            Win.CheckWinCondition();
            // Destroys the boss
            Destroy(gameObject);
        }

        if (Vector3.Distance(transform.position, player.position) <= 15)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            player.position,
            moveSpeed * Time.deltaTime);
        }
        timer1 += Time.deltaTime;
        if (timer1 > closingTimer1)
        {
            timer1 = 0.2f;
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                Shoot1();
            }
        }

        timer2 += Time.deltaTime;
        if (timer2 > closingTimer2)
        {
            timer2 = 0.4f;
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                Shoot2();
            }
        }
        timer3 += Time.deltaTime;
        if (timer3 > closingTimer3)
        {
            timer3 = 0.6f;
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                Shoot3();
            }
        }
    }

    void Shoot1()
    {
        // Koden som gör att fienden skjuter
        EnemyProjectile p1 = Instantiate(Projectile, bulletShootPoint1.position, Quaternion.LookRotation(transform.forward, transform.up));
        p1.fireDirection = -transform.right;
    }
    void Shoot2()
    {
        EnemyProjectile p2 = Instantiate(Projectile, bulletShootPoint2.position, Quaternion.LookRotation(transform.forward, transform.up));
        p2.fireDirection = -transform.right;
    }
    void Shoot3()
    {
        EnemyProjectile p3 = Instantiate(Projectile, bulletShootPoint3.position, Quaternion.LookRotation(transform.forward, transform.up));
        p3.fireDirection = -transform.right;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().currentHealth -= 60;
        }
    }
}
