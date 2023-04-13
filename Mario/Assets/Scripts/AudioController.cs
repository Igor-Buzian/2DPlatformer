using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    private float audioOfValue;
    private void Start()
    {       
        if (!PlayerPrefs.HasKey("Muzic")) audioSource.volume = .2f;
        else audioSource.volume = PlayerPrefs.GetFloat("Muzic");
        audioOfValue = audioSource.volume;
    }
    private void Update()
    {
        if(audioOfValue != PlayerPrefs.GetFloat("Muzic")) { 
            audioSource.volume = PlayerPrefs.GetFloat("Muzic");
            audioOfValue = audioSource.volume;
        }           
    }
}
