using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Manages all Keypad states and sounds remitted to AudioManager
/// Author: Ivonne Martinez
/// Date: 28/11/2024
/// </summary>
public class KeypadHandler : MonoBehaviour
{
    [Header("Key Mappings")]
    [Tooltip("Buttons for numeric keys (0-9). Index 0 = Button 0, Index 1 = Button 1, etc.")]
    public Button[] numericButtons; // Assign 10 buttons for 0-9
    [Tooltip("Button for the Z key.")]
    public Button zButton;
    [Tooltip("Button for the Q key.")]
    public Button qButton;
    [Header("Sound Settings")]
    [Tooltip("AudioManager for playing sounds.")]
    private AudioManager audioManager;
    private void Start()
    {
        // Attempt to get the AudioManager instance
        audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance is not found. Ensure it exists and is marked DontDestroyOnLoad.");
        }
    }

    private void Update()
    {
        HandleNumericKeys();
        HandleCustomKeys();
    }
    private void HandleNumericKeys()
    {
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) ||
                (i == 0 && Input.GetKeyDown(KeyCode.Keypad0)) ||
                (i > 0 && Input.GetKeyDown(KeyCode.Keypad1 + i - 1)))
            {
                HighlightButton(numericButtons[i]);
                PlayKeypadSound(i);
            }
        }
    }
    private void HandleCustomKeys()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            HighlightButton(zButton);
            PlayKeypadSound(10); // Z's sound index
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            HighlightButton(qButton);
            PlayKeypadSound(11); // Q's sound index
        }
    }
    private void HighlightButton(Button button)
    {
        if (button != null)
        {
            var colors = button.colors;
            colors.normalColor = Color.yellow;
            button.colors = colors;

            StartCoroutine(ResetButtonHighlight(button, 0.2f));
        }
    }
    private System.Collections.IEnumerator ResetButtonHighlight(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (button != null)
        {
            var colors = button.colors;
            colors.normalColor = Color.white;
            button.colors = colors;
        }
    }
    private void PlayKeypadSound(int index)
    {
        if (audioManager != null)
        {
            audioManager.PlayKeypadSFX(index);
        }
        else
        {
            Debug.LogWarning("AudioManager instance is not available. Sound will not be played.");
        }
    }
}