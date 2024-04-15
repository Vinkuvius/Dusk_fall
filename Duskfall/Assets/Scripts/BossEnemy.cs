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
    public AudioClip IceGolemWalk;
    public AudioClip hurtSound;
    public AudioClip defeatSound;

    private Transform player; // Refererar till player's transform
    public float health = 300f; // Fiende HP
    private AudioSource audioSource;
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
    private Vector2 lastPosition; // Variable to store the position of the enemy in the previous frame
    public float moveThreshold = 0.1f;
    public LevelLoader levelmenu;
    public WinCondition Winner;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Finds player
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Winner.CheckWinCondition();
            PlaySound(defeatSound);
            // Destroys the boss
            Invoke("DestroyBoss", 3.0f);
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

        if (isMoving())
        {
            PlaySound(IceGolemWalk);
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
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(60);
        }
    }

    public bool isMoving()
    {
        Vector2 currentPosition = transform.position;
        float distanceMoved = Vector2.Distance(currentPosition, lastPosition);

        // Update the lastPosition for the next frame
        lastPosition = currentPosition;

        // If the distance moved is greater than a small threshold, consider the enemy as moving
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