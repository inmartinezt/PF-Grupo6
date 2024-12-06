using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour
{
    [Header("Cinematic Settings")]
    public Image[] cinematicImages; // Array to hold the images for the cinematic
    public float[] imageDisplayDurations; // Array to specify the duration for each image
    public int[] audioClipIndices; // Indices to specify which audio clips to play for this cinematic

    private int currentImageIndex = 0;
    private bool isCinematicPlaying = true;

    private void Start()
    {
        // Start the cinematic when the scene is loaded
        StartCoroutine(PlayCinematic());
    }

    private IEnumerator PlayCinematic()
    {
        // Hide all images initially
        foreach (Image img in cinematicImages)
        {
            img.enabled = false;
        }

        // Start playing the audio for this cinematic
        PlayCinematicAudio();

        // Display images sequentially
        while (currentImageIndex < cinematicImages.Length)
        {
            cinematicImages[currentImageIndex].enabled = true; // Show current image
            yield return new WaitForSeconds(imageDisplayDurations[currentImageIndex]); // Wait for the specified duration
            cinematicImages[currentImageIndex].enabled = false; // Hide current image
            currentImageIndex++; // Move to the next image
        }

        // After all images are displayed, load the next scene (MainMenu or whatever you need)
        LoadNextScene();
    }

    // Function to play the audio associated with this cinematic
    private void PlayCinematicAudio()
    {
        if (audioClipIndices.Length > 0 && audioClipIndices[currentImageIndex] >= 0)
        {
            // Play the audio using AudioManager based on the index for the current image
            AudioManager.Instance.PlaySFX(audioClipIndices[currentImageIndex]);
        }
    }

    // Function to load the next scene (MainMenu or other specified scene)
    private void LoadNextScene()
    {
        SceneManager.LoadScene("MainMenu"); // You can change this scene name dynamically later
    }

    // Method to pass a different set of images, durations, and audio clip indices to this controller (useful if you need to trigger different cinematics)
    public void SetCinematicDetails(Image[] images, float[] durations, int[] audioIndices)
    {
        cinematicImages = images;
        imageDisplayDurations = durations;
        audioClipIndices = audioIndices;
    }
}