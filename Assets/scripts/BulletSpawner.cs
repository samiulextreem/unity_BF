using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bullet;
    public Transform bulletSpawnerPosition;
    public float FireRate = 15f;
    private float NextTimeToFire = 0f;

    private OnclickedButton clickbuttBullet;
 
  
    //public bool ShouldMookShoot = true;
    private Animator gunAnim;

    const string GUN_IDLE = "gun_idle";
    const string GUN_SHOOT = "gun_shoot";
    private string currentAnimstate;




    void Start()
    {
        bulletSpawnerPosition = gameObject.GetComponent<Transform>();
        Transform canvas = GameObject.Find("Canvas").transform;
        clickbuttBullet = canvas.GetChild(2).GetComponent<OnclickedButton>();
    
        //Debug.Log("Player's Parent script: " + this.transform.root.name);
        gunAnim = this.GetComponent<Animator>();

        

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        //gunAnim.Play(GUN_IDLE);
     
        if(clickbuttBullet.isButtPres == true && Time.time >= NextTimeToFire){
            NextTimeToFire = Time.time + 1f / FireRate;
            Shoot();

        }
        if (clickbuttBullet.isButtPres == false)
        {
            changeAnimState(GUN_IDLE);
        }
 
      

    }


    void Shoot(){
        //Debug.Log("spawning bullet");
        Instantiate(bullet,bulletSpawnerPosition.position,bulletSpawnerPosition.rotation);
        changeAnimState(GUN_SHOOT);
        //hasshootingOccured = true;
        //instanciate light 
    }


    void changeAnimState(string newState)
    {
        if (newState == currentAnimstate) return;
        gunAnim.Play(newState);
        currentAnimstate = newState;
    }
}


