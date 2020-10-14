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
    //public Transform butt;
    private OnclickedButton clickbuttBullet;
    //public GameObject ParentObject;
    //public bool ShouldMookShoot = true;
    
    




    void Start()
    {
        bulletSpawnerPosition = gameObject.GetComponent<Transform>();
        Transform canvas = GameObject.Find("Canvas").transform;
        clickbuttBullet = canvas.GetChild(2).GetComponent<OnclickedButton>();
        //print(clickbutt.name);
        //Debug.Log("Player's Parent script: " + this.transform.root.name);
      
      
   

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if(clickbuttBullet.isButtPres == true && Time.time >= NextTimeToFire){
            NextTimeToFire = Time.time + 1f / FireRate;
            Shoot();

        }

    }


    void Shoot(){
        //Debug.Log("spawning bullet");
        Instantiate(bullet,bulletSpawnerPosition.position,bulletSpawnerPosition.rotation);
    }
}


