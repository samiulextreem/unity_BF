using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mookBulletSpawner : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletSpawnerPosition;
    public float FireRate = 15f;
    private float NextTimeToFire = 0f;
    //public Transform butt;
    private OnclickedButton clickbutt;
    //public GameObject ParentObject;
    public bool ShouldMookShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnerPosition = gameObject.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= NextTimeToFire && ShouldMookShoot == true)
        {
            NextTimeToFire = Time.time + 1f / FireRate;
            Shoot();

        }

    }

    void Shoot()
    {
        //Debug.Log("spawning bullet");
        Instantiate(bullet, bulletSpawnerPosition.position, bulletSpawnerPosition.rotation);
    }
}
