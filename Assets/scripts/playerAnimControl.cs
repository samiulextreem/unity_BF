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
    const string PLAYER_IDLE = "player_idle";
    const string PLAYER_RUN = "player_run";
    const string PLAYER_JUMP = "player_jump";
    const string PLAYER_FALL = "player_fall";
    const string PLAYER_HIT = "player_hit";
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
        if (Mathf.Abs(playerVelocity) < 1 && playerJmp.IsGrounded == true)
        {

            changeAnimState(PLAYER_IDLE);

        }
        else if (Mathf.Abs(playerVelocity) > 1 && playerJmp.IsGrounded == true)
        {

            changeAnimState(PLAYER_RUN);
        }

        else if (playerJmp.IsGrounded == false   && playerrb2d.velocity.y > 0)
        {
            //print("jump up animation is playing");
            changeAnimState(PLAYER_JUMP);
        }

        else if (playerJmp.IsGrounded == false && playerJmp.hit2dWallhanging == false  && playerrb2d.velocity.y < 0)
        {
            //print("jump down animation is playing");
            changeAnimState(PLAYER_FALL);
        }
        else if (playerJmp.IsGrounded == false && playerJmp.hit2dWallhanging == true && Mathf.Abs(jstk.Horizontal) < .3f && playerrb2d.velocity.y < 0)
        {
            //print("jump down animation is playing");
            changeAnimState(PLAYER_FALL);
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
        playerAnimator.Play(newState);
        currentAnimstate = newState;
    }

}
