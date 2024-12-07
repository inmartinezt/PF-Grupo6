using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject panelPausa;

    private bool isCursorLocked = true;

    void Start()
    {
        Time.timeScale = 1.0f;
        botonPausa.SetActive(true);
        panelPausa.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ModeCursor();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        panelPausa.SetActive(true);
        botonPausa.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        panelPausa.SetActive(false);
        botonPausa.SetActive(true);
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
        isCursorLocked = !isCursorLocked;

        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
}
