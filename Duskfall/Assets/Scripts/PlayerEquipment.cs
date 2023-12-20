using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEditor.Progress;

public class PlayerEquipment : MonoBehaviour
{
    // Assuming you have an Item class that contains attributes like armor and magic resistance
    public Item equippedArmor;
    public Item equippedMagicResistanceItem;

    // This method is called to apply the effects of equipped items on the player
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
}
