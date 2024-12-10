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

    [Header(" Settings ")]
    private int currentWordContainerIndex;
    // Start is called before the first frame update
    void Start()
    {

        Initialize();

        KeyboardKey.onKeyPressed += KeyPressedCallback;
        error.SetActive(false);
        correct.SetActive(false);
        notificacion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize()
    {
         for (int i = 0;  i < wordContainers.Length; i++)
         wordContainers[i].Initialize();

    }

    private void KeyPressedCallback(char letter)
    {

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
}
