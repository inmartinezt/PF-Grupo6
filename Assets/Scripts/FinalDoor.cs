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

    private void Update()
    {
        if (_isPlayerNearby)
        {
            // Aquí puedes añadir alguna lógica para lo que pasa cuando el jugador está cerca
            Debug.Log("Jugador cerca de la puerta.");
        }
    }

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