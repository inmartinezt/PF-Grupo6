using UnityEngine;

/// <summary>
/// Interaction with the Radio object inside the scene to play an audio. Referenced the AudioManager instance.
/// Author: Ivonne Martinez
/// Date: 05/12/2024
/// </summary>
public class RadioInteraction : MonoBehaviour
{
    public float interactionDistance = 4f; // Distance to trigger interaction
    private Transform playerTransform; // Reference to the player's transform
    private bool isNear = false; // Is the player near the object
    public GameObject Sub;

    private void Start()
    {
        Sub.SetActive(false);
        // Find the player by tag (make sure the player object has the "Player" tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Ensure the Player object has the 'Player' tag.");
        }
    }

    private void Update()
    {
        // Check proximity to the player
        if (playerTransform != null)
        {
            isNear = Vector3.Distance(transform.position, playerTransform.position) <= interactionDistance;
            
        } 
        if (isNear)
        {
            Sub.SetActive(true);
        }
        else Sub.SetActive(false);

        // Trigger interaction when near and pressing "E"
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            PlayRadioAudio();
        }
    }

    private void PlayRadioAudio()
    {
        if (AudioManager.Instance != null)
        {
            // Lower the background music volume
            AudioManager.Instance.LowerBgMusicVolume(0.1f);

            // Play the SFX
            AudioManager.Instance.PlaySFX(0);

            // Restore the background music volume after the SFX finishes
            float sfxDuration = AudioManager.Instance.sfxClips[0].length; // Duration of the SFX
            Invoke(nameof(RestoreMusicVolume), sfxDuration);
        }
        else
        {
            Debug.LogWarning("AudioManager instance not found! Ensure it exists in the scene.");
        }
    }

    private void RestoreMusicVolume()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.RestoreBgMusicVolume();
        }
    }

    private void OnDrawGizmos()
    {
        // Draw interaction range in the editor for debugging
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
