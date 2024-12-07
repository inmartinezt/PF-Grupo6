using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    public GameObject notification;
    public GameObject projectorsfound;
    public int film;

    public TextMeshProUGUI filmsrecogido;

    private void Update()
    {
        filmsrecogido.text = film.ToString();  
    }
    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto que colisiona es el objeto
        if (other.CompareTag("Film"))
        {
            Destroy(other.gameObject);
            film++;
            StartCoroutine(Notificacion());
        }
    }
    IEnumerator Notificacion()
    {
        notification.SetActive(true);
        projectorsfound.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        notification.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        projectorsfound.SetActive(false);
    }
}
