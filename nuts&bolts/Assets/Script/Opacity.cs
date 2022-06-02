using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opacity : MonoBehaviour
{
    public GameObject solidObj;
    public GameObject transparentObj;

    private void Awake()
    {
        MakeSolid();
    }

    public void MakeTransparent()
    {
        transparentObj.SetActive(true);
        this.gameObject.SetActive(false);
    }
    
    public void MakeSolid()
    {
        this.gameObject.SetActive(true);
        transparentObj.SetActive(false);
    }
}
