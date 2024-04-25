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
                Quaternion VoidRotation = Quaternion.identity;
                if (transform.localScale.x == -1)
                {
                    VoidRotation = Quaternion.Euler(0f, 180f, 0f);
                }
                Instantiate(ProjectilePrefab, Launcher.position, VoidRotation);
                StartCoroutine(ShootWithCooldown());
                Debug.Log("Ultimate Move: Abyssal Void Collapse");
            }
            if (Input.GetKeyDown(KeyCode.F) && ToGo)
            {
                Quaternion fireballRotation = Quaternion.identity;
                if (transform.localScale.x == -1)
                {
                    fireballRotation = Quaternion.Euler(0f, 180f, 0f);
                }

                Instantiate(FireBallSpellPrefab, FireBallLuanch.position, fireballRotation);
                Instantiate(FireBallSpellPrefab, FireBallLuanch2.position, fireballRotation);
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