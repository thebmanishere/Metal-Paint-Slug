using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEditor;
using UnityEngine;

public class GrenadeMovement : MonoBehaviour
{
 

    public float grenadeSpeed;

    public Transform grenadeSpawnLocation;

    public Vector3 grenadeDir = new Vector3(1, 1, 0);

  
    public void FixedUpdate()
    {       

        transform.position += grenadeDir * grenadeSpeed * Time.deltaTime;
       
    }

}
