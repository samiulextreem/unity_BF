using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public Tilemap destructableTileMap;
    public float tileDistance;
    public Vector2 hitPosition;
    public Vector2 UppaddedTile;
    private WorldTile _tile;
    public float upwWardPadding = .8f;
    public GameObject DestructionPartical;
    
 

    void Start()
    {
        destructableTileMap = GetComponent<Tilemap>();
        


    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(hitPosition, destructableTileMap.WorldToCell(hitPosition), Color.red);
        Debug.DrawLine(UppaddedTile, destructableTileMap.WorldToCell(UppaddedTile), Color.yellow);
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {
            ContactPoint2D hit = collision.GetContact(0);

         
            hitPosition.x = hit.point.x+ hit.normal.x * tileDistance;
            hitPosition.y = hit.point.y+ hit.normal.y * tileDistance;
            Vector3 newVec3 = new Vector3((int)hitPosition.x, (int)hitPosition.y, 0);

            var tiles = GameTiles.instance.tiles;
            /*

            foreach (KeyValuePair<Vector3,WorldTile> kvp in tiles)
                Debug.Log("key is "+ kvp.Key +" value is "+ kvp.Value);
            */



            //print("tile count" + tiles.Count);
            //print("collition point"+newVec3);
            //
            
            if (tiles.TryGetValue(newVec3, out _tile))
            {
                //print("Tile " + _tile.Name + " Health: " + _tile.Health);
                //_tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
                //_tile.TilemapMember.SetColor(_tile.LocalPlace, Color.green);
                
                _tile.Health -= 1;
                if(_tile.Health <= 0)
                {
                    //Debug.Log("tile shuld be killed");
                    destructableTileMap.SetTile(destructableTileMap.WorldToCell(hitPosition), null);
                    
                    UppaddedTile = hitPosition + new Vector2(0, upwWardPadding);
                    destructableTileMap.SetTile(destructableTileMap.WorldToCell(UppaddedTile), null);
                    if (DestructionPartical != null)
                    {
                        Instantiate(DestructionPartical, hitPosition, Quaternion.identity);
                        Instantiate(DestructionPartical, UppaddedTile, Quaternion.identity);
                        //Debug.Log("spawned partical");
                    }

                }
            }
            else
            {
                //Debug.Log("No tile");
            }


        }
    }
   
}
