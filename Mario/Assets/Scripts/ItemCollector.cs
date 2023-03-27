using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public int Bananas = 0;
    [SerializeField] private Text BananasScare;
    [SerializeField] private AudioSource BananasSoundEffect;
    private void Start()
    {

        BananasSoundEffect.volume = PlayerPrefs.GetFloat("SoundOfCharacter");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Banana"))
        {
            BananasSoundEffect.Play();
            Destroy(collision.gameObject);
            Bananas++;
            PlayerPrefs.SetInt("coin" + SceneManager.GetActiveScene(), Bananas);
            BananasScare.text = $"Bananas: {Bananas}";
        }
    }
}
