using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opacity : MonoBehaviour
{
    public GameObject solidObj;
    public GameObject transparentObj;

    private void Start()
    {
        MakeSolid();
        /*if (Vector3.Distance(solidObj.transform.position, GameObject.Find("Player1").transform.position) 
            <
            Vector3.Distance(solidObj.transform.position, GameObject.Find("Player2").transform.position))
        {
            transparentObj.transform.parent = GameObject.Find("P1Map").transform.Find("Generated Map");
        }
        else
        {
            transparentObj.transform.parent = GameObject.Find("P2Map").transform.Find("Generated Map");
        }*/

        transparentObj.transform.parent = solidObj.transform.parent;
    }

    public void MakeTransparent()
    {
        if (this.gameObject == solidObj)
        {
            transparentObj.SetActive(true);
            solidObj.SetActive(false);
        }

    }
    
    public void MakeSolid()
    {
        if (this.gameObject == transparentObj)
        {
            solidObj.SetActive(true);
            transparentObj.SetActive(false);
        }
    }
}
