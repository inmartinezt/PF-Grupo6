using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages all Keypad states and sounds remitted to AudioManager
/// Author: Ivonne Martinez
/// Date: 28/11/2024
/// </summary>
public class KeypadInteraction : MonoBehaviour
{
    public GameObject canvas; // Canvas to show when interacting
    public float interactionDistance = 3f; // Distance to trigger interaction
    public string nextSceneName = "NextScene"; // Name of the next scene to load
    private bool isNear = false; // Is the player near the object
    private bool isInteracting = false; // Is the Canvas active (interaction mode)
    private string inputSequence = ""; // Track the key presses for sequence "6" and "4"
    private Transform playerTransform; // Store player's transform
    private void Start()
    {
        // Find the player based on their tag (make sure the player has the "Player" tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Ensure the Player object has the 'Player' tag.");
        }

        canvas.SetActive(false); // Hide the canvas initially
    }
    private void Update()
    {
        // Check proximity to the player
        if (playerTransform != null)
        {
            isNear = Vector3.Distance(transform.position, playerTransform.position) <= interactionDistance;
        }
        // Toggle Canvas visibility when near and pressing "E"
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = !isInteracting;
            canvas.SetActive(isInteracting);

            if (isInteracting)
            {
                inputSequence = ""; // Reset the sequence on interaction start
            }
        }
        // Handle key inputs when interacting
        if (isInteracting)
        {
            // Track key presses for the "6" and "4" sequence
            if (Input.GetKeyDown(KeyCode.Alpha6)) inputSequence += "6";
            if (Input.GetKeyDown(KeyCode.Alpha4)) inputSequence += "4";

            // If the sequence "6" and "4" is completed, load the next scene
            if (inputSequence == "64")
            {
                SceneManager.LoadScene(nextSceneName);
            }
            // Press "Q" to close the interaction
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isInteracting = false;
                canvas.SetActive(false);
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Draw the interaction range in the editor for debugging
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}