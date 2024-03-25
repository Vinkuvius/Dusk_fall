using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float Speed = 4.5f;
    public float timeToDespawn = 1.5f;
    public float time;


    public void Start()
    {
        // Shortcut for timeToDespawn
        time = timeToDespawn;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
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
        else if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<BossEnemy>().health -= 150; 
        }

        Destroy(gameObject);
    }
}
