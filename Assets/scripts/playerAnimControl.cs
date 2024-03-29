﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimControl : MonoBehaviour
{
    // Start is called before the first frame update
    private float playerVelocity;
    private Rigidbody2D playerrb2d;
    private Animator playerAnimator;
    private string currentAnimstate;
    private PlayerController playercntrl;
    private Jump playerJmp;
    public Joystick jstk;
    const string PLAYER_IDLE = "rambro_idle";
    const string PLAYER_RUN = "rambro_run";
    const string PLAYER_JUMP_RISE = "rambro_jump_rise";
    const string PLAYER_JUMP_FALL = "rambro_jump_fall";
    const string PLAYER_FALL = "player_fall";
    void Start()
    {
        playerrb2d = transform.parent.GetComponent<Rigidbody2D>();
        playerAnimator = this.GetComponent<Animator>();
        playercntrl = transform.parent.GetComponent<PlayerController>();
        playerJmp = transform.parent.GetComponent<Jump>();
    
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = playerrb2d.velocity.x;
        //print("is player grounded " + playerJmp.IsGrounded);
        //print("player velocity is  " + playerVelocity);

        if (Mathf.Abs(playerVelocity) < 5 && playerJmp.IsGrounded == true)
        {
 
            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("grounded", true);
            playerAnimator.SetBool("jump_rise", false);
            playerAnimator.SetBool("jump_fall", false);

        }
        else if (Mathf.Abs(playerVelocity) > 5 && playerJmp.IsGrounded == true)
        {

            playerAnimator.SetBool("run", true);
            playerAnimator.SetBool("grounded", true);
            playerAnimator.SetBool("jump_rise", false);
            playerAnimator.SetBool("jump_fall", false);
        }

        else if (playerJmp.IsGrounded == false   && playerrb2d.velocity.y > 0)
        {
            //print("jump up animation is playing");
            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("grounded", false);
            playerAnimator.SetBool("jump_rise", true);
            playerAnimator.SetBool("jump_fall", false);

        }

        else if (playerJmp.IsGrounded == false && playerJmp.hit2dWallhanging == false  && playerrb2d.velocity.y < 0)
        {
            //print("jump down animation is playing");

            //print("player falling");
            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("grounded", false);
            playerAnimator.SetBool("jump_rise", false);
            playerAnimator.SetBool("jump_fall", true);
        }
        else if (playerJmp.IsGrounded == false && playerJmp.hit2dWallhanging == true && Mathf.Abs(jstk.Horizontal) < .3f && playerrb2d.velocity.y < 0)
        {
            //print("jump down animation is playing");
            
        }


        else if (playerJmp.IsGrounded == false && playerJmp.hit2dWallhanging == true && Mathf.Abs(jstk.Horizontal) > .3f)
        {
            //print("hanging with the wall");
            //todo add wall hanginf animation
        }

        //print("jstk command is "+Mathf.Abs(jstk.Horizontal));



    }



    void changeAnimState(string newState)
    {
        if (newState == currentAnimstate) return;
        playerAnimator.CrossFade(newState,0,-1,0);
        currentAnimstate = newState;
    }

}
