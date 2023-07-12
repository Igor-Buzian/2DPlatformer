using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ChooseMenu");
    }
    public void GoToSettingMenu()
    {
        SceneManager.LoadScene("SettingMenu");
    }
    public void GoToMainMenuu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
