using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovingMulti : MonoBehaviour
{
    public Transform button1;
    public Transform button2;
    bool doItOnce = true;

    Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        if (button1.GetComponent<ButtonLogic>().isActive && button2.GetComponent<ButtonLogic>().isActive)
        {
            if (doItOnce)
            {
                doItOnce = false;
                transform.Translate(new Vector3(0, -100f, 0));
            }
        }
        else
        {
            doItOnce = true;
            transform.position = initialPos;
        }
    }
}
