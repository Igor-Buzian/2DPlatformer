using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player1")
        {
            collision.gameObject.transform.SetParent(transform);
        }
        else if (collision.gameObject.name == "Player2")
        {
            collision.gameObject.transform.SetParent(transform);
        }
        else if (collision.gameObject.name == "Player3")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player1")
        {
            collision.gameObject.transform.SetParent(null);
        }
        else if (collision.gameObject.name == "Player2")
        {
            collision.gameObject.transform.SetParent(null);
        }
        else if (collision.gameObject.name == "Player3")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
