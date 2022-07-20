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
                transform.Translate(new Vector3(0, -100f, 0));
            }
        }
        else if (CanReset(bolts1, bolts2))
        {
            doItOnce = true;
            transform.position = initialPos;
        }
    }

    bool CanReset(int bolts1, int bolts2)
    {
        if (bolts1 != p1Amount || bolts2 != p2Amount)
        {
            var p1Z = GameObject.Find("Player1").transform.position.z;
            var p2Z = GameObject.Find("Player2").transform.position.z;

            if (p1Z < 7 && p2Z > 7) return false;
            if (p1Z > 7 && p2Z < 7) return false;
        }

        return true;
    }
}
