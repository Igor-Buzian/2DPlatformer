using UnityEngine;
using UnityEngine.SceneManagement;


public class Finish : MonoBehaviour
{
    private AudioSource finishSoundEffect;
    private Animator anim;
    [SerializeField]private GameObject BananasScare;
    [SerializeField] private GameObject fade;

    private float SecondsDelay = 1f;
    int allCoins;
    int level;
    private void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
        anim = fade.GetComponent<Animator>();
        allCoins = GameObject.FindGameObjectsWithTag("Banana").Length;
        finishSoundEffect.volume = PlayerPrefs.GetFloat("SoundOfCharacter");
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            level = 1;
            PlayerPrefs.SetInt("level", level);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player1")
        {        
            finishSoundEffect.Play();
            Star();
            anim.SetTrigger("Fade");
            Invoke("CompleteLevel", SecondsDelay);
        }
        else if (collision.gameObject.name == "Player2")
        {
            finishSoundEffect.Play();
            Star();
            anim.SetTrigger("Fade");
            Invoke("CompleteLevel", SecondsDelay);
        }
        else if (collision.gameObject.name == "Player3")
        {
            finishSoundEffect.Play();
            Star();
            anim.SetTrigger("Fade");
            Invoke("CompleteLevel", SecondsDelay);
        }
    }

    private void Star()
    {
        if (((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) <= 0.33f && !PlayerPrefs.HasKey("stars" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 1);
        }
        else if (((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) > 0.33f && ((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) <= 0.99f && (!PlayerPrefs.HasKey("stars" + SceneManager.GetActiveScene().buildIndex) || PlayerPrefs.GetInt("stars" + SceneManager.GetActiveScene().buildIndex) < 2))
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 2);
        }
        else if (((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) > 0.99f && (!PlayerPrefs.HasKey("stars" + SceneManager.GetActiveScene().buildIndex) || PlayerPrefs.GetInt("stars" + SceneManager.GetActiveScene().buildIndex) < 3))
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
}
