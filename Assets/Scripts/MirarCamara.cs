using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarCamara : MonoBehaviour
{

    public float velocidad;
    float rotaci�nX = 0;

    public Transform Player;

    private float pos;

    Vector3 offset1 = new Vector3(0.1f, 1.76f, 0.3f);
    Vector3 offset2 = new Vector3(0.1f, 1.94f, -1.56f);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = Player.transform.position + offset1;
        pos = 1;

    }

    
    void Update()
    {
        float MauseX = Input.GetAxis("Mouse X") * velocidad * Time.deltaTime;
        float MauseY = Input.GetAxis("Mouse Y") * velocidad * Time.deltaTime;

        rotaci�nX -= MauseY;
        rotaci�nX = Mathf.Clamp(rotaci�nX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotaci�nX, 0f, 0f);
        Player.Rotate (Vector3.up * MauseX);

        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangePosition();
        }
    }

    private void ChangePosition()
    {
        if (pos == 1)
        {
            transform.position = Player.transform.position + offset2;
            pos = 2;
        }
        else
        {
            transform.position = Player.transform.position + offset1;
            pos = 1;
        }
        
    }
}
