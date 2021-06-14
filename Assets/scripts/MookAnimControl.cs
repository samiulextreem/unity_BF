using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MookAnimControl : MonoBehaviour
{
    // Start is called before the first frame update

    private float mookVelocity;
    private Rigidbody2D mookrb2d;
    private Animator mookAnimator;
    private string currentAnimstate;
    const string MOOK_IDLE = "enem_idle";
    const string MOOK_RUN = "enem_run";
    void Start()
    {
        mookrb2d = transform.parent.GetComponent<Rigidbody2D>();
        mookAnimator = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        mookVelocity = mookrb2d.velocity.x;
        //print("is player grounded " + playerJmp.IsGrounded);
        //print("player velocity is  " + playerVelocity);
        if (Mathf.Abs(mookVelocity) < 1)
        {

            changeAnimState(MOOK_IDLE);

        }
        else if (Mathf.Abs(mookVelocity) > 1)
        {

            changeAnimState(MOOK_RUN);
        }


    }
    void changeAnimState(string newState)
    {
        if (newState == currentAnimstate) return;
        mookAnimator.Play(newState);
        currentAnimstate = newState; 
    }
}
