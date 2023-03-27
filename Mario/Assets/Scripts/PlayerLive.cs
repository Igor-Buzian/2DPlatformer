using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLive : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource deathSoundEffect;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        deathSoundEffect = GetComponent<AudioSource>();
        deathSoundEffect.volume = PlayerPrefs.GetFloat("SoundOfCharacter");
        //не работает изменение звука смерти 
    }
    private void Update()
    {
        if (transform.position.y < -5f)
        {
            Die();
            RestartLevel();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();            
        }     
    }
    private void Die()
    {
        deathSoundEffect.Play();
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
