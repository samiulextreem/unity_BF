using JetBrains.Annotations;
using MiscUtil.Extensions.TimeRelated;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public Vector3 pos;
    public GameObject DestructionEffect;

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
  
        countdown = countdown - Time.deltaTime;
        Debug.DrawRay(pos, Vector3.up, Color.red);

        if (countdown <= 0)
        {
            explodeGrenade();
            
            if (ExplotionPartical != null)
            {
                Instantiate(ExplotionPartical, this.transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }



    void explodeGrenade()
    {
        float upwardsModifier = 0.8F;
        
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
                pos = this.transform.position + new Vector3(x, y, 0);
                Debug.DrawRay(pos, Vector3.up, Color.red);
                if (tilHlt.destructableTileMap.HasTile(tilHlt.destructableTileMap.WorldToCell(pos)))
                {
                    tilHlt.destructableTileMap.SetTile(tilHlt.destructableTileMap.WorldToCell(pos), null);
                    Instantiate(DestructionEffect, pos, Quaternion.identity);


                }

            }

            

        }
        

        //explosionCameraShake.instance.shakeCamera(xPlotionshake, xPlotiontime);
      

    }



  
}
