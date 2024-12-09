using UnityEngine;
using System.Collections;
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

    private void Start()
    {
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
            // Inicia la coroutine para restaurar el volumen después de que el SFX termine
            StartCoroutine(RestoreMusicVolumeAfterSFX(AudioManager.Instance.sfxClips[0].length));
        }
        else
        {
            Debug.LogWarning("AudioManager instance not found! Ensure it exists in the scene.");
        }
    }
    // Coroutine para restaurar el volumen de la música después de que termine el SFX
    private IEnumerator RestoreMusicVolumeAfterSFX(float sfxDuration)
    {
        // Esperamos hasta que el SFX termine
        yield return new WaitForSeconds(sfxDuration);

        // Restauramos el volumen de la música de fondo
        AudioManager.Instance.RestoreBgMusicVolume();
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