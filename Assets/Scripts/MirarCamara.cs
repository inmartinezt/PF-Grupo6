using UnityEngine;
/// <summary>
/// Description: Camera controller with mouse movements and cursor selection options.
/// Author: Pedro Barrios A. Optimized by: Ivonne Martinez.
/// Date: 03/12/2024
/// </summary>
public class MirarCamara : MonoBehaviour
{
    public float speed = 5f;
    private float rotationX = 0;
    public Transform Player;
    public CameraPositionController cameraPositionController;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = cameraPositionController.GetCurrentCameraPosition();
    }
    void Update()
    {
        float MauseX = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        float MauseY = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
        rotationX -= MauseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        Player.Rotate(Vector3.up * MauseX);
        if (Input.GetKeyDown(KeyCode.K))
        {
            cameraPositionController.ToggleCameraPosition();  // Cambiar posición usando el controlador
        }
    }
}
public class CameraPositionController : MonoBehaviour
{
    public Transform ReferenceCam1;
    public Transform ReferenceCam3;
    private int pos = 1;  // Usaremos un entero para rastrear la posición actual
    public Vector3 GetCurrentCameraPosition() =>
        pos == 1 ? ReferenceCam1.position : ReferenceCam3.position;
    public void ToggleCameraPosition()
    {
        pos = pos == 1 ? 2 : 1;  // Alterna entre 1 y 2.
    }
}