using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private WordContainer[] wordContainers;


    [Header(" Settings ")]
    private int currentWordContainerIndex;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        KeyboardKey.onKeyPressed += KeyPressedCallback;
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
            Debug.Log("Correct word");
         else
         {
              Debug.Log("Wrong word");
              currentWordContainerIndex++;
         }

    }
}
