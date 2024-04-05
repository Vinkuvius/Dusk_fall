using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ProjectileBehavior ProjectilePrefab;
    public FireBallSpell FireBallSpellPrefab;
    public MeleeAtk Melee;
    public Transform Launcher;
    public Transform WeaponPoint;
    public Transform FireBallLuanch;
    public Transform FireBallLuanch2;

    public float CoolDown = 60f;
    public float Rest = 3f;
    private bool canFire = true;
    private bool ToGo = true;
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetMouseButtonDown(0) && canFire)
            {
                Instantiate(ProjectilePrefab, Launcher.position, transform.rotation);
                StartCoroutine(ShootWithCooldown());
                Debug.Log("Ultimate Move: Abyssal Void Collapse");
            }
            if (Input.GetKeyDown(KeyCode.F) && ToGo)
            {
                Instantiate(FireBallSpellPrefab, FireBallLuanch.position, transform.rotation);
                Instantiate(FireBallSpellPrefab, FireBallLuanch2.position, transform.rotation);
                StartCoroutine(MagicBeluga());
                Debug.Log("Fireball");
            }
        }
    }

    IEnumerator ShootWithCooldown()
    {
        // Start cooldown
        canFire = false;
        yield return new WaitForSeconds(CoolDown);
        canFire = true;
    }

    IEnumerator MagicBeluga() 
    {
        ToGo = false;
        yield return new WaitForSeconds(Rest);
        ToGo = true;
    }
}