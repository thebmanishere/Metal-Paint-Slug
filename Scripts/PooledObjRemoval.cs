using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjRemoval : MonoBehaviour
{

    public float RemovalTimer;

    void OnEnable()
    {
        Invoke("Destroy", RemovalTimer);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
