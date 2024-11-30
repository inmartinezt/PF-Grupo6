using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efectos : MonoBehaviour
{
    private AudioSource efectos;
    public AudioClip clickAudio;
    public AudioClip switchClick;

    // Start is called before the first frame update
    void Start()
    {
        efectos = GetComponent<AudioSource>();
    } 

    public void ClickAudioOn()
    {
        efectos.PlayOneShot(clickAudio);
    }

    public void SwitchClickOn()
    {
        efectos.PlayOneShot(switchClick);
    }

}
