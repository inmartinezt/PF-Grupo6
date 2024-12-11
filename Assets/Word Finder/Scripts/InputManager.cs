using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private WordContainer[] wordContainers;
    public GameObject error;
    public GameObject correct;
    public GameObject notificacion;

    public GameObject clues;
    public GameObject lost;
    public GameObject instru;

    [Header(" Settings ")]
    private int currentWordContainerIndex;
    // Start is called before the first frame update
    void Start()
    {

        Initialize();

        KeyboardKey.onKeyPressed += KeyPressedCallback;
        instru.SetActive(true);
        error.SetActive(false);
        correct.SetActive(false);
        notificacion.SetActive(false);

        clues.SetActive(false);
        lost.SetActive(false);
    }

    private void Initialize()
    {
         for (int i = 0;  i < wordContainers.Length; i++)
         wordContainers[i].Initialize();

    }

    private void KeyPressedCallback(char letter)
    {
        if (currentWordContainerIndex < 0 || currentWordContainerIndex >= wordContainers.Length)
        {
            Debug.LogWarning("Índice fuera de los límites. Pausando el juego.");
            lost.SetActive(true);
            return; // Sal de la función para evitar errores.
        }
        wordContainers[currentWordContainerIndex].Add(letter);

         if(wordContainers[currentWordContainerIndex].IsComplete())
         {
               CheckWord();
              // currentWordContainerIndex++;
         }

    }

    public void CheckWord()
    {                                      
         string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
         string secretWord = WordManager.instance.GetSecretWord();

         if(wordToCheck == secretWord)
         {
            Debug.Log("Correct word");
            StartCoroutine(Correct());
         }
         else
         {
              Debug.Log("Wrong word");
              currentWordContainerIndex++;
            StartCoroutine(Error());
         }

    }

    IEnumerator Error()
    {
        error.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        error.SetActive(false);
    }

    IEnumerator Correct()
    {
        correct.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        correct.SetActive(false);
        notificacion.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Level3");
    }

    public void Clues()
    {
        clues.SetActive(true);
    }
    public void BackClues()
    {
        clues.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Wordle");
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Continue()
    {
        instru.SetActive(false);
    }
}
