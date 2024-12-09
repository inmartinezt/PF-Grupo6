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
            // No reproducimos la música inicial aquí para evitar conflictos
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        originalBgMusicVolume = bgMusicSource.volume;
        AssignButtonSounds();

        // Play initial music if the starting scene is Cinematic1
        if (SceneManager.GetActiveScene().name == "Cinematic1")
        {
            PlayInitialMusic();
        }
    }
    // Subscribe to the scene loaded event
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Unsubscribe from the scene loaded event
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // On scene loaded, update the background music depending on the scene
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateBackgroundMusic(scene.name); // Cambia la música según la escena cargada
    }
    /// <summary>
    /// This method will play the correct background music based on the scene name.
    /// </summary>
    private void UpdateBackgroundMusic(string sceneName)
    {
        Debug.Log($"Scene loaded: {sceneName}. Updating background music...");
        // Restore volume before assigning new music
        RestoreBgMusicVolume();
        if (bgMusicSource.isPlaying)
        {
            Debug.Log("Stopping current background music.");
            bgMusicSource.Stop();
        }

        if (sceneName == "Cinematic1" || sceneName == "MainMenu" || sceneName == "Cinematic2")
        {
            Debug.Log("Playing music for cinematic or main menu scenes (Index 0).");
            PlayBackgroundMusic(0);
        }
        else if (sceneName == "GamePlay" || sceneName == "Level3")
        {
            Debug.Log("Playing music for gameplay or Level3 scenes (Index 1).");
            PlayBackgroundMusic(1);
        }
        else
        {
            Debug.LogWarning("Scene does not have associated background music.");
        }
    }
    /// <summary>
    /// Plays a background music clip based on the index from the list.
    /// </summary>
    private void PlayBackgroundMusic(int index)
    {
        if (index < 0 || index >= backgroundMusicClips.Count)
        {
            Debug.LogError($"Invalid background music index: {index}. List count: {backgroundMusicClips.Count}");
            return;
        }

        if (bgMusicSource.isPlaying && bgMusicSource.clip == backgroundMusicClips[index])
        {
            Debug.LogWarning($"Background music index {index} is already playing.");
            return;
        }

        Debug.Log($"Assigning background music: {backgroundMusicClips[index].name}");
        bgMusicSource.Stop(); // Stop any currently playing clip
        bgMusicSource.clip = backgroundMusicClips[index];
        bgMusicSource.loop = true;
        bgMusicSource.Play();
        Debug.Log($"Playing background music: {bgMusicSource.clip.name}");
    }
    /// <summary>
    /// Plays the initial music (menu music by default).
    /// </summary>
    private void PlayInitialMusic()
    {
        if (backgroundMusicClips.Count > 0 && bgMusicSource.clip != backgroundMusicClips[0])
        {
            bgMusicSource.clip = backgroundMusicClips[0];
            bgMusicSource.loop = true;
            bgMusicSource.Play();
        }
    }
    // The rest of the SFX functions can stay the same (no need to change them):
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
    // Play random footsteps sound effect
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
    // Play random button click sound effect
    public void PlayButtonSFX()
    {
        if (buttonClips.Count > 0)
        {
            AudioClip randomClip = buttonClips[Random.Range(0, buttonClips.Count)];
            sfxSource.PlayOneShot(randomClip);
        }
    }

    // Assign PlayButtonSFX method to all buttons in the scene
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
    // Lower the volume of the background music
    public void LowerBgMusicVolume(float newVolume)
    {
        bgMusicSource.volume = Mathf.Clamp(newVolume, 0f, originalBgMusicVolume);
    }
    // Restore the original volume of the background music
    public void RestoreBgMusicVolume()
    {
        bgMusicSource.volume = originalBgMusicVolume;
    }
}