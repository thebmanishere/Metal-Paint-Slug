using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject enemyPooler;
    public Transform[] spawnPoints;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Spawn();
        }
    }


    void Spawn()
    {
        GameObject obj = enemyPooler.GetComponent<ObjectPooler>().GetPooledObject();

        if (obj == null) return;

        obj.transform.position = spawnPoints[0].position;
        
        obj.SetActive(true);
    }


}
