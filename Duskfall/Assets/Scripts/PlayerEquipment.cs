using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEditor.Progress;

public class PlayerEquipment : MonoBehaviour
{
    // Reference the Item class from PlayerHealth
    public PlayerHealth.Item equippedArmor;
    public PlayerHealth.Item equippedMagicResistanceItem;

    public void ApplyEquipmentEffects()
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            float armor = (equippedArmor != null) ? equippedArmor.armor : 0f;
            float magicResistance = (equippedMagicResistanceItem != null) ? equippedMagicResistanceItem.magicResistance : 0f;

            playerHealth.SetArmor(armor);
            playerHealth.SetMagicResistance(magicResistance);
        }
    }
