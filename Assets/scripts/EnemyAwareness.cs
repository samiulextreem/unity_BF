using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float dicisionspread;

    public bool IstakingBreak = false;



    void Start()
    {
        mkbulletspnr = mookBulPoint.GetComponent<mookBulletSpawner>();
        rbenmy = GetComponent<Rigidbody2D>();
        mkbulletspnr.FireRate = 0;
        gndChecker = gndChkrObjct.GetComponent<Transform>();

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

        dicisionspread = UnityEngine.Random.Range(1, 0);
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
        Debug.DrawRay(gndChecker.position, -gndChecker.up, Color.yellow);
        Physics2D.Raycast(gndChecker.position, -gndChecker.up, checkobstracle, whatIsGround);
        Collider2D targetInArea = Physics2D.OverlapCircle(this.transform.position, searchRadious, playerMask);
        if (IsAwareOfPlayer == false)
        {
            if (dicisionCooldown <= 0)
            {


                dicisionspread = UnityEngine.Random.Range(1, 0);
                print("dicision spread  " + dicisionspread);
                if (dicisionspread > .5)
                {
                    print("took dicision to move");
                    IsMoving = true;
                    IstakingBreak = false;
                }
                else if (dicisionspread < .5)
                {
                    print("took dicision to break");
                    IsMoving = false;
                    IstakingBreak = true;
                }
                dicisionCooldown = 5;

            }
            dicisionCooldown = dicisionCooldown - Time.deltaTime;





            if (IsMoving == true)
            {
                Debug.Log(" moving");
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
                Debug.Log(" break");
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


            }

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
                mkbulletspnr.FireRate = 2;
                mkbulletspnr.ShouldMookShoot = true;
            }

        }

    }



}