using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeyboardKey : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI letterText;

    [Header(" Events ")]
    public static Action<char> onKeyPressed;


    void Start()
    {
          GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SendKeyPressedEvent()
    {
          onKeyPressed?.Invoke(letterText.text[0]);
    }
}
