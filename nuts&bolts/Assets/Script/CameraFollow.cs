using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform target;

    public float smoothSpeed = 1;
    public Vector3 offset ;

    enum Look {
        up,
        down,
        left,
        right
    }

    Look v;

    void Start(){
        
        v = Look.up;
        offset.y = 5;
        offset.z = -10;


    }

    private void next_look_s(){
        switch(v){
            case Look.up:
            offset.x = 0;
            offset.z = 10;
            v = Look.down;
            break;

            case Look.down:
            offset.x = 0;
            offset.z = -10;
            v = Look.up;
            break;

            case Look.left:
            offset.x = 10;
            offset.z = 0;
            v = Look.right;
            break;

            case Look.right:
            offset.x = -10;
            offset.z = 0;
            v = Look.left;
            break;

            default:
            break;
        }

    }

    private void next_look_d(){
        switch(v){
            case Look.up:
                offset.x = 10;
                offset.z = 0;
                v = Look.right;
                break;

            case Look.down:
                offset.x = -10;
                offset.z = 0;
                v = Look.left;
                break;

            case Look.left:
                offset.x = 0;
                offset.z = -10;
                v = Look.up;
                break;

            case Look.right:
                offset.x = 0;
                offset.z = 10;
                v = Look.down;
                break;

            default:
                break;
        }    
        
    }

    private void next_look_a(){
        switch (v) {
            case Look.up:
                offset.x = -10;
                offset.z = 0;
                v = Look.left;
                break;

            case Look.down:
                offset.x = 10;
                offset.z = 0;
                v = Look.right;
                break;

            case Look.left:
                offset.x = 0;
                offset.z = 10;
                v = Look.down;
                break;

            case Look.right:
                offset.x = 0;
                offset.z = -10;
                v = Look.up;
                break;

            default:
                break;
        }
        
    }

    void Update(){
        if(Input.GetKeyDown("s")){

            next_look_s();
            Debug.Log(v);
            

        }
        if(Input.GetKeyDown("d")){

            next_look_d();
            Debug.Log(v);

            
        }
        if(Input.GetKeyDown("a")){

            next_look_a();
            Debug.Log(v);

            
        }

    }


     void LateUpdate() 
    {


        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);  
        

        
    }
}
