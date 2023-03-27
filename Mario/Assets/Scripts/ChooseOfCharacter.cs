using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChooseOfCharacter : MonoBehaviour
{ 
    [SerializeField]private GameObject[] character;

    private int PlayerNum;
    void Start()
    {
        PlayerNum = PlayerPrefs.GetInt("selectedSkin");
        if(PlayerNum == 0)
        {
            character[0].transform.gameObject.SetActive(true);
            character[1].transform.gameObject.SetActive(false);
            character[2].transform.gameObject.SetActive(false);
        }
        else if (PlayerNum == 1)
        {
            character[0].transform.gameObject.SetActive(false);
            character[1].transform.gameObject.SetActive(true);
            character[2].transform.gameObject.SetActive(false);
        }
        else if (PlayerNum == 2)
        {
            character[0].transform.gameObject.SetActive(false);
            character[1].transform.gameObject.SetActive(false);
            character[2].transform.gameObject.SetActive(true);
        }
    }
}
