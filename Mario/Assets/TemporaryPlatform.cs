using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlatform : MonoBehaviour
{
    private GameObject firstPart;
    private GameObject secondPart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).Rotate(1, 0, 0);
        transform.GetChild(1).Rotate(1, 0, 0);
    }
}
