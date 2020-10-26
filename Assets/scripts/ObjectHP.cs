using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHP : MonoBehaviour
{
    // Start is called before the first frame update
    public float InitialHP;
    public float currentHP;
    public GameObject destroyParticl;
 
    public bool isDroppedDown = false;
    public GameObject bodyPart;
    void Start()
    {
        //Debug.Log(this.name+" HP  "+ InitialHP);
        currentHP = InitialHP;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP < 15 && isDroppedDown == false)
        {
            //Debug.Log("should kill yourself");
            isDroppedDown = true;
            this.transform.Rotate(0, 0, 90);
            //Destroy(gameObject);
            
        }
        if(currentHP < 0 && isDroppedDown == true)
        {
            //spawn dismembered body part
            Instantiate(bodyPart, this.transform.position, Quaternion.identity);

            Instantiate(bodyPart, this.transform.position, Quaternion.identity);
            //print("should dismember unit");
            Destroy(gameObject);
        }

        
    }

    public void reduceHP(float damage)
    {
        currentHP = currentHP - damage;
    }

}
