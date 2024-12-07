using UnityEngine;
/// <summary>
/// Manages radio SFX connected with AudioManager.
/// Implements Singleton for global access and ensures scalability and reusability.
/// Author: Ivonne Martinez
/// Date: 23/11/2024
/// </summary>
public class Playsound : MonoBehaviour
{
    // Index for the keypad sound in the AudioManager's keypadClips list
    public int keypadSFXIndex;
    // Reference to AudioManager to play the correct keypad sound
    private AudioManager _audioManager;
    // Start is called before the first frame update
    private void Start()
    {
        // Get the AudioManager instance (Singleton)
        _audioManager = AudioManager.Instance;
    }
    // Method to be called when a keypad button is pressed
    public void Clicky()
    {
        // Check if the AudioManager is available and play the keypad sound by index
        _audioManager?.PlayKeypadSFX(keypadSFXIndex);
    }
}