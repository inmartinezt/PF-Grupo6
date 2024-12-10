using System.Collections;
using UnityEngine;
using TMPro;
/// <summary>
/// Manages the notifications for the first puzzles.
/// Increment films and play the SFX.
/// Author: Pedro Barrios & Optimized by: Ivonne Martinez
/// Date: 09/12/2024
/// </summary>
public class Notification : MonoBehaviour
{
    public GameObject notification;
    public GameObject projectorsfound;
    public int film;
    public TextMeshProUGUI filmsrecogido;
    public GameObject Sub;
    public bool isNear = false;

    private void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            film++;
            isNear = false;
            Sub.SetActive(false);
            StartCoroutine(Notificacion());
            AudioManager.Instance.PlaySFX(4);
            
        }
            
        filmsrecogido.text = film.ToString();  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Film"))
        {
            isNear = true;
            Sub.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Film"))
        {
            isNear = false;
            Sub.SetActive(false);
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