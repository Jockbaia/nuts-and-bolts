using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoving_PBolts : MonoBehaviour
{
    public int p1Amount;
    public int p2Amount;

    bool doItOnce = true;

    Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        int bolts1 = GameObject.Find("Player1").GetComponent<RobotPowers>()._components.bolts;
        int bolts2 = GameObject.Find("Player2").GetComponent<RobotPowers>()._components.bolts;

        if (bolts1 == p1Amount && bolts2 == p2Amount)
        {
            if (doItOnce)
            {
                doItOnce = false;
                transform.Translate(new Vector3(0, -4.01f, 0));
            }
        }
        else
        {
            doItOnce = true;
            transform.position = initialPos;
        }
    }
}
