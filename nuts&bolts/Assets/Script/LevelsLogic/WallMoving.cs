using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoving : MonoBehaviour
{
    public Transform button;
    bool doItOnce = true;

    Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        if (button.GetComponent<ButtonLogic>().isActive)
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
