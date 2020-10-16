using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHP : MonoBehaviour
{
    // Start is called before the first frame update
    public float InitialHP;
    public float currentHP;
    public GameObject destroyParticl;
    void Start()
    {
        //Debug.Log(this.name+" HP  "+ InitialHP);
        currentHP = InitialHP;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP < 0)
        {
            //Debug.Log("should kill yourself");
            if (destroyParticl != null)
            {
                Instantiate(destroyParticl, this.transform.position,Quaternion.identity);
                
            }
            Destroy(gameObject);
            
        }
        
    }

    public void reduceHP(float damage)
    {
        currentHP = currentHP - damage;
    }
}
