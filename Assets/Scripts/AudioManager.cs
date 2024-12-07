using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages all audio functionalities in the game, including background and SFX.
/// Implements Singleton for global access and ensures scalability and reusability.
/// Author: Ivonne Martinez
/// Date: 21/11/2024
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Singleton instance
    [Header("Music")]
    [Tooltip("AudioSource for background music")]
    public AudioSource bgMusicSource;
    [Tooltip("List of background music clips for random play")]
    public List<AudioClip> backgroundMusicClips;
    [Header("SFX")]
    [Tooltip("AudioSource for general sound effects (SFX)")]
    public AudioSource sfxSource;
    [Tooltip("List of general SFX clips (Indexed)")]
    public List<AudioClip> sfxClips;
    [Header("Footsteps SFX")]
    [Tooltip("List of footsteps sound effects for random play")]
    public List<AudioClip> footstepsClips;
    [Header("Button SFX")]
    [Tooltip("List of button click sound effects for random play")]
    public List<AudioClip> buttonClips;
    [Header("Keypad SFX")] // New section for keypad-specific sounds
    public List<AudioClip> keypadClips; // Dedicated list for keypad sounds
    private float originalBgMusicVolume; // To store the original volume
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayMenuMusic(); // Start with menu music
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        originalBgMusicVolume = bgMusicSource.volume; // Store the original volume
        AssignButtonSounds();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignButtonSounds(); // Re-assign button sounds for the new scene
    }
    /// <summary>
    /// Plays the first background music clip (typically for the menu).
    /// </summary>
    public void PlayMenuMusic()
    {
        if (backgroundMusicClips.Count > 0 && bgMusicSource.clip != backgroundMusicClips[0])
        {
            bgMusicSource.clip = backgroundMusicClips[0];
            bgMusicSource.loop = true;
            bgMusicSource.Play();
        }
    }
    /// <summary>
    /// Plays a specific SFX by index.
    /// </summary>
    public void PlaySFX(int index)
    {
        if (sfxClips == null || sfxClips.Count == 0)
        {
            Debug.LogError("SFX clips list is empty or not assigned.");
            return;
        }

        if (sfxSource == null)
        {
            Debug.LogError("SFX AudioSource is not assigned.");
            return;
        }

        if (index >= 0 && index < sfxClips.Count)
        {
            Debug.Log($"Playing SFX: {sfxClips[index].name}");
            sfxSource.PlayOneShot(sfxClips[index]);
        }
        else
        {
            Debug.LogWarning($"SFX index out of range: {index}. Valid range is 0 to {sfxClips.Count - 1}.");
        }
    }
    /// <summary>
    /// Plays a random sound effect for footsteps.
    /// </summary>
    public void PlayFootstepsSFX()
    {
        PlayRandomSFX(footstepsClips);
    }
    private void PlayRandomSFX(List<AudioClip> clips)
    {
        if (clips.Count > 0)
        {
            AudioClip randomClip = clips[Random.Range(0, clips.Count)];
            sfxSource.PlayOneShot(randomClip);
        }
        else
        {
            Debug.LogWarning("No sound effects available in the list.");
        }
    }
    /// <summary>
    /// Play a random button click sound effect.
    /// </summary>
    public void PlayButtonSFX()
    {
        if (buttonClips.Count > 0)
        {
            AudioClip randomClip = buttonClips[Random.Range(0, buttonClips.Count)];
            sfxSource.PlayOneShot(randomClip);
        }
    }
    /// <summary>
    /// Assigns the PlayButtonSFX method to all buttons in the scene.
    /// </summary>
    public void AssignButtonSounds()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayButtonSFX());
        }
    }
    // Play sound for Keypad by index
    public void PlayKeypadSFX(int index)
    {
        if (keypadClips.Count > 0)
        {
            sfxSource.PlayOneShot(keypadClips[0]); // Always play the first sound in the list
        }
        else
        {
            Debug.LogWarning("No Keypad SFX available in the list.");
        }
    }
    /// <summary>
    /// Lowers the volume of the background music.
    /// </summary>
    public void LowerBgMusicVolume(float newVolume)
    {
        bgMusicSource.volume = Mathf.Clamp(newVolume, 0f, originalBgMusicVolume);
    }
    /// <summary>
    /// Restores the original volume of the background music.
    /// </summary>
    public void RestoreBgMusicVolume()
    {
        bgMusicSource.volume = originalBgMusicVolume;
    }
}