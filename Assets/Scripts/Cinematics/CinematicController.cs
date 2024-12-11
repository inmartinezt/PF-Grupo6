using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages all Cinematic changes. It shows all images and controls time.
/// Author: Ivonne Martinez
/// Date: 30/11/2024
/// </summary>
public class CinematicController : MonoBehaviour
{
    public List<Image> cinematicImages; // Lista de imágenes en el canvas
    public float[] imageDisplayTimes;   // Tiempos para mostrar cada imagen
    public int audioIndex = 2; // Índice del SFX en el AudioManager
    private int currentImageIndex = 0;
    private void Start()
    {
        StartCinematic();
    }
    public void StartCinematic()
    {
        // Asegúrate de que hay suficientes imágenes y tiempos
        if (cinematicImages.Count != imageDisplayTimes.Length)
        {
            // Debug.LogError("El número de imágenes no coincide con el número de tiempos.");
            return;
        }
        // Reproducir el audio desde el AudioManager
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(audioIndex); // Reproducir el SFX con el índice que indicas
        }
        else
        {
            Debug.LogError("AudioManager no está asignado correctamente.");
            return;
        }
        // Mostrar la primera imagen
        ShowImageAndPlayAudio(currentImageIndex);
    }
    private void ShowImageAndPlayAudio(int index)
    {
        if (index < cinematicImages.Count)
        {
            // Mostrar la imagen en el Canvas
            cinematicImages[index].gameObject.SetActive(true);
            // Log de depuración
            //  Debug.Log($"Mostrando imagen {index} durante {imageDisplayTimes[index]} segundos.");
            // Desactivar la imagen después del tiempo asignado
            StartCoroutine(WaitAndHideImage(index, imageDisplayTimes[index]));
        }
    }
    private IEnumerator WaitAndHideImage(int index, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cinematicImages[index].gameObject.SetActive(false);
        currentImageIndex++;
        if (currentImageIndex < cinematicImages.Count)
        {
            ShowImageAndPlayAudio(currentImageIndex);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}