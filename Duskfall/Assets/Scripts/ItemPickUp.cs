using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            // Handle item pickup logic here
            HandleItemPickup();
        }
    }

    private void HandleItemPickup()
    {
        // Display a message when an item is picked up
        Debug.Log("Thou hast laid hands upon an item! Thou shouldst take a gander at its wonders");
        // You can also add logic to add the item to the player's inventory here if needed.

        // Deactivate the item object (you can also destroy it if needed)
        gameObject.SetActive(false);
    }
}
