using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorState : MonoBehaviour
{
    private Animator _animator;
    private bool _isPlayerNearby = false;
    private bool _isOpen = false;
    public GameObject aura;

    public GameObject SubAbrir;

    private bool subs = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Abrir", false);
        aura.SetActive(true);
        SubAbrir.SetActive(false);
    }

    void Update()
    {
        if (_isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetBool("Abrir", true);
            _isOpen = true;
            aura.SetActive(false);
            subs = false;
        }
        if (subs == false)
        {
            SubAbrir.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            Debug.Log("Jugador cerca de la puerta");
            if (subs == true)
            {
                SubAbrir.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el jugador se aleja, resetea la variable
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            Debug.Log("Jugador se alejó de la puerta");
            if (subs == true)
            {
                SubAbrir.SetActive(false);
            }
            
        }
    }
}

