using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    private float speed_x, speed_y;
    public Transform bulletSpawnLocation;

    public float Speed_X
    {
       
        get { return speed_x; }
        set { speed_x = value; }
        
    }

    public float Speed_Y
    {
        get { return speed_y; }
        set { speed_y = value; }
    }

    public void Update()
    {
        transform.position += new Vector3(speed_x, speed_y, 0) * Time.deltaTime;       
    }
}
