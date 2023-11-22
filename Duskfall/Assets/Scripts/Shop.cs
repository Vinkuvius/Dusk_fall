using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopMenu; // Reference to the shop menu UI GameObject
    public Transform player; // Reference to the player's GameObject
    public float interactionDistance = 10f; // Distance at which the player can interact with the shop
    private bool isShopMenuOpen = false; // Flag to track if the shop menu is open
    private DialogueManager dialogueManager; // Reference to the DialogueManager

    public GameObject shopUI;
    public PlayerInventory playerInventory;
    public Text shopItemListText;
    public Text messageText;

    // Add public fields for item prices
    public int greenPotionPrice = 5;
    public int bluePotionPrice = 10;
    public int redPotionPrice = 20;
    public int yellowPotionPrice = 30;
    public int purplePotionPrice = 45;
    public int GreenBandanaPrice = 40;

    private bool isShopOpen = false;

    private void DisplayWelcomeMessage()
    {
        // Display the welcome message in the DialogueManager
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue("Greetings. You've stumbled upon the CrystalGaze Emporium, or CrGaEm for short. I suppose there are mysteries and marvels around, or whatever. just buy something and go...");
        }
    }

    private void Start()
    {
        // Initialize the shop UI
        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }

        // Hide the message text
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isShopOpen)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }
    }

    private void OpenShop()
    {
        isShopOpen = true;
        shopUI.SetActive(true);

        // Build the shop item list based on player's inventory
        BuildShopItemList();
    }

    private void CloseShop()
    {
        isShopOpen = false;
        shopUI.SetActive(false);
    }

    private void BuildShopItemList()
    {
        // Create a list of items available in the shop
        string itemList = "Items for Sale:\n";

        // Logic for adding items based on player's golem cores
        if (playerInventory.HasItem("Golem Core (Green)"))
        {
            itemList += "1. Green Potion - " + greenPotionPrice + " Gold\n";
            itemList += "1. Greenbandana - " + GreenBandanaPrice + "Gold\n";
        }

        if (playerInventory.HasItem("Golem Core (Blue)"))
        {
            itemList += "2. Blue Potion - " + bluePotionPrice + " Gold\n";
        }

        if (playerInventory.HasItem("Golem Core (Red)"))
        {
            itemList += "3. Red Potion - " + redPotionPrice + " Gold\n";
        }

        if (playerInventory.HasItem("Golem Core (Yellow)"))
        {
            itemList += "4. Yellow Potion - " + yellowPotionPrice + " Gold\n";
        }

        // Include Purple Potion
        if (playerInventory.HasItem("Golem Core (Purple)"))
        {
            itemList += "5. Purple Potion - " + purplePotionPrice + " Gold\n";
        }

        // Display the item list in the shop UI
        shopItemListText.text = itemList;
    }

    public void PurchaseItem(int itemNumber)
    {
        // Logic for purchasing items
        int itemPrice = 0;

        switch (itemNumber)
        {
            case 1:
                itemPrice = greenPotionPrice;
                // Handle purchase of Green Potion
                playerInventory.RemoveItem("Gold", itemPrice);
                playerInventory.AddItem("Green Potion");
                messageText.text = "You purchased a Green Potion!";
                break;
            case 2:
                itemPrice = bluePotionPrice;
                // Handle purchase of Blue Potion
                playerInventory.RemoveItem("Gold", itemPrice);
                playerInventory.AddItem("Blue Potion");
                messageText.text = "You purchased a Blue Potion!";
                break;
            case 3:
                itemPrice = redPotionPrice;
                // Handle purchase of Red Potion
                playerInventory.RemoveItem("Gold", itemPrice);
                playerInventory.AddItem("Red Potion");
                messageText.text = "You purchased a Red Potion!";
                break;
            case 4:
                itemPrice = GreenBandanaPrice;
                //Handle purchase of Greenbandana
                playerInventory.RemoveItem("Gold", itemPrice);
                playerInventory.AddItem("GreenBandana");
                messageText.text = "You purchased a Greenbandana!";
                break;
            case 5:
                itemPrice = purplePotionPrice;
                //Handle purchase of Pruple potion
                playerInventory.RemoveItem("Gold", itemPrice);
                playerInventory.AddItem("Purple potion");
                messageText.text = "You purchased a Pruple potion";
                break;
            case 6:
                itemPrice = yellowPotionPrice;
                //Handle purchase of Yellow potion
                playerInventory.RemoveItem("Gold", itemPrice);
                playerInventory.AddItem("Yellow potion");
                messageText.text = "You purchased a Yellow potion";
                break;

            default:
                messageText.text = "Invalid item number.";
                break;
        }

        // Update the shop item list
        BuildShopItemList();
        messageText.gameObject.SetActive(true);
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

    
}
