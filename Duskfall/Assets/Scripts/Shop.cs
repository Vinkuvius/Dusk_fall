using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopMenu; // Reference to the shop menu UI GameObject
    public Transform player; // Reference to the player's GameObject
    public float interactionDistance = 10f; // Distance at which the player can interact with the shop
    private bool isShopMenuOpen = false; // Flag to track if the shop menu is open
    private DialogueManager dialogueManager; // Reference to the DialogueManager

    private void Start()
    {
        // Disable the shop menu initially
        if (shopMenu != null)
        {
            shopMenu.SetActive(false);
        }

        // Find the DialogueManager in the scene
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        // Check if the player is within interaction distance and presses the "E" key
        float distanceToShop = Vector3.Distance(transform.position, player.position);
        if (distanceToShop <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Toggle the shop menu on and off
                ToggleShopMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isShopMenuOpen)
            {
                // Close the shop menu if it's open and the "Esc" key is pressed
                CloseShopMenu();
            }
        }
    }

    private void ToggleShopMenu()
    {
        // Toggle the shop menu on and off
        isShopMenuOpen = !isShopMenuOpen;

        // Show or hide the shop menu UI accordingly
        if (shopMenu != null)
        {
            shopMenu.SetActive(isShopMenuOpen);

            // Lock or unlock player controls when the shop menu is open
            // You can implement this part based on your player controller script.
            // For example, you might set a flag in your player controller to disable movement and inputs while the shop menu is open.
        }

        // Display the welcome message when the shop menu is opened
        if (isShopMenuOpen)
        {
            DisplayWelcomeMessage();
        }
    }

    private void CloseShopMenu()
    {
        // Close the shop menu and unlock player controls
        isShopMenuOpen = false;

        if (shopMenu != null)
        {
            shopMenu.SetActive(false);

            // Unlock player controls (adjust based on your player controller script)
            // For example, set a flag to enable movement and inputs.
        }
    }

    private void DisplayWelcomeMessage()
    {
        // Display the welcome message in the DialogueManager
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue("Hark, and welcome to mine wondrous emporium of mystics, replete with mysteries and marvels to witness. Pray, what service may I render thee this day?");
        }
    }
}
