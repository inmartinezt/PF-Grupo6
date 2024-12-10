using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Manages the full screen mode on the options menu.
/// Author: Ivonne Martinez, & Optimized by:Pedro Barrios
/// Date: 25/11/2024
/// </summary>
public class LogicaPantallaCompleta : MonoBehaviour
{
    public Toggle fullscreenToggle;
    private const string FullscreenPrefKey = "FullscreenMode";
    private void Start()
    {
        // Load saved fullscreen preference
        bool isFullscreen = PlayerPrefs.GetInt(FullscreenPrefKey, 0) == 1; // Default to fullscreen
        Screen.fullScreen = isFullscreen;

        // Initialize the toggle
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = isFullscreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
        else
        {
            Debug.LogError("Fullscreen Toggle is not assigned in the Inspector.");
        }
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        // Save the preference
        PlayerPrefs.SetInt(FullscreenPrefKey, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log($"Fullscreen mode set to: {isFullscreen} and saved.");
    }
    private void OnDestroy()
    {
        if (fullscreenToggle != null)
        {
            fullscreenToggle.onValueChanged.RemoveListener(SetFullscreen);
        }
    }
}