using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Movement movmnt_script;
    public float movspeed;
    
    


    void Start()
    {
        
        movmnt_script = GetComponent<Movement>();
     
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
       if (Input.anyKey)
       {
           
            movmnt_script.player_movement();
       }

    }
  

}
