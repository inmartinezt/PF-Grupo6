using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject inst1;
    public GameObject inst2;
    public GameObject inst3;

    private void Awake()
    {
        
    }
    void Start()
    {
        inst1.SetActive(false);
        inst2.SetActive(false);
        inst3.SetActive(false);
        StartCoroutine(Instrucciones());
    }

    IEnumerator Instrucciones()
    {
        inst1.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        inst1.SetActive(false);
        inst2.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        inst2.SetActive(false);
        inst3.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        inst3.SetActive(false);
    }
}
