using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    private AudioSource finishSoundEffect;
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField]private GameObject BananasScare;
    [SerializeField] private GameObject fade;
 private GameObject Player;

    private float SecondsDelay = 1f;
    private bool NextLevel;
    int allCoins;
    int level;
    private void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
        anim = fade.GetComponent<Animator>();
        allCoins = GameObject.FindGameObjectsWithTag("Banana").Length;
        level = PlayerPrefs.GetInt("level");
        finishSoundEffect.volume = PlayerPrefs.GetFloat("SoundOfCharacter");
        /*        if (GameObject.Find("Player1").SetActive(true) || GameObject.Find("Player2").SetActive(false);)
                {
                    GameObject.Find("Player1").SetActive(true);
                    rb = GameObject.Find("Player1").GetComponent<Rigidbody2D>();
                }
                else if(GameObject.Find("Player2").GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
                {
                    rb = GameObject.Find("Player2").GetComponent<Rigidbody2D>();
                }
                else if(GameObject.Find("Player3").GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
                { 
                    rb = GameObject.Find("Player3").GetComponent<Rigidbody2D>(); 
                }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player1")
        {        
            finishSoundEffect.Play();
            Star();
            Invoke("StaticBody", 0.5f);
            anim.SetTrigger("Fade");
            Invoke("CompleteLevel", SecondsDelay);
        }
        else if (collision.gameObject.name == "Player2")
        {
            finishSoundEffect.Play();
            Star();
            Invoke("StaticBody", 0.5f);
            anim.SetTrigger("Fade");
            Invoke("CompleteLevel", SecondsDelay);
        }
        else if (collision.gameObject.name == "Player3")
        {
            finishSoundEffect.Play();
            Star();
            Invoke("StaticBody", 0.5f);
            anim.SetTrigger("Fade");
            Invoke("CompleteLevel", SecondsDelay);
        }
    }

    private void Star()
    {
        if (((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) <= 0.33f)
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 1);
        }
        else if (((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) > 0.33f && ((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) <= 0.99f)
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 2);
        }
        else
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 3);
        }
    }
    private void CompleteLevel()
    {
        {
            if (SceneManager.GetActiveScene().buildIndex == level)
            {
                level = SceneManager.GetActiveScene().buildIndex+1;
                PlayerPrefs.SetInt("level", level);
                SceneManager.LoadScene("chooseMenu");
            }
            else
            {
                /*level = 1;
                PlayerPrefs.SetInt("level", level); Для того что бы был открыт только 1 уровень*/
                SceneManager.LoadScene("chooseMenu");
            }
        }
    }
/*    private void StaticBody()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }*/
}
