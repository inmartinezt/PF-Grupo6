using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages all audio functionalities in the game, including background  and SFX.
/// Implements Singleton for global access and ensures scalability and reusability.
/// Author: Ivonne Martinez
/// Date: 21/11/2024/// 
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
    private void Awake() // Singleton pattern implementation
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
    public void PlayMenuMusic() /// Plays the first background music clip (typically for the menu).
    {
        if (backgroundMusicClips.Count > 0 && bgMusicSource.clip != backgroundMusicClips[0])
        {
            bgMusicSource.clip = backgroundMusicClips[0];
            bgMusicSource.loop = true;
            bgMusicSource.Play();
        }
    }
    public void PlayRandomBgMusic() /// Plays a random background music clip for gameplay (excluding the first clip).
    {
        if (backgroundMusicClips.Count > 1)
        {
            int randomIndex = Random.Range(1, backgroundMusicClips.Count);
            AudioClip randomClip = backgroundMusicClips[randomIndex];
            bgMusicSource.clip = randomClip;
            bgMusicSource.loop = true;
            bgMusicSource.Play();
        }
        else
        {
            Debug.LogWarning("Not enough background music clips for gameplay.");
        }
    }
    public void PlaySFX(int index) /// Plays a specific SFX by index.
    {
        if (index >= 0 && index < sfxClips.Count)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
        else
        {
            Debug.LogWarning("SFX index out of range.");
        }
    }
    public void PlayFootstepsSFX() /// Plays a random sound effect for footsteps.
    {
        PlayRandomSFX(footstepsClips);
    }
    public void PlayButtonSFX() /// Plays a random button click sound effect.
    {
        PlayRandomSFX(buttonClips);
    }
    private void PlayRandomSFX(List<AudioClip> clips) /// Plays a random sound effect from a given list.
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
}