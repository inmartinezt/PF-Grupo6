using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogicaCalidad : MonoBehaviour
{
    public TMP_Dropdown dropDown;
    public int calidad;

    void Start()
    {
        calidad = PlayerPrefs.GetInt("numeroDeCalidad", 3);
        dropDown.value = calidad;
        AjustarCalidad();
    }

    void Update()
    {
        
    }

    public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropDown.value);
        PlayerPrefs.SetInt("numeroDeCalidad", dropDown.value);
        calidad = dropDown.value;
    }
}
