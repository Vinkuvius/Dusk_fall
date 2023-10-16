using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDoor : MonoBehaviour
{
    public PlayerInventory playerInventory; // Reference to the player's inventory
    public int requiredGolemCores = 5; // Number of golem cores required to open the door
    private bool isDoorOpen = false; // Flag to track if the door is open

    private void Update()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player Inventory is not assigned in the GolemDoor script.");
            return;
        }

        // Check if the player is within interaction distance and presses the "E" key
        float distanceToPlayer = Vector3.Distance(transform.position, playerInventory.transform.position);

        if (distanceToPlayer <= 3f && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory.HasItem("Golem Core") && playerInventory.GetItemCount("Golem Core") >= requiredGolemCores)
            {
                OpenDoor();
            }
            else
            {
                // Inform the player that they need more golem cores to open the door
                Debug.Log("Thou lacks the required amount of anccient artifacts");
            }
        }
    }

    private void OpenDoor()
    {
        // Remove the required golem cores from the player's inventory
        playerInventory.RemoveItem("Golem Core", requiredGolemCores);

        // Open the door (implement your door opening logic here)
        // For simplicity, we'll just deactivate the door GameObject.
        gameObject.SetActive(false);

        // Set the flag to indicate that the door is open
        isDoorOpen = true;
    }
}
