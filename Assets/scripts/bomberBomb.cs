using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomberBomb : MonoBehaviour
{
    public float FuseTime = 10f;
    public float countdown;
    public float xPlotionshake;
    public float xPlotiontime;
    public float grenadeDamage;
    public float xplotionRadious;
    public float xplotionForce;


    public GameObject DestructionEffect;
    public GameObject ExplotionPartical;
    public tileHealth tilHlt;
    public float checkpointRad;
    public Transform Bomberplyr;
    public Vector3 pos;
 
    public LayerMask bodypartsLayer;
    public bool willDestroyNextFrame;
    public float upwardsModifier = 0.8F;
    public bool shouldDestroyBomber = false;



    // Start is called before the first frame update
    void Start()
    {
        tilHlt = GameObject.Find("Grid").GetComponentInChildren<tileHealth>();

        countdown = FuseTime;

    }

    // Update is called once per frame
    void Update()
    {

        if (willDestroyNextFrame == true)
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
            shouldDestroyBomber = true;
        }
        else
        {
            countdown = countdown - Time.deltaTime;
            Debug.DrawRay(pos, Vector3.up, Color.red);

            if (countdown <= 0)
            {
                explodeMyself();

                if (ExplotionPartical != null)
                {
                    Instantiate(ExplotionPartical, this.transform.position, Quaternion.identity);
                }

            }

        }


    }



    void explodeMyself()
    {

        //print("executed explode func");

        //instanciate partical effect
        Collider2D[] objectsInExplotion = Physics2D.OverlapCircleAll(transform.position, xplotionRadious);

        foreach (Collider2D nearbyObject in objectsInExplotion)
        {
            ObjectHP hp = nearbyObject.GetComponent<ObjectHP>();
            if (hp != null)
            {
                hp.reduceHP(grenadeDamage);
            }
            Rigidbody2D rb2d = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb2d != null && nearbyObject.name != this.name)
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








        for (int i = 0; i < checkpointRad; i++)
        {
            float angle = i;
            for (float incremental = 0; incremental < xplotionRadious; incremental = incremental + .2f)
            {
                float x = Mathf.Cos(angle) * incremental;
                float y = Mathf.Sin(angle) * incremental;
                pos = this.transform.position + new Vector3(x, y, 0);
                Debug.DrawRay(pos, Vector3.up, Color.red);
                if (tilHlt.destructableTileMap.HasTile(tilHlt.destructableTileMap.WorldToCell(pos)))
                {
                    tilHlt.destructableTileMap.SetTile(tilHlt.destructableTileMap.WorldToCell(pos), null);
                    Instantiate(DestructionEffect, pos, Quaternion.identity);

                }

            }

        }

        willDestroyNextFrame = true;
        //explosionCameraShake.instance.shakeCamera(xPlotionshake, xPlotiontime);

    }



}
