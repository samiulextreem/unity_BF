using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public int movement_speed;
    
    public float player_mov_accelr ;

    private Rigidbody2D rb2d;
    private Transform playerPosiiton;
    
    public Joystick jstk;
    
    

    private void Awake()
    {
    }

    void Start()
    {
        
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        playerPosiiton = gameObject.GetComponent<Transform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void player_movement()
    {
        //player_mov_accelr = Input.GetAxis("Horizontal");
        player_mov_accelr = jstk.Horizontal;
        if (Mathf.Abs(player_mov_accelr) > .3f){
            Vector3 movement_vec = new Vector3(1,0,0);
            if (player_mov_accelr > .3f)
            {
                orient_myself(0);
                movement_vec = new Vector3(1, 0, 0);
            }
            else if (player_mov_accelr < .3f)
            {
                orient_myself(180);
                movement_vec = new Vector3(-1, 0, 0);
            }

            movement_vec = movement_vec.normalized * movement_speed * Time.deltaTime;
            rb2d.velocity = new Vector2(player_mov_accelr * movement_speed * Time.deltaTime, rb2d.velocity.y);
            ///rb2d.velocity = movement_vec;
        }

     
    }





    public void moving_right() 
    {
        orient_myself(0);
        Vector3 movement_vec = new Vector3(1, 0, 0);
        movement_vec = movement_vec.normalized * movement_speed * Time.deltaTime;

        //rb2d.MovePosition(transform.position + movement_vec);

        //float step = movement_speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, (transform.position + movement_vec), step);
        rb2d.velocity = movement_vec;


    }

    public void moving_left()
    {
       
        orient_myself(180);
        Vector2 movement_vec = new Vector2(-1,0);
        movement_vec = movement_vec.normalized * movement_speed * Time.deltaTime ;

        //rb2d.MovePosition(transform.position + movement_vec);
        
        //float step = movement_speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, (transform.position+movement_vec), step);
        rb2d.velocity = movement_vec;

    }
    public void orient_myself(int angle)
    {
        transform.eulerAngles = new Vector3(0, angle,0);         
    }
  

}
