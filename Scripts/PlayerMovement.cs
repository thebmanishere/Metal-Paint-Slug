using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController cc;

    public float speed, jumpSpeed, gravity;
    float startingHeight, crouchHeight, crouchSpeed, baseSpeed, crouchingTransitionValue;

    public float movementPauseTimer = 1f;

    bool startTimer = false;
    bool movePlayer = true;
    bool isCrouching = false;


    private Vector3 moveDirection;

    public WeaponBehaviors weaponBehaviors;

    void Start()
    {
        startingHeight = cc.height;
        crouchHeight = cc.height / 2;

        baseSpeed = speed;
        crouchSpeed = baseSpeed * 2;

        crouchingTransitionValue = -100f;

    }

    void PlayerRotation() //use for sprite direction
    {    

        if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") > 0f)
        {
            //Debug.Log("Player is facing right");
            
        }
        else if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") < 0f)
        {
            //Debug.Log("Player is facing left");
            
        }

    }

    bool canCrawl() //use this to determine when the player can move again after firing
    {
      
        if (Input.GetButtonDown("Fire1"))
        {
            startTimer = true;
            movePlayer = false;
        }

        if(startTimer)
        {
            movementPauseTimer -= Time.deltaTime;
        }

        if(movementPauseTimer <= 0)
        {
            movementPauseTimer = 1f;
            startTimer = false;
            movePlayer = true;
        }

        return movePlayer;
        
    }


    void Update()
    {

        PlayerRotation();

        canCrawl();

        if (cc.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), -0.1f, 0);
            moveDirection *= speed;
            

            if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") <= 0f)
            {
                if(!canCrawl())
                {
                    moveDirection = new Vector3(0, crouchingTransitionValue, 0);

                } else
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), crouchingTransitionValue, 0);
                }

               
                cc.height = crouchHeight;
                speed = crouchSpeed;               

            } else {


                cc.height = startingHeight;
                speed = baseSpeed;

            }

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
                cc.height = startingHeight;
                speed = baseSpeed;

            }

            //air control 
        } else if(!cc.isGrounded) {         

            moveDirection.x = Input.GetAxis("Horizontal") * speed; 
        }

        moveDirection.y -= gravity * Time.deltaTime;

        cc.Move(moveDirection * Time.deltaTime);



    }

}
