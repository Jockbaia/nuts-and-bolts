using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class XRayView : MonoBehaviour
{
    public RenderObjects xray1;
    public RenderObjects xray2;

    // Start is called before the first frame update
    void Start()
    {      
        xray1.SetActive(false);
        xray2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {      
        if (this.name == "Player1")
        {
            if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "Xray")
            {
                xray1.SetActive(true);
            }
            else
            {
                xray1.SetActive(false);
            }
        }
        else if (this.name == "Player2")
        {
            if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "Xray")
            {
                xray2.SetActive(true);
            }
            else
            {
                xray2.SetActive(false);
            }
        }
    }
}
