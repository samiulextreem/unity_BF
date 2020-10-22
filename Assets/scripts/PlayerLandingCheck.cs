using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public Jump jmp;

    public bool isAirborn;
    public bool justLanded;
    public float shakeIntensity;
    
    
    
    
    
    void Start()
    {
        jmp = GetComponent<Jump>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jmp.IsGrounded == false)
        {
            isAirborn = true;
        }
        if(isAirborn == true)
        {
            if (Physics2D.Raycast(jmp.feetPosition_1.position, -jmp.feetPosition_1.up,jmp.checkdistance, jmp.whatIsGround) || Physics2D.Raycast(jmp.feetPosition_2.position, -jmp.feetPosition_2.up, jmp.checkdistance, jmp.whatIsGround))
            {
                //Debug.Log("Just landed");
                OnlandCameraShake.instance.shakeCamera(shakeIntensity, .1f);
                isAirborn = false;
                

            }
         
        }
    }
}
