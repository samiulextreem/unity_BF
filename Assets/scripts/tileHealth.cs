using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public Tilemap destructableTileMap;
    public float tileDistance;
    public Vector2 hitPosition = Vector2.zero;

    void Start()
    {
        destructableTileMap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(hitPosition, destructableTileMap.WorldToCell(hitPosition), Color.red);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {


            

            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x+ hit.normal.x * tileDistance ;
                hitPosition.y = hit.point.y+ hit.normal.y* tileDistance ;
                
                destructableTileMap.SetTile(destructableTileMap.WorldToCell(hitPosition), null);
               
               
                Debug.Log("hit the tilemap");
            }
            
        }
    }
   
}
