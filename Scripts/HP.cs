using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public int HitPoints;

    public LayerMask Hitbox; 

    void Start()
    {
        switch(gameObject.tag)
        {
            case "Player":
            HitPoints = 1;
            break;

            case "Enemy":
            HitPoints = 1;
            break;

            case "Vehicle":
            HitPoints = 100;
            break;
        }
    }

    int TakeDamage(int dmgValue) => HitPoints =- dmgValue;
   
    void Update()
    {

        if(HitPoints <= 0)
        {
            gameObject.SetActive(false);
            transform.root.gameObject.SetActive(false);
            
        }
    }

    void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, Quaternion.identity, Hitbox);
        int i = 0;

        while(i < hitColliders.Length)
        {
            Debug.Log("Hit: " + hitColliders[i].name + i);

            hitColliders[i].gameObject.SetActive(false);
            
            i++;
            
            TakeDamage(1);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
       
       Gizmos.DrawWireCube(transform.position, transform.localScale);
            
    }
}
