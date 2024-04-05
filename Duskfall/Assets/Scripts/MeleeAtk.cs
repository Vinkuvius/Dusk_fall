using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtk : MonoBehaviour
{
    public GameObject Attackpoint;
    private Animator animator;
    public LayerMask Enemy;
    public float radius;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                rend.enabled = true;
                animator.SetBool("IsAttacking", true);
                StartCoroutine(DeactivateAfterDely(0.5f));
            }
        }
    }

    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(Attackpoint.transform.position, radius, Enemy);
    }

    public void endAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Attackpoint.transform.position,radius);
    }

    IEnumerator DeactivateAfterDely(float delay)
    {
        yield return new WaitForSeconds(delay);
        rend.enabled = false;
        animator.SetBool("IsAttacking", false);
    }

    public void OnTriggerEnter2D(Collider2D Enemy1)
    {
        if (rend.enabled)
        {
            if (Enemy1.gameObject.CompareTag("Enemy"))
            {
                Enemy1.gameObject.GetComponent<EnemyStuff>().health -= 15f;
            }
            else if (Enemy1.gameObject.CompareTag("Boss"))
            {
                Enemy1.gameObject.GetComponent<BossEnemy>().health -= 70f;
            }
        }
    }

}
