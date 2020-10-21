using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    public Transform feetPosition_1;
    public Transform feetPosition_2;
   
    public float checkdistance;
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
    public OnclickedButton clickbuttJump;
    public Joystick jstk;
    
    
    


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Transform canvas = GameObject.Find("Canvas").transform;
        clickbuttJump = canvas.GetChild(1).GetComponent<OnclickedButton>();
        //print(clickbutt.name);
        


    }

    // Update is called once per frame
    void Update()
    {

        if(Physics2D.Raycast(feetPosition_1.position, -feetPosition_1.up,checkdistance,whatIsGround) || Physics2D.Raycast(feetPosition_2.position, -feetPosition_2.up, checkdistance, whatIsGround))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
       
       
        Debug.DrawRay(this.transform.position, this.transform.right * hit2dWallDetectionLength, Color.green);
        Debug.DrawRay(feetPosition_1.position, - feetPosition_1.up* checkdistance,Color.white);
        Debug.DrawRay(feetPosition_2.position, -feetPosition_2.up * checkdistance, Color.white);


        hit2dWallhanging = Physics2D.Raycast(this.transform.position, this.transform.right, hit2dWallDetectionLength, walllayer);

        if (clickbuttJump.isButtPres == true && (IsGrounded == true || jump_count > 1))
        {
           
            
            rb2d.velocity = Vector2.up * jump_force;
            jump_count = jump_count - 1;
            //print("executed");
            clickbuttJump.isButtPres = false;


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
        else if(rb2d.velocity.y >0 && !clickbuttJump.isButtPres == false) {
     
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier -1)* Time.deltaTime;
    
        }
        if(IsGrounded == true)
        {
            jump_count = 2;
        }

    }
   


}
