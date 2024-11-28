using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorState : MonoBehaviour
{
    private Animator _animator;
    private bool _isPlayerNearby = false;
    private bool _isOpen = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Abrir", false);
    }

    void Update()
    {
        if (_isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetBool("Abrir", true);
            _isOpen = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            Debug.Log("Jugador cerca de la puerta");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el jugador se aleja, resetea la variable
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            Debug.Log("Jugador se alejó de la puerta");
        }
    }
}

