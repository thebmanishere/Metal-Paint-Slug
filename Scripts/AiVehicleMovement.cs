using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;

public class AiVehicleMovement : MonoBehaviour
{

    public bool isHelicopter, isTank, automatic,pauseTankMovement;

    public Transform target;

    public Vector3 helicopterTrackingOffset, tankTrackingOffset;

    private UnityEngine.Vector3 velocity, right, wrld, currentTarget;

    public float smoothSpeed, tankPosY, tankSpeed, delayTime;

    public float offsetValue;

    private float delayStart, tolerance, maxBounds, minBounds, tankStoppingDistance;


    bool isMovingLeft;

    void Start()
    {
        velocity = Vector3.zero;
        right = Vector3.right;

        wrld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));

        tolerance = tankSpeed * Time.deltaTime;

        maxBounds = wrld.x - 5;

        minBounds = wrld.x - 32;

        currentTarget = new Vector3(minBounds, tankPosY, 0);

        isMovingLeft = true;
     

    }

 
    void Update()
    {

        if (isHelicopter)
        {
            UnityEngine.Vector3 desiredPosition = target.position + helicopterTrackingOffset;
            UnityEngine.Vector3 smoothedPosition = UnityEngine.Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;
        }

        if(isTank)
        {

            switch(isMovingLeft)
            {
                case true:
                    tankStoppingDistance = target.position.x + offsetValue;
                    break;

                case false:
                    tankStoppingDistance = target.position.x - offsetValue;
                    break;

            }

            

            if (transform.position != currentTarget)
            {

                if(!pauseTankMovement)
                {
                    Vector3 heading = currentTarget - transform.position;

                    transform.position += heading / heading.magnitude * tankSpeed * Time.deltaTime;


                    if (heading.magnitude < tolerance)
                    {
                        transform.position = currentTarget;
                        delayStart = Time.time;
                    }
                }


                if (transform.position.x <= tankStoppingDistance)
                {
                    StartCoroutine("pauseMovement");
                } 

            }
            else
            {


                if (automatic)
                {
                    if (Time.time - delayStart > delayTime)
                    {                       
                        SwitchDirections();
                    }
                }

            }



        }

 

    }

    IEnumerator pauseMovement()
    {
        pauseTankMovement = true;
        yield return new WaitForSeconds(2f);
        pauseTankMovement = false;
    }

    void SwitchDirections()
    {

        if (transform.position.x + 5f >= wrld.x)
        {
            //Debug.Log(gameObject.name + "is at the bounds of camera (right side)");
            currentTarget = new Vector3(minBounds, tankPosY, 0);
        }

        if (transform.position.x - 3f <= wrld.x - 35)
        {
            //Debug.Log(gameObject.name + "is at the bounds of camera (left side)");

            currentTarget = new Vector3(maxBounds, tankPosY, 0);
        }

        isMovingLeft = !isMovingLeft;

    }
   

}




/*
 
             UnityEngine.Vector3 desiredPosition = target.position + tankTrackingOffset;
            UnityEngine.Vector3 smoothedPosition = UnityEngine.Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, tankPosY, 0);
     
     
     */
