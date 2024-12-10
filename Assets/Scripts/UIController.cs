using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// Manages the UI of the play.
/// Author: Pedro Barrios
/// Date: 25/11/2024
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject panelPausa;

    void Start()
    {
        Time.timeScale = 1.0f;
        botonPausa.SetActive(false);
        panelPausa.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ModeCursor();
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        panelPausa.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        panelPausa.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GamePLay");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ModeCursor()
    {
            
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
