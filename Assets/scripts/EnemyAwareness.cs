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
    public bool isMovingRight = true;
    public bool isMovingLeft = false;
    public float checkobstracle;
    private Rigidbody2D rbenmy;
    public Transform transfm;
    

    void Start()
    {
        mkbulletspnr = mookBulPoint.GetComponent<mookBulletSpawner>();
        rbenmy = GetComponent<Rigidbody2D>();
        mkbulletspnr.FireRate = 0;
        //print(mkbulletspnr.FireRate);
        
    }


 

    // Update is called once per frame
    void Update()
    {
        Collider2D targetInArea = Physics2D.OverlapCircle(this.transform.position, searchRadious, playerMask);
        if(IsAwareOfPlayer == false)
        {
            Debug.DrawLine(this.transform.position, transfm.right *checkobstracle, Color.yellow);
            if(isMovingRight == true)
            {
                isMovingLeft = false;
                mvmnt.moving_right();
                if (Physics2D.Raycast(this.transform.position, this.transform.right, checkobstracle, whatIsGround))
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
            }





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

    void FindVisibleTargets(Collider2D targetInArea)
    {

        RaycastHit2D[] hitObject = Physics2D.RaycastAll(transform.position, (targetInArea.transform.position - this.transform.position).normalized, searchRadious);
        
        Debug.DrawLine(this.transform.position, (this.transform.position+(targetInArea.transform.position - this.transform.position).normalized*3), Color.blue);
        
        Debug.DrawLine(this.transform.position, targetInArea.transform.position, Color.white);
        if(hitObject.Length > 1)
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
