using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectionRange = 10f;
    public int maxHealth = 10;
    public  int currentHealth;
    public Transform player;

    public AudioClip walkSound;
    public AudioClip hurtSound;
    public AudioClip defeatSound;
    private AudioSource audioSource;

    private Vector2 lastPosition;
    public float moveThreshold = 0.1f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            movementDirection = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
        }

        if (currentHealth <= 0)
        {
            PlaySound(defeatSound);
            Invoke("DestroyEnemy", 1.0f);
        }

        if (isMoving())
        {
            PlaySound(walkSound);
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

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<PlayerHealth>().currentHealth -= 5;
        }
    }
}
