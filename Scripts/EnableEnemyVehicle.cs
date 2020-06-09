using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnemyVehicle : MonoBehaviour
{
    public GameObject Vechicle;
    public CameraFollow cameraFollow;
    public GameObject progressWall;


    void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "Player")
        {
            Vechicle.SetActive(true);
            cameraFollow.enabled = false;
        }
    }
}
   