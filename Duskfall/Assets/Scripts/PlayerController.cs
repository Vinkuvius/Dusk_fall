using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ProjectileBehavior ProjectilePrefab;
    public Transform Launcher;

    public float CoolDown = 60f;
    private bool canFire = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            Instantiate(ProjectilePrefab, Launcher.position, transform.rotation);
            StartCoroutine(ShootWithCooldown());
        }
    }

    IEnumerator ShootWithCooldown()
    {
        // Start cooldown
        canFire = false;
        yield return new WaitForSeconds(CoolDown);
        canFire = true;
    }
}