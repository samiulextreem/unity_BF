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
    public GameObject bodyPart_1;
    public GameObject bodyPart_2;
    public GameObject bodyPart_3;
    public GameObject bodyPart_4;
    public Transform bodTns_1;
    public Transform bodTns_2;
    public Transform bodTns_3;
    public Transform bodTns_4;
    public xplosiveBrallel xplbarrel;


    void Start()
    {
        //Debug.Log(this.name+" HP  "+ InitialHP);
        currentHP = InitialHP;
        if (this.CompareTag("barrel"))
        {
            //print("this is xplosive barrel");
            xplbarrel = GetComponent<xplosiveBrallel>();
            //print("xplosive barrel status " + xplbarrel.Isdamaged);
        }



    }

    // Update is called once per frame
    void Update()
    {
        //this is for player and enemy character
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
            if(bodyPart_1 != null && bodTns_1 != null)
            {
                Instantiate(bodyPart_1, bodTns_1.position, Quaternion.identity);
            }
            if (bodyPart_2 != null && bodTns_2 != null)
            {
                Instantiate(bodyPart_2, bodTns_2.position, Quaternion.identity);
            }

            if (bodyPart_3 != null && bodTns_3 != null)
            {
                Instantiate(bodyPart_3, bodTns_3.position, Quaternion.identity);
            }

            if (bodyPart_4 != null && bodTns_4 != null)
            {
                Instantiate(bodyPart_4, bodTns_4.position, Quaternion.identity);
            }

            
            //print("should dismember unit");
            Destroy(gameObject);
        }
  
        
    }

    public void reduceHP(float damage)
    {
        currentHP = currentHP - damage;
        if(xplbarrel != null)
        {
            xplbarrel.Isdamaged = true;
        }
        
    }

}
