using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;

public class Stars : MonoBehaviour
{
    public GameObject[] hiddenElements;
    public Button[] lvls;
    public Sprite star, NoStar;

    public int level = 1;

    public void Start()
    {
        for (int i = 1; i < lvls.Length+1; i++)
        {
            if (PlayerPrefs.HasKey("stars" + i))
            {
                if (PlayerPrefs.GetInt("stars" + i) == 1)
                {
                    lvls[i - 1].transform.GetChild(0).GetComponent<Image>().sprite = star;
                    lvls[i - 1].transform.GetChild(1).GetComponent<Image>().sprite = NoStar;
                    lvls[i - 1].transform.GetChild(2).GetComponent<Image>().sprite = NoStar;
                }
                else if (PlayerPrefs.GetInt("stars" + i) == 2)
                {
                    lvls[i - 1].transform.GetChild(0).GetComponent<Image>().sprite = star;
                    lvls[i - 1].transform.GetChild(1).GetComponent<Image>().sprite = star;
                    lvls[i - 1].transform.GetChild(2).GetComponent<Image>().sprite = NoStar;
                }
                else if(PlayerPrefs.GetInt("stars" + i) == 3)
                {
                    lvls[i - 1].transform.GetChild(0).GetComponent<Image>().sprite = star;
                    lvls[i - 1].transform.GetChild(1).GetComponent<Image>().sprite = star;
                    lvls[i - 1].transform.GetChild(2).GetComponent<Image>().sprite = star;
                }
            }
            else 
            {
                lvls[i - 1].transform.GetChild(0).gameObject.SetActive(false);
                lvls[i - 1].transform.GetChild(1).gameObject.SetActive(false); 
                lvls[i - 1].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    private void Update()
    {
        //Debug.Log($"star {level}");
        for (int i = 0; i < level; i++)
        {
            hiddenElements[i].SetActive(false);
        }
        level = PlayerPrefs.GetInt("level");
    }
    public void goToTheFirstLvl()
    {
        SceneManager.LoadScene(1);
    }
    public void goToTheSecondLvl()
    {
        SceneManager.LoadScene(2);
    }
    public void goToTheThirdLvl()
    {
        SceneManager.LoadScene(3);
    }
}
