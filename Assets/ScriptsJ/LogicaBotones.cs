using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaBotones : MonoBehaviour
{
  public void QuitGame()
  {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #endif 
    }
}
