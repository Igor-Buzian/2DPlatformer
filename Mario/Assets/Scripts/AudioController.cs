using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    private void Start()
    {       
        if (!PlayerPrefs.HasKey("Muzic")) audioSource.volume = .2f;           
    }
    private void Update()
    {
            audioSource.volume = PlayerPrefs.GetFloat("Muzic");
    }

}
