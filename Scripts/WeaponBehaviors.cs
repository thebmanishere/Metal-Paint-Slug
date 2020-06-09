using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType
{
    //just need the weapons that appear in the first level

    Handgun = 0,
    HeavyMachineGun = 1,
    Flameshot = 2
};

public enum PlayerDirections
{
    UP = 0,
    RIGHT = 1,
    LEFT = -1
}

public class WeaponBehaviors : MonoBehaviour
{

    float damageValue, ammoCount, grenadeCount, currentWeaponFireSpeed;

    int currentDirection, lastDirection;

    public float currentWeapon, rateOfFire;
   
    public GameObject bulletSpawnPoint, grenadeSpawnPoint, bulletPooler, grenadePooler;

    bool canThrowGrenade;
    
    UnityEngine.Vector3 bulletSpawnLocation_x, bulletSpawnLocation_y, grenadeDirection;

    
   

    void Start()
    {
        currentWeapon = (float)WeaponType.Handgun;

        currentDirection = (int)PlayerDirections.RIGHT;


        lastDirection = currentDirection;

        canThrowGrenade = false;

        bulletSpawnLocation_x = new UnityEngine.Vector3(1.2f, 0, 0);
        bulletSpawnLocation_y = new UnityEngine.Vector3(1, .6f, 0);

        grenadeDirection = new UnityEngine.Vector3(1, 1, 0);


    }
  
    void Fire()
    {

        GameObject obj = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();

        //Debug.Log(obj.name);

        if (obj == null) return;

        switch (currentDirection)
        {
            case (int)PlayerDirections.RIGHT:

                obj.GetComponent<BulletMovement>().Speed_X = currentWeaponFireSpeed;
                obj.GetComponent<BulletMovement>().Speed_Y = 0;

                break;

            case (int)PlayerDirections.LEFT:

                obj.GetComponent<BulletMovement>().Speed_X = -currentWeaponFireSpeed;
                obj.GetComponent<BulletMovement>().Speed_Y = 0;

              
                break;

            case (int)PlayerDirections.UP:

                obj.GetComponent<BulletMovement>().Speed_X = 0;
                obj.GetComponent<BulletMovement>().Speed_Y = currentWeaponFireSpeed;

               
                break;

        }


        obj.transform.position = bulletSpawnPoint.transform.position;
        obj.transform.rotation = bulletSpawnPoint.transform.rotation;

        obj.SetActive(true);


    }

    void Grenade()
    {

        GameObject obj = grenadePooler.GetComponent<ObjectPooler>().GetPooledObject();

        if (obj == null) return;

  
        switch (currentDirection)
        {
            case (int)PlayerDirections.RIGHT:

                obj.GetComponent<GrenadeMovement>().grenadeDir = grenadeDirection;
               
                break;

            case (int)PlayerDirections.LEFT:

                obj.GetComponent<GrenadeMovement>().grenadeDir = -grenadeDirection;
               
               
                break;

        }

        obj.transform.position = grenadeSpawnPoint.transform.position;
        obj.transform.rotation = grenadeSpawnPoint.transform.rotation;

        obj.SetActive(true);

    }

    void PlayerDirection()
    {


        if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") > 0f)
        {

            bulletSpawnPoint.transform.localPosition = bulletSpawnLocation_y;

            currentDirection = (int)PlayerDirections.UP;
        }
        else
        {
            currentDirection = lastDirection;

            switch(currentDirection)
            {
                case (int)PlayerDirections.RIGHT:
                    bulletSpawnPoint.transform.localPosition = bulletSpawnLocation_x;
                    break;

                case (int)PlayerDirections.LEFT:
                    bulletSpawnPoint.transform.localPosition = -bulletSpawnLocation_x;
                    break;
            }
        }


        if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") > 0f && Input.GetAxis("Vertical") <= 0f)
        {


            bulletSpawnPoint.transform.localPosition = bulletSpawnLocation_x;
            grenadeSpawnPoint.transform.localPosition = new UnityEngine.Vector3(0.77f, 0.64f, 0);


            lastDirection = (int)PlayerDirections.RIGHT;
            currentDirection = (int)PlayerDirections.RIGHT;

        }

        if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") < 0f && Input.GetAxis("Vertical") <= 0f)
        {

            bulletSpawnPoint.transform.localPosition = -bulletSpawnLocation_x;
            grenadeSpawnPoint.transform.localPosition = new UnityEngine.Vector3(-0.77f, 0.64f, 0);

            lastDirection = (int)PlayerDirections.LEFT;
            currentDirection = (int)PlayerDirections.LEFT;

        }

    }


    void Update()
    {
        PlayerDirection();

        if (Input.GetButtonDown("Fire1")) Fire();

        if (Input.GetButtonDown("Fire2")) Grenade();

        switch (currentWeapon)
        {
            case (int)WeaponType.Handgun:
                currentWeaponFireSpeed = 100;
                break;
        }
    }

   

}
