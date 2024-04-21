using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    public GameObject winScreen; // Reference to the win screen GameObject
    private bool isGameWon = false; // Flag to track if the game is won

    void Update()
    {
        // Check if the player presses the "Q" key and the game is not already won
        if (!isGameWon)
        {
            // Check if the player is colliding with the house
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    // Show the win screen
                    if (winScreen != null)
                    {
                        winScreen.SetActive(true);
                        isGameWon = true; // Set the flag to true
                        Time.timeScale = 0f; // Pause the game
                        Invoke("CloseGame", 5f); // Call CloseGame function after 5 seconds
                        break;
                    }
                }
            }
        }
    }

    void CloseGame()
    {
        // Close the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    void OnDrawGizmosSelected()
    {
        // Draw a box representing the collider for visualization purposes
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
