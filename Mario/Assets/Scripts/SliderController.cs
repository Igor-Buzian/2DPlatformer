using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider muzicSlider;
    public Slider CharacterVolSlider;

    public float characterVolume;
    public float musicVolume;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Muzic")) muzicSlider.value = .2f;
        else muzicSlider.value = PlayerPrefs.GetFloat("Muzic");

        if (!PlayerPrefs.HasKey("SoundOfCharacter")) CharacterVolSlider.value = .2f;
        else CharacterVolSlider.value = PlayerPrefs.GetFloat("Muzic");

        musicVolume = muzicSlider.value;
        characterVolume = CharacterVolSlider.value;
    }
    private void Update()
    {
        if(musicVolume != muzicSlider.value)
        {
            PlayerPrefs.SetFloat("Muzic", muzicSlider.value);
            PlayerPrefs.Save();
            musicVolume = muzicSlider.value;
        }
        if(characterVolume != CharacterVolSlider.value)
        {
            PlayerPrefs.SetFloat("SoundOfCharacter", characterVolume);
            PlayerPrefs.Save();
            characterVolume = CharacterVolSlider.value;
        }
    }
}
