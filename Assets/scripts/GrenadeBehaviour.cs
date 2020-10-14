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


    void Start()
    {
        countdown = FuseTime;
        print(this.name);
    }

    // Update is called once per frame
    void Update()
    {
        countdown = countdown - Time.deltaTime;
        
        if(countdown <= 0)
        {
            explodeGrenade();
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
            Rigidbody2D rb2d = nearbyObject.GetComponent<Rigidbody2D>();
            if(rb2d != null && nearbyObject.name != this.name)
            {
                //AddExplosionForce(rb2d, xplotionForce,transform.position);
                var explotionDir = rb2d.position - (Vector2) transform.position;
                var explotionDist = explotionDir.magnitude;
                
                explotionDir.y = explotionDir.y + upwardsModifier;
                explotionDir.Normalize();
                print("Name -- " + nearbyObject.name + " normalize explotdir -- " + explotionDir + "  explot dist --" + explotionDist);

                rb2d.AddForce( xplotionForce* explotionDir,ForceMode2D.Impulse);
                //rb2d.AddForce(explotionDir*xplotionForce, ForceMode2D.Force);

            }
        }
        
        //explosionCameraShake.instance.shakeCamera(xPlotionshake, xPlotiontime);
      

    }



    /*
    void AddExplosionForce(Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    }
    */
}
