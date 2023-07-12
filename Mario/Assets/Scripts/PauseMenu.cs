using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    private Rigidbody2D rb;
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    private int playerSprite;

    private void Start()
    {
        
        playerSprite = PlayerPrefs.GetInt("selectedSkin");
        if (playerSprite == 0)
        {
            rb = players[0].GetComponent<Rigidbody2D>();
        }
        else if (playerSprite == 1)
        {
            rb = players[1].GetComponent<Rigidbody2D>();
        }
        else if (playerSprite == 2)
        {
            rb = players[2].GetComponent<Rigidbody2D>();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            {
                if (GameIsPaused)
                {
                    Resume();

                }
                else
                {                   
                    Pause();
                }
            }         
        }  
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        rb.bodyType = RigidbodyType2D.Static;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("StartScene");
        Resume();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
   
}
