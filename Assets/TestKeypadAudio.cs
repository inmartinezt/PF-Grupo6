using UnityEngine;

public class TestKeypadAudio : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to test
        {
            AudioManager.Instance.PlayKeypadSFX(0); // Play the first sound in keypadClips
        }
    }
}
