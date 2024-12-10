using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
/// <summary>
/// Manages the interaction with the 3 letters into the Level 3 and those appears on Canvas.
/// Author: Ivonne Martinez & Optimized by:Pedro Barrios
/// Date: 21/11/2024
/// </summary>
public class ObjectInteraction : MonoBehaviour
{
    public Image note1Image; // Image for Note1
    public Image note2Image; // Image for Note2
    public Image morseImage; // Image for Morse

    public GameObject Sub;
    public GameObject Rec;

    private bool isNear = false; // If player is near the object
    private string currentObjectTag = ""; // To track which object we are interacting with

    private void Start()
    {
        Sub.SetActive(false);
        Rec.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            isNear = true;
            Sub.SetActive(true);
            Debug.Log("Player is near: " + gameObject.name);
            currentObjectTag = gameObject.tag; // Set the current object tag
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            Sub.SetActive(false);
            isNear = false;
            Debug.Log("Player left: " + gameObject.name);
            currentObjectTag = ""; // Reset tag when player leaves
        }
    }

    private void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E)) // If the player is near and presses "E"
        {
            // Toggle the Canvas images based on the tag of the object
            switch (currentObjectTag)
            {
                case "Note1":
                    note1Image.gameObject.SetActive(!note1Image.gameObject.activeSelf); // Toggle visibility
                    break;
                case "Note2":
                    note2Image.gameObject.SetActive(!note2Image.gameObject.activeSelf); // Toggle visibility
                    break;
                case "Morse":
                    morseImage.gameObject.SetActive(!morseImage.gameObject.activeSelf); // Toggle visibility
                    break;
            }

            Sub.SetActive(false);
        }
    }

}