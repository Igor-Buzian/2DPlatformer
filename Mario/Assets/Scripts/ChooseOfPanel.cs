using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseOfPanel : MonoBehaviour
{
    [SerializeField] public GameObject[] Panel;
    private int NumberOfPanel;
    public void Next()
    {
        NumberOfPanel++;
            if (NumberOfPanel == Panel.Length)
            {
                NumberOfPanel = 0;
            }
       PlayerPrefs.SetInt("NumberOfPanel", NumberOfPanel);

        switch (NumberOfPanel)
        {
            case 0:
                Panel[0].SetActive(true);
                Panel[1].SetActive(false);
                break;

            case 1:
                Panel[0].SetActive(false);
                Panel[1].SetActive(true);
                break;
        }
    }
    public void Back()
    {
            NumberOfPanel--;
            if (NumberOfPanel < 0)
            {
            NumberOfPanel = Panel.Length - 1;
            }
            PlayerPrefs.SetInt("NumberOfPanel", NumberOfPanel);
        switch (NumberOfPanel)
        {
            case 0:
                Panel[0].SetActive(true);
                Panel[1].SetActive(false);
                break;

            case 1:
                Panel[0].SetActive(false);
                Panel[1].SetActive(true);
                break;
        }
    }
    public void Close()
    {
        SceneManager.LoadScene("StartScene");
    }
    private void Start()
    {
        Debug.Log($"NumberOfPanel: {NumberOfPanel}");
    }
}
