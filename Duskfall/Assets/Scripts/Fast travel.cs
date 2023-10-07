using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastTravelPoint : MonoBehaviour
{
    public string destinationScene; // The scene to load when using this fast travel point
    public Transform destinationTransform; // The position to teleport the player to

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Display a menu with available destinations
            DisplayDestinationMenu(other.gameObject);
        }
    }

    private void DisplayDestinationMenu(GameObject player)
    {
        // You can implement your own UI for selecting a destination
        // For simplicity, let's assume you have a UI canvas named "FastTravelMenu" with buttons for each destination.
        // When a button is clicked, call the TeleportToDestination method with the corresponding destination scene and position.
    }

    public void TeleportToDestination(string destinationScene, Transform destinationTransform, GameObject player)
    {
        // Load the destination scene
        SceneManager.LoadScene(destinationScene);

        // Teleport the player to the specified position
        player.transform.position = destinationTransform.position;
    }
}

