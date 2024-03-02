using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ProjectileBehavior ProjectilePrefab;
    public MeleeWeapon MeleeWeaponPrefab;
    public FireBallSpell FireBallSpellPrefab;
    public Transform Launcher;
    public Transform WeaponPoint;
    public Transform FireBallLuanch;

    public float CoolDown = 60f;
    public float Recharge = 1f;
    public float Rest = 3f;
    private bool canFire = true;
    private bool IsReady = true;
    private bool ToGo = true;

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
        if (Input.GetKeyDown(KeyCode.F) && ToGo)
        {
            Instantiate(FireBallSpellPrefab, FireBallLuanch.position, transform.rotation);
            StartCoroutine(MagicBeluga());
            Debug.Log("Fireball");
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

    IEnumerator MagicBeluga() 
    {
        ToGo = false;
        yield return new WaitForSeconds(Rest);
        ToGo = true;
    }
}