using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelection : MonoBehaviour
{
    public GameObject fireballSpellPrefab;
    public GameObject lightningSpellPrefab;
    public GameObject iceSpikeSpellPrefab;

    private GameObject selectedSpellPrefab;

    void Start()
    {
        // Set the default selected spell (fireball).
        selectedSpellPrefab = fireballSpellPrefab;
    }

    void Update()
    {
        // Check if the player is holding down the "X" key.
        if (Input.GetKey(KeyCode.X))
        {
            // While holding "X," cycle through the spell options.
            if (selectedSpellPrefab == fireballSpellPrefab)
            {
                selectedSpellPrefab = lightningSpellPrefab;
            }
            else if (selectedSpellPrefab == lightningSpellPrefab)
            {
                selectedSpellPrefab = iceSpikeSpellPrefab;
            }
            else if (selectedSpellPrefab == iceSpikeSpellPrefab)
            {
                selectedSpellPrefab = fireballSpellPrefab;
            }
        }

        // If the player releases the "X" key, cast the selected spell.
        if (Input.GetKeyUp(KeyCode.X))
        {
            CastSelectedSpell();
        }
    }

    void CastSelectedSpell()
    {
        // Instantiate and cast the selected spell.
        if (selectedSpellPrefab != null)
        {
            Instantiate(selectedSpellPrefab, transform.position, transform.rotation);
        }
    }
}
