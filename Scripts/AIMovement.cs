using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private Vector3 currentTarget;

    public float xRightMax, xLeftMax, speed, delayTime;

    private float delayStart, tolerance;

    public bool automatic;

    void Start()
    {
        currentTarget = FindNewDestination();
        tolerance = speed * Time.deltaTime;
    }

    void Update()
    {
        if(transform.position != currentTarget)
        {
            Movement();

        } else
        {
            GetNextPositon();
        }
    }

    void Movement()
    {
        Vector3 heading = currentTarget - transform.position;

        transform.position += heading / heading.magnitude * speed * Time.deltaTime;

        if (heading.magnitude < tolerance)
        {
            transform.position = currentTarget;
            delayStart = Time.time;
        }
    }

    void GetNextPositon()
    {
       if(automatic)
        {
            if(Time.time - delayStart > delayTime)
            {
                FindNewDestination();
            }
        }
    }

    Vector3 FindNewDestination() => currentTarget = new Vector3(Random.Range(transform.position.x - xLeftMax, transform.position.x + xRightMax),0 ,0);
 
   

}
