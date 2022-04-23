using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
   
    public Rigidbody rigidB;
    public int speed = 10;
    

    void Start() {
        rigidB = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
         if(Input.GetKey("w")){ 
            rigidB.AddForce(Vector3.forward * speed);
        }
        
    }
}
