using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float Speed = 4.5f;
    public float timeToDespawn = 0.9f;
    public float time;


    public void Start()
    {
        // Shortcut for timeToDespawn
        time = timeToDespawn;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed * 4;
        // States that when the timer of "timeToDespawn" reaches zero the projectile despawns
        if (time < 0f)
        {
            Destroy(gameObject);
            return;
        }
        time -= Time.deltaTime;
    }

    //This is meant to be a insta kill move, dot mess with it
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        //else if (other.gameObject.CompareTag("Boss"))
        //{
        //    other.gameObject.GetComponent<BossEnemy>();
        //    if (other.gameObject.GetComponent<BossEnemy>() != null)
        //    {
        //        other.gameObject.GetComponent<BossEnemy>().PlaySound(hurtSound);
        //    }
        //    other.gameObject.GetComponent<BossEnemy>().health /=2;
        //}
        else if (other.gameObject.CompareTag("Boss"))
        {
            BossEnemy bossEnemy = other.gameObject.GetComponent<BossEnemy>();

            if (bossEnemy != null)
            {
                // Access the hurtSound property from the BossEnemy component
                AudioClip bossHurtSound = bossEnemy.hurtSound;

                // Check if the bossHurtSound is not null before playing
                if (bossHurtSound != null)
                {
                    bossEnemy.PlaySound(bossHurtSound); // Play the hurt sound
                }

                bossEnemy.health /= 2;
            }

            Destroy(gameObject);
        }
    }
}