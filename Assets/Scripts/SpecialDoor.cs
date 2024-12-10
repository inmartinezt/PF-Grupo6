using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// Manages the states of the diferrent doors.
/// Author: Pedro Barrios
/// Date: 25/11/2024
public class SpecialDoor : MonoBehaviour
{
    private Animator _animator;
    private bool _isPlayerNearby = false;

    public GameObject aura1;
    public GameObject aura2;
    public GameObject SubAbrir;
    public GameObject mission1;

    private Collider objectCollider;

    public Notification notificacion;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Abrir", false);
        aura1.SetActive(true);
        aura2.SetActive(false);
        SubAbrir.SetActive(false);
        objectCollider = GetComponent<Collider>();
        mission1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (notificacion.film != 5)
        {
            aura1.SetActive(true);
            aura2.SetActive(false);
        }
        if (notificacion.film == 5)
        {
            aura2.SetActive(true);
            aura1.SetActive(false);
        }
        if (_isPlayerNearby && Input.GetKeyDown(KeyCode.E) && notificacion.film == 5)
        {
            _animator.SetBool("Abrir", true);
            SubAbrir.SetActive(false);
            Destroy(objectCollider);
            aura2.SetActive(false);
            aura1.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto que colisiona es el jugador
        if (other.CompareTag("Player") && notificacion.film == 5)
        {
            _isPlayerNearby = true;
            SubAbrir.SetActive(true);
        }

        if (other.CompareTag("Player") && notificacion.film != 5)
        {
            mission1.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el jugador se aleja, resetea la variable
        if (other.CompareTag("Player") && notificacion.film == 5)
        {
            _isPlayerNearby = false;
            SubAbrir.SetActive(false);
        }

        if (other.CompareTag("Player") && notificacion.film != 5)
        {
            mission1.SetActive(false);
        }
    }
}
