using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    private bool _isPlayerNearby = false;

    public GameObject aura1;
    public GameObject mission2;

    void Start()
    {
        aura1.SetActive(true);
        mission2.SetActive(false);
    }

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            mission2.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Cuando el jugador se aleja, resetea la variable
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            mission2.SetActive(false);
        }
    }
}
