﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    // Start is called before the first frame update


    public float searchRadious;

    public LayerMask playerMask;
    public LayerMask whatIsGround;

    public bool IsAwareOfPlayer;
    public bool isPlayerInarea = false;
    public GameObject mookBulPoint;
    public bomberBomb bmb;
    private mookBulletSpawner mkbulletspnr;
    public bool isPlayerPatroling;
    public Movement mvmnt;
    public bool isMovingRight;
    public bool isMovingLeft;
    public float checkobstracle;
    private Rigidbody2D rbenmy;
    private Transform gndChecker;
    public GameObject gndChkrObjct;
    public bool IsMoving = false;
    public float dicisionCooldown;
    public float flipSpreadCooldown;
    private float flipspread;
    public float dicisionspread;
    public int bomberMvSpeed = 710;

    public bool IstakingBreak = false;
    public float highValue;
    public float lowValue;
    public ObjectHP myHP;
    public GameObject playerPos;
    
    void Start()
    {
        mkbulletspnr = mookBulPoint.GetComponent<mookBulletSpawner>();

     
        
        rbenmy = GetComponent<Rigidbody2D>();
        mkbulletspnr.FireRate = 0;
        gndChecker = gndChkrObjct.GetComponent<Transform>();
        myHP = GetComponent<ObjectHP>();

        //print(mkbulletspnr.FireRate);

        if (this.transform.eulerAngles.y == 180)
        {
            //Debug.Log(this.name+"is facing right");
            isMovingRight = true;
            isMovingLeft = false;
        }
        if (this.transform.eulerAngles.y == 0)
        {
            //Debug.Log(this.name+"is facing left");
            isMovingRight = false;
            isMovingLeft = true;
        }

        dicisionspread = UnityEngine.Random.Range(highValue, lowValue);
        if (dicisionspread > .5)
        {
            IsMoving = true;
            IstakingBreak = false;
        }
        else if (dicisionspread < .5)
        {
            IsMoving = false;
            IstakingBreak = true;
        }

    }




    // Update is called once per frame
    void Update()
    {

        if (this.CompareTag("mook"))
        {
            behaveConditionMook();
     
        }
        if (this.CompareTag("bomber"))
        {
            behaveConditionBomber();
        }

   
    }

    void FindVisibleTargets(Collider2D targetInArea)
    {

        RaycastHit2D[] hitObject = Physics2D.RaycastAll(transform.position, (targetInArea.transform.position - this.transform.position).normalized, searchRadious);

        Debug.DrawLine(this.transform.position, (this.transform.position + (targetInArea.transform.position - this.transform.position).normalized * 3), Color.blue);

        Debug.DrawLine(this.transform.position, targetInArea.transform.position, Color.white);
        if (hitObject.Length > 1)
        {
            float angleDirection = Vector3.Angle(this.transform.right, (targetInArea.transform.position - this.transform.position).normalized);

            if (hitObject[1].collider.gameObject.name == "player" && angleDirection < 55f)
            {

                //Debug.Log("object name " + hitObject[1].collider.gameObject.name);

                //print("player angle is " + angleDirection);

                IsAwareOfPlayer = true;
                
            }

        }

    }

    void flipDirection()
    {
        if (this.transform.eulerAngles.y == 180)
        {
            this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            //print("set rotation to zero");
            return;
            
        }
        if (this.transform.eulerAngles.y == 0)
        {

            this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            //print("set rotation to 180");
            return;
        }
    }

    void behaveConditionMook()
    {
        if (myHP.isDroppedDown == false)
        {
            //Debug.DrawRay(gndChecker.position, -gndChecker.up, Color.yellow);
            //Physics2D.Raycast(gndChecker.position, -gndChecker.up, checkobstracle, whatIsGround);
            Collider2D targetInArea = Physics2D.OverlapCircle(this.transform.position, searchRadious, playerMask);
            if (IsAwareOfPlayer == false)
            {
                if (dicisionCooldown <= 0)
                {
                    dicisionspread = UnityEngine.Random.Range(highValue, lowValue);
                    //print("dicision spread  " + dicisionspread);
                    dicisionCooldown = 3;
                }
                if (dicisionspread > 1)
                {
                    //print("took dicision to move");
                    IsMoving = true;
                    IstakingBreak = false;
                }
                else if (dicisionspread < 1)
                {
                    //print("took dicision to break");
                    IsMoving = false;
                    IstakingBreak = true;
                }
                if (flipSpreadCooldown <= 0)
                {
                    flipspread = UnityEngine.Random.Range(highValue, lowValue);
                    //print("flip spread  " + flipspread);
                    if (flipspread > 1)
                    {
                        //print("i should flip");
                        flipDirection();
                    }

                    flipSpreadCooldown = 2;

                }

                dicisionCooldown = dicisionCooldown - Time.deltaTime;
                flipSpreadCooldown = flipSpreadCooldown - Time.deltaTime;

                if (IsMoving == true)
                {
                    //Debug.Log(" moving");
                    IstakingBreak = false;
                    if (isMovingRight == true)
                    {
                        isMovingLeft = false;
                        mvmnt.moving_right();
                        if (Physics2D.Raycast(this.transform.position, this.transform.right, checkobstracle, whatIsGround))
                        {
                            isMovingLeft = true;
                            isMovingRight = false;
                        }
                        if (!Physics2D.Raycast(gndChecker.position, -gndChecker.up, checkobstracle, whatIsGround))
                        {
                            isMovingLeft = true;
                            isMovingRight = false;
                        }
                    }
                    if (isMovingLeft == true)
                    {
                        isMovingRight = false;
                        mvmnt.moving_left();
                        if (Physics2D.Raycast(this.transform.position, this.transform.right, checkobstracle, whatIsGround))
                        {
                            isMovingLeft = false;
                            isMovingRight = true;
                        }
                        if (!Physics2D.Raycast(gndChecker.position, -gndChecker.up, checkobstracle, whatIsGround))
                        {
                            isMovingLeft = false;
                            isMovingRight = true;
                        }
                    }

                }
                if (IstakingBreak == true)
                {
                    IsMoving = false;

                }

                if (targetInArea != null && IsAwareOfPlayer == false)
                {
                    //enemy is aware of player and stop patrolling

                    FindVisibleTargets(targetInArea);

                }

                if (IsAwareOfPlayer == true)
                {
                    isMovingLeft = false;
                    isMovingRight = false;
                    mkbulletspnr.FireRate = 2;
                    mkbulletspnr.ShouldMookShoot = true;
                    Debug.Log("FOUND PLAYERRRRRRRRRRRR");


                }

            }

        }

    }

    void behaveConditionBomber()
    {
        if (myHP.currentHP <= 0)
        {
            bmb = GetComponentInChildren<bomberBomb>();
            bmb.IsfuseLit = true;
            bmb.countdown = 0;
            
        }
        if (myHP.isDroppedDown == false)
        {

            if (IsAwareOfPlayer == false)
            {
                Collider2D targetInArea = Physics2D.OverlapCircle(this.transform.position, searchRadious, playerMask);
                if (flipSpreadCooldown <= 0)
                {

                    flipspread = UnityEngine.Random.Range(highValue, lowValue);

                    if (flipspread > 1)
                    {
                        //print("should flip direction");
                        flipDirection();
                    }

                    flipSpreadCooldown = 2;
                }

                flipSpreadCooldown = flipSpreadCooldown - Time.deltaTime;

                if (targetInArea != null && IsAwareOfPlayer == false)
                {
                    //enemy is aware of player and stop patrolling

                    FindVisibleTargets(targetInArea);

                }
                if (IsAwareOfPlayer == true)
                {
                    isMovingLeft = false;
                    isMovingRight = false;

                    //Debug.Log("FOUND PLAYERRRRRRRRRRRR");


                }

            }

            if (IsAwareOfPlayer == true)
            {
                bmb = GetComponentInChildren<bomberBomb>();
                bmb.IsfuseLit = true;
                //Debug.Log("run to enemy");

                //bomberBomb.SetActive(true);
                float distanceX;
                if (playerPos != null && bmb != null)
                {
                    distanceX = (playerPos.transform.position.x - this.transform.position.x);
                    //print("distance x is "+distanceX);

                    mvmnt.movement_speed = bomberMvSpeed;

                    if (distanceX > 1)
                    {
                        mvmnt.moving_right();
                    }
                    else if (distanceX < -1)
                    {
                        mvmnt.moving_left();
                    }
                    if (Mathf.Abs(distanceX) < 1)
                    {
                        bmb.countdown = 0;
                        //print("bomb countdown "+bmb.countdown);
                    }
                }

            }


        }



    }



}