using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{

    public GameObject gnd;
    public Transform GndSpawnerPosition;
    public float FireRate = 1f;
    public float NextTimeToThrw = 0f;
    //public Transform butt;
    private OnclickedButton clickbuttGrend;
    public float throwForce;

    


    // Start is called before the first frame update
    void Start()
    {
        GndSpawnerPosition = gameObject.GetComponent<Transform>();
        Transform canvas = GameObject.Find("Canvas").transform;
        clickbuttGrend = canvas.GetChild(3).GetComponent<OnclickedButton>();

    }

    // Update is called once per frame
    void Update()
    {

        if (clickbuttGrend.isButtPres == true && Time.time >= NextTimeToThrw)
        {
            NextTimeToThrw = Time.time + 1f / FireRate;
            Debug.Log("grenade should be spawned");

            GameObject spawnedGnd =  (GameObject)Instantiate(gnd, GndSpawnerPosition.position,GndSpawnerPosition.rotation);
            print(spawnedGnd.name);
            Rigidbody2D rb2dGnd =spawnedGnd.GetComponent<Rigidbody2D>();
            if(rb2dGnd != null)
            {
                rb2dGnd.AddForce(GndSpawnerPosition.forward*throwForce, ForceMode2D.Impulse);
                print("lunch grenade");

            }
            
        }

    }
}
