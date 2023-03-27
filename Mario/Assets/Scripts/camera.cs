using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
   //[SerializeField] private Transform player;
   [SerializeField] private GameObject[] character;
    private int playerSprite;
    // Update is called once per frame
    private void Update()
    {
        // transform.position = new Vector3 (player.position.x, player.position.y, transform.position.z);
        playerSprite = PlayerPrefs.GetInt("selectedSkin");
        if (playerSprite == 0)
        {
            transform.position = new Vector3(character[0].transform.position.x, character[0].transform.position.y, transform.position.z);
        }
        else if (playerSprite == 1)
        {
            transform.position = new Vector3(character[1].transform.position.x, character[1].transform.position.y, transform.position.z);
        }
        else if (playerSprite == 2)
        {
            transform.position = new Vector3(character[2].transform.position.x, character[2].transform.position.y, transform.position.z);
        }

    }

}

