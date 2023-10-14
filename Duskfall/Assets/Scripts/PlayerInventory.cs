using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public void AddItem(string item)
    {
        // Add an item to the player's inventory
        if (inventory.ContainsKey(item))
        {
            inventory[item]++;
        }
        else
        {
            inventory.Add(item, 1);
        }
    }

    public bool HasItem(string item)
    {
        // Check if the player has a specific item in their inventory
        return inventory.ContainsKey(item) && inventory[item] > 0;
    }

    public int GetItemCount(string item)
    {
        // Get the count of a specific item in the player's inventory
        if (inventory.ContainsKey(item))
        {
            return inventory[item];
        }
        return 0;
    }

    public void RemoveItem(string item, int count = 1)
    {
        // Remove a specific number of items from the player's inventory
        if (inventory.ContainsKey(item) && inventory[item] >= count)
        {
            inventory[item] -= count;
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
        }
    }
}
