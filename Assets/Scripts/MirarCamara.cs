using UnityEngine;
/// <summary>
/// Description: Camera controller with mouse movements and cursor selection options.
/// Author: Pedro Barrios A. Optimized by: Ivonne Martinez.
/// Date: 03/12/2024
/// </summary>
public class MirarCamara : MonoBehaviour
{
    public float speed;
    float rotaciónX = 0;

    public Transform Player;
    public Transform ReferenceCam1;
    public Transform ReferenceCam3;

    private float pos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = ReferenceCam1.transform.position;
        pos = 1;
    }


    void Update()
    {
        float MauseX = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        float MauseY = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;

        rotaciónX -= MauseY;
        rotaciónX = Mathf.Clamp(rotaciónX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotaciónX, 0f, 0f);
        Player.Rotate(Vector3.up * MauseX);

        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangePosition();
        }
    }

    private void ChangePosition()
    {
        if (pos == 1)
        {
            transform.position = ReferenceCam3.transform.position;
            pos = 2;
        }
        else
        {
            transform.position = ReferenceCam1.transform.position;
            pos = 1;
        }

    }
}