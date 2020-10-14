﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    public Transform feetPosition;
   
    public float checkradious;
    public LayerMask whatIsGround;
    public int jump_force;
    public bool IsGrounded;
    
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.5f;
    public int jump_count = 2;
    public RaycastHit2D hit2dWallhanging;
    public float hit2dWallDetectionLength;
    int walllayer = 1 << 8;

    public GameObject gun;
    public GameObject gunPoint;
    //public Transform butt;
    public OnclickedButton clickbutt;
    public Joystick jstk;
    
    
    


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Transform canvas = GameObject.Find("Canvas").transform;
        clickbutt = canvas.GetChild(1).GetComponent<OnclickedButton>();
        //print(clickbutt.name);
        


    }

    // Update is called once per frame
    void Update()
    {

        //issue 1---### when at the corner of an edge this IsGrounded return false so no jump (solved)
       
        IsGrounded = Physics2D.OverlapCircle(feetPosition.position, checkradious, whatIsGround);
        Debug.DrawRay(this.transform.position, this.transform.right * hit2dWallDetectionLength, Color.green);
        hit2dWallhanging = Physics2D.Raycast(this.transform.position, this.transform.right, hit2dWallDetectionLength, walllayer);

        if (clickbutt.isButtPres == true && (IsGrounded == true || jump_count > 1))
        {
           
            
            rb2d.velocity = Vector2.up * jump_force;
            jump_count = jump_count - 1;
            //print("executed");
            clickbutt.isButtPres = false;


        }
        if (hit2dWallhanging == true && IsGrounded == false && Mathf.Abs(jstk.Horizontal) > .3)
        {
            //print("grabbed wall");
            if (jump_count != 3)
            {
                jump_count = 3;

            }



        }
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y *(fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb2d.velocity.y >0 && !clickbutt.isButtPres == false) {
     
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier -1)* Time.deltaTime;
    
        }
        if(IsGrounded == true)
        {
            jump_count = 2;
        }

    }
   


}
