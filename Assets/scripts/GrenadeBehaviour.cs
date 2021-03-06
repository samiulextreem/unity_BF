﻿using JetBrains.Annotations;
using MiscUtil.Extensions.TimeRelated;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;


public class GrenadeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public float FuseTime = 3f;
    public float countdown;
    public float xPlotionshake;
    public float xPlotiontime;
    public float grenadeDamage;
    public float xplotionRadious;
    public float xplotionForce;
    public Rigidbody2D gndBody;
    public float throwForce;
    public float throwYangle;
    public GameObject ExplotionPartical;
    public tileHealth tilHlt;
    public float checkpointRad;
    public Transform plyr;
    public Vector3 incremntlTileDustructForGrand;
    public GameObject DestructionEffect;
    public LayerMask bodypartsLayer;
    public bool willDestroyNextFrame;
    public float upwardsModifier = 0.8F;




    void Start()
    {
        tilHlt = GameObject.Find("Grid").GetComponentInChildren<tileHealth>();
    
        
        plyr = GameObject.Find("player").transform;
        Transform GndOriginalPos = plyr.GetChild(3).GetComponent<Transform>();
        Vector3 x = GndOriginalPos.transform.right;
        Vector3 f = x + new Vector3(0f, throwYangle, 0);
 
        countdown = FuseTime;
        gndBody = this.GetComponent<Rigidbody2D>();
        gndBody.AddForce(f * throwForce, ForceMode2D.Impulse);
        
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if(willDestroyNextFrame == true)
        {
            Collider2D[] bodypartInExplotion = Physics2D.OverlapCircleAll(transform.position, xplotionRadious);
            foreach (Collider2D nearbybodypart in bodypartInExplotion)
            {
                if (nearbybodypart.CompareTag("bodyPart"))
                {
                    //print(nearbybodypart.tag);
                    Rigidbody2D rb2d = nearbybodypart.GetComponent<Rigidbody2D>();

                    if (rb2d != null)
                    {
                        //AddExplosionForce(rb2d, xplotionForce,transform.position);
                        var explotionDir = rb2d.position - (Vector2)transform.position;
                        var explotionDist = explotionDir.magnitude;

                        explotionDir.y = explotionDir.y + upwardsModifier;
                        explotionDir.Normalize();
                        //print("Name -- " + nearbyObject.name + " normalize explotdir -- " + explotionDir + "  explot dist --" + explotionDist);

                        rb2d.AddForce(xplotionForce * explotionDir, ForceMode2D.Impulse);

                    }
                }
            }
            Destroy(gameObject);
        }
        else
        {
            countdown = countdown - Time.deltaTime;
            Debug.DrawRay(incremntlTileDustructForGrand, Vector3.up, Color.red);

            if (countdown <= 0)
            {
                explodeGrenade();

                if (ExplotionPartical != null)
                {
                    Instantiate(ExplotionPartical, this.transform.position, Quaternion.identity);
                }

                //Destroy(gameObject);
            }

        }
 
    }



    void explodeGrenade()
    {
        
        //print("executed explode func");
        
        //instanciate partical effect
        Collider2D[] objectsInExplotion = Physics2D.OverlapCircleAll(transform.position, xplotionRadious);
        
        foreach(Collider2D nearbyObject in objectsInExplotion)
        {
            ObjectHP hp = nearbyObject.GetComponent<ObjectHP>();
            if(hp != null )
            {
                hp.reduceHP(grenadeDamage);
            }
            Rigidbody2D rb2d = nearbyObject.GetComponent<Rigidbody2D>();
            if(rb2d != null && nearbyObject.name != this.name)
            {
                //AddExplosionForce(rb2d, xplotionForce,transform.position);
                var explotionDir = rb2d.position - (Vector2) transform.position;
                var explotionDist = explotionDir.magnitude;
                
                explotionDir.y = explotionDir.y + upwardsModifier;
                explotionDir.Normalize();
                //print("Name -- " + nearbyObject.name + " normalize explotdir -- " + explotionDir + "  explot dist --" + explotionDist);

                rb2d.AddForce( xplotionForce* explotionDir,ForceMode2D.Impulse);
                

            }
        }


       





        for (int i = 0; i < checkpointRad; i++)
        {
            float angle = i;
            for (float incremental = 0; incremental < xplotionRadious; incremental = incremental+.2f)
            {
                float x = Mathf.Cos(angle) *incremental;
                float y = Mathf.Sin(angle) *incremental;
                incremntlTileDustructForGrand = this.transform.position + new Vector3(x, y, 0);
                Debug.DrawRay(incremntlTileDustructForGrand, Vector3.up, Color.red);
                if (tilHlt.destructableTileMap.HasTile(tilHlt.destructableTileMap.WorldToCell(incremntlTileDustructForGrand)))
                {
                    tilHlt.destructableTileMap.SetTile(tilHlt.destructableTileMap.WorldToCell(incremntlTileDustructForGrand), null);
                    Instantiate(DestructionEffect, incremntlTileDustructForGrand, Quaternion.identity);


                }

            }

        }
       
        willDestroyNextFrame = true;
        //explosionCameraShake.instance.shakeCamera(xPlotionshake, xPlotiontime);
      
    }



  
}
