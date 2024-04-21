using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvl2 : MonoBehaviour
{
    public Transform bulletShootPoint1;
    public LayerMask GroundLayer;
    public float moveSpeed = 7.5f;
    public float attackRange = 25f;
    public int damage = 10;
    public AudioClip IceGolemWalk;
    public AudioClip hurtSound;
    public AudioClip defeatSound;

    private Transform player;
    public float health = 350f;
    private AudioSource audioSource;
    public float rayDistance;
    public bool isGround;

    public float timer1 = 0.25f;
    public float closingTimer1;
    public float offset;

    public EnemyProjectile Projectile;
    private Vector2 lastPosition;
    public float moveThreshold = 0.1f;

    public House house;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (health <= 0)
        {
            PlaySound(defeatSound);
            Invoke("DestroyBoss", 3.0f);
            house.gameObject.SetActive(true);
        }

        if (Vector3.Distance(transform.position, player.position) <= 55)
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

        if (isMoving())
        {
            PlaySound(IceGolemWalk);
        }
    }

    void Shoot1()
    {
        Vector3 directionToPlayer = (player.position - bulletShootPoint1.position).normalized;
        Quaternion rotationToPlayer = Quaternion.LookRotation(Vector3.forward, directionToPlayer);

        EnemyProjectile p1 = Instantiate(Projectile, bulletShootPoint1.position, rotationToPlayer);
        p1.fireDirection = directionToPlayer;
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
