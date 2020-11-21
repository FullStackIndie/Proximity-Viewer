using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{   
    public Light l = new Light();

    void Start()
    {
        l.type = LightType.Spot;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            l.transform.eulerAngles += Vector3.right * Time.deltaTime * 100;
        }    
        if (Input.GetKey(KeyCode.D))
        {
            
               l.transform.eulerAngles += Vector3.left * Time.deltaTime * 100;
            
        }
    }
}
