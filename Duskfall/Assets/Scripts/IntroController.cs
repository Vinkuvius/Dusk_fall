using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public Transform character;
    public Vector2 startPosition; // Initial position of the character
    public float runDistance = 10f; // Distance to run from left to right
    public float movementSpeed = 3f; // Speed of movement
    public float resetDelay = 2f; // Delay before resetting the position

    private Animator animator;


    private void Start()
    {
        // Store the initial position of the character
        startPosition = character.position;

        // Get the Animator component attached to the character
        animator = character.GetComponent<Animator>();

        // Start the running animation
        if (animator != null)
        {
            animator.SetBool("Character1Run", true); // Set the "Character1Run" parameter to true to trigger the running animation
        }
    }

    private void Update()
    {
        // Check if the character has reached the end of the run
        if (character.position.x < startPosition.x + runDistance)
        {
            // Move the character to the right
            character.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }
        else
        {
            // Reset the character's position after reaching the run distance
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        // Reset the character's position to the starting position
        character.position = startPosition;

        // Stop the running animation
        if (animator != null)
        {
            animator.SetBool("Character1Run", false); // Set the "Character1Run" parameter to false to stop the running animation
        }
    }
}