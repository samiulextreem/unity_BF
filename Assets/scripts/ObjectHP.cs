using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHP : MonoBehaviour
{
    // Start is called before the first frame update
    public float InitialHP;

    void Start()
    {
        Debug.Log(this.name+" HP  "+ InitialHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
