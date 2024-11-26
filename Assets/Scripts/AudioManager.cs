using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Singleton instance
    [Header("Music")]
    public AudioSource bgMusicSource; // Drag Background Music's AudioSource
    public List<AudioClip> backgroundMusicClips; // List of background music clips for random play
    [Header("SFX")]
    public AudioSource sfxSource; // Drag SFX's AudioSource
    public List<AudioClip> sfxClips; // List of win SFX clips
    [Header("SFX Footsteps")]
    public List<AudioClip> footstepsClips;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayRandomBgMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Play random background music from the list
    public void PlayRandomBgMusic()
    {
        if (bgMusicSource.isPlaying) return;

        if (backgroundMusicClips.Count > 0)
        {
            AudioClip randomClip = backgroundMusicClips[Random.Range(0, backgroundMusicClips.Count)];
            bgMusicSource.clip = randomClip;
            bgMusicSource.loop = true;
            bgMusicSource.Play();
        }
    }
    // Play random SFX clip for a win
    public void FootstepsSFX()
    {
        PlayRandomSFX(footstepsClips);
    }
    // Play random SFX clip for a loss
    private void PlayRandomSFX(List<AudioClip> sfxClips)
    {
        if (sfxClips.Count > 0)
        {
            AudioClip randomClip = sfxClips[Random.Range(0, sfxClips.Count)];
            sfxSource.PlayOneShot(randomClip);
        }
    }
    // Play specific
}