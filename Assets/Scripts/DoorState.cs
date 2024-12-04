using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorState : MonoBehaviour
{
    private Animator _animator;
    private bool _isPlayerNearby = false;
    public GameObject aura;

    public GameObject SubAbrir;
    private Collider objectCollider;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Abrir", false);
        aura.SetActive(true);
        SubAbrir.SetActive(false);
        objectCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (_isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetBool("Abrir", true);
            aura.SetActive(false);
            SubAbrir.SetActive(false);
            Destroy(objectCollider);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            Debug.Log("Jugador cerca de la puerta");
            SubAbrir.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el jugador se aleja, resetea la variable
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            Debug.Log("Jugador se alejó de la puerta");
            SubAbrir.SetActive(false);
        }
    }

}

