using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ProjectileBehavior ProjectilePrefab;
    public MeleeWeapon MeleeWeaponPrefab;
    public Transform Launcher;
    public Transform WeaponPoint;

    public float CoolDown = 60f;
    public float Recharge = 1f;
    private bool canFire = true;
    private bool IsReady = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            Instantiate(ProjectilePrefab, Launcher.position, transform.rotation);
            StartCoroutine(ShootWithCooldown());
            Debug.Log("Ultimate Move: Abyssal Void Collapse");
        }
        //melee attack, the "R" is temporary, QOL for me
        if (Input.GetKeyDown(KeyCode.R) && IsReady)
        {
            Instantiate(MeleeWeaponPrefab, WeaponPoint.position, transform.rotation);
            StartCoroutine(StabWithRecharge());
            Debug.Log("Stabby Stab");
        }
    }

    IEnumerator ShootWithCooldown()
    {
        // Start cooldown
        canFire = false;
        yield return new WaitForSeconds(CoolDown);
        canFire = true;
    }

    IEnumerator StabWithRecharge()
    {
        IsReady = false;
        yield return new WaitForSeconds(Recharge);
        IsReady = true;
    }
}