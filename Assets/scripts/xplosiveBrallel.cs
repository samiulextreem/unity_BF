using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xplosiveBrallel : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Isdamaged = false;
    public float FuseTime = 3f;
    public float countdown;
    public float xPlotionshake;
    public float xPlotiontime;
    public float BarrelDamage;
    public float xplotionRadious;
    public float xplotionForce;
    public Rigidbody2D BarrelBody;

    public GameObject ExplotionPartical;
    public tileHealth tilHlt;
    public float checkpointRad;
    public GameObject DestructionEffect;
    public LayerMask bodypartsLayer;
    public bool willDestroyNextFrame;
    public float upwardsModifier = 0.8F;
    public Vector3 incermntlTileDustructForBarrel;
    void Start()
    {
        tilHlt = GameObject.Find("Grid").GetComponentInChildren<tileHealth>();
        countdown = FuseTime;
        BarrelBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Isdamaged == true)
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
                Destroy(gameObject);
            }
            else
            {
                countdown = countdown - Time.deltaTime;


                if (countdown <= 0)
                {
                    explodeBarrel();

                    if (ExplotionPartical != null)
                    {
                        Instantiate(ExplotionPartical, this.transform.position, Quaternion.identity);
                    }

                    //Destroy(gameObject);
                }

            }

        }

    }

    void explodeBarrel()
    {
        //print("executed explode func");

        //instanciate partical effect
        Collider2D[] objectsInExplotion = Physics2D.OverlapCircleAll(transform.position, xplotionRadious);

        foreach (Collider2D nearbyObject in objectsInExplotion)
        {
            ObjectHP hp = nearbyObject.GetComponent<ObjectHP>();
            if (hp != null)
            {
                hp.reduceHP(BarrelDamage);
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
                incermntlTileDustructForBarrel = this.transform.position + new Vector3(x, y, 0);
                Debug.DrawRay(incermntlTileDustructForBarrel, Vector3.up, Color.red);
                if (tilHlt.destructableTileMap.HasTile(tilHlt.destructableTileMap.WorldToCell(incermntlTileDustructForBarrel)))
                {
                    tilHlt.destructableTileMap.SetTile(tilHlt.destructableTileMap.WorldToCell(incermntlTileDustructForBarrel), null);
                    Instantiate(DestructionEffect, incermntlTileDustructForBarrel, Quaternion.identity);


                }

            }

        }




        willDestroyNextFrame = true;
        //explosionCameraShake.instance.shakeCamera(xPlotionshake, xPlotiontime);

    }

}
