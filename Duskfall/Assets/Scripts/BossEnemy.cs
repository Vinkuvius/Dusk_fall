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
    public float health = 30; // Fiende HP
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
    //public Projectile Projectile;
    public WinCondition Win;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            // Bossen är dödad, Kalla till win condition check
            Win.CheckWinCondition();
            // Förstör Bossen
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().currentHealth -= 60;
        }
    }
}
