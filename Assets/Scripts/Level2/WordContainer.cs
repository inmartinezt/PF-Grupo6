using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordContainer : MonoBehaviour
{
    [Header(" Elements ")]
    private LetterContainer[] letterContainers;

    [Header(" Settings ")]
    private int  currentLetterIndex;

    private void Awake()
    {
       letterContainers = GetComponentsInChildren<LetterContainer>();
       //Initialize();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
         for (int i = 0; i< letterContainers.Length; i++)
               letterContainers[i].Initialize();
    }

    public void Add(char letter)
    {
         letterContainers[currentLetterIndex].SetLetter(letter);
         currentLetterIndex++;
    }

    public string GetWord()
    {
         string word = "";

         for (int i = 0; i< letterContainers.Length; i++)
               word += letterContainers[i].GetLetter().ToString();


         return word; 

    }

    public bool IsComplete()
    { 
        return currentLetterIndex >= 7;
    }
}










