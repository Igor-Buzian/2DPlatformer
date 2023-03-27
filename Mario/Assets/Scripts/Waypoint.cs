using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentwayIndex = 0;


    [SerializeField] private float speed = 2f;
    void Update()
    {
        if (Vector2.Distance(waypoints[currentwayIndex].transform.position, transform.position) < .1f)
        {
            currentwayIndex++; 
            if (currentwayIndex >= waypoints.Length)
            {
                currentwayIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentwayIndex].transform.position, Time.deltaTime*speed);
    }
}
