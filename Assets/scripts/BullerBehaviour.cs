using MiscUtil.Extensions.TimeRelated;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb2dBullet;
    public float bulletSpeed;
    public float maxTravelDistance = 40;
    public float distanceTraveled;
    
    public Transform originatePointforBullet;
    public float initialPos;
    public float randomSpreadRotationZ ;
    public float bulletSpreadMax;
    public float bulletSpreadMin;
    public GameObject ObjectToDamage;
    public ObjectHP HP;
    public float playerBullateDamage;
    //public Rigidbody2D Collidedrb2D;
    public GameObject BloodEffect;

 



    void Start()
    {
        randomSpreadRotationZ = UnityEngine.Random.Range(bulletSpreadMin, bulletSpreadMax);
        //print("random spread rotationz" + randomSpreadRotationZ);
        
        rb2dBullet = gameObject.GetComponent<Rigidbody2D>();
        originatePointforBullet = gameObject.GetComponent<Transform>();
        originatePointforBullet.Rotate(0,  0, randomSpreadRotationZ);

        
        rb2dBullet.velocity = originatePointforBullet.right * bulletSpeed;
 
        initialPos = originatePointforBullet.position.x;

        
    }

    void Update()
    {
        distanceTraveled =  initialPos - originatePointforBullet.position.x; 
        if (Mathf.Abs(distanceTraveled) > maxTravelDistance )
        {
           
            Destroy(gameObject);
        }



    }

 
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.collider.gameObject.layer);    

        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            //print("object is " + collision.collider.gameObject.name);
            Destroy(gameObject);
        }else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            //print("object is " + collision.collider.gameObject.name);
            Instantiate(BloodEffect, this.transform.position,Quaternion.identity);
            Debug.Log("spawned blood effect");
            Destroy(gameObject);
        }

        ObjectToDamage = GameObject.Find(collision.collider.gameObject.name);
        HP = ObjectToDamage.GetComponent<ObjectHP>();
        if (HP != null)
        {
           
           HP.reduceHP(playerBullateDamage);
            


           //print(ObjectToDamage.name + " has " + HP.currentHP);

        }

        Destroy(gameObject);
        //Collidedrb2D = gameObject.GetComponent<Rigidbody2D>();
        

        


    }

}
