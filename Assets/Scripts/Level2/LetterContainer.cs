using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterContainer : MonoBehaviour
{
    // Start is called before the first frame update
    [Header(" Elements ")]
    [SerializeField] private TextMeshPro letter;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
         letter.text = "";
    }

    public void SetLetter(char letter)
    {
         this.letter.text = letter.ToString();
    }

    public char GetLetter()
    {
        return letter.text[0];
    }
}
