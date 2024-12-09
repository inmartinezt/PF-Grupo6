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
    [Header("Keypad SFX")]
    [Tooltip("List of keypad sound effects (Indexed)")]
    public List<AudioClip> keypadClips;

    private float originalBgMusicVolume; // To store the original volume

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        originalBgMusicVolume = bgMusicSource.volume;

        // Start the appropriate music based on the current scene
        UpdateBackgroundMusic(SceneManager.GetActiveScene().name);
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
        UpdateBackgroundMusic(scene.name);
    }

    /// <summary>
    /// Updates the background music based on the current scene.
    /// </summary>
    private void UpdateBackgroundMusic(string sceneName)
    {
        Debug.Log($"Scene loaded: {sceneName}. Updating background music...");
        int musicIndex = GetMusicIndexForScene(sceneName);

        if (musicIndex != -1)
        {
            PlayBackgroundMusic(musicIndex);
        }
        else
        {
            Debug.LogWarning("No background music is assigned to this scene.");
            bgMusicSource.Stop();
        }
    }

    /// <summary>
    /// Determines which music index to use based on the scene name.
    /// </summary>
    private int GetMusicIndexForScene(string sceneName)
    {
        if (sceneName == "Cinematic1" || sceneName == "MainMenu" || sceneName == "Cinematic2")
        {
            return 0; // Index 0 for cinematic and main menu
        }
        else if (sceneName == "GamePlay" || sceneName == "Level3")
        {
            return 1; // Index 1 for gameplay levels
        }

        return -1; // No music assigned for this scene
    }

    /// <summary>
    /// Plays a background music clip based on the index.
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
            Debug.Log($"Music index {index} is already playing.");
            return;
        }

        Debug.Log($"Switching to music: {backgroundMusicClips[index].name}");
        bgMusicSource.Stop();
        bgMusicSource.clip = backgroundMusicClips[index];
        bgMusicSource.loop = true;
        bgMusicSource.Play();
    }

    // --- Additional Functions (Original AudioManager Functionality) ---
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

    public void PlayButtonSFX()
    {
        if (buttonClips.Count > 0)
        {
            AudioClip randomClip = buttonClips[Random.Range(0, buttonClips.Count)];
            sfxSource.PlayOneShot(randomClip);
        }
    }

    public void AssignButtonSounds()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayButtonSFX());
        }
    }

    public void PlayKeypadSFX(int index)
    {
        if (keypadClips.Count > 0 && index >= 0 && index < keypadClips.Count)
        {
            sfxSource.PlayOneShot(keypadClips[index]);
        }
        else
        {
            Debug.LogWarning("No Keypad SFX available or index out of range.");
        }
    }

    public void LowerBgMusicVolume(float newVolume)
    {
        bgMusicSource.volume = Mathf.Clamp(newVolume, 0f, originalBgMusicVolume);
    }

    public void RestoreBgMusicVolume()
    {
        bgMusicSource.volume = originalBgMusicVolume;
    }
}
