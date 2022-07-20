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
        else
        {
            doItOnce = true;

            var p1MP = GameObject.Find("Player1").GetComponent<PlayerLogic>().movePoint.transform;
            var p2MP = GameObject.Find("Player2").GetComponent<PlayerLogic>().movePoint.transform;
            var p1 = GameObject.Find("Player1").transform;
            var p2 = GameObject.Find("Player2").transform;

            if (p1.position.z <= 7 && p2.position.z >= 7)
            {
                p2.position = new Vector3(p2.position.x, p2.position.y, 0f);
                p2MP.position = new Vector3(p2MP.position.x, p2MP.position.y, 0f);
            }

            if (p1.position.z >= 7 && p2.position.z <= 7)
            { 
                p1.position = new Vector3(p1.position.x, p1.position.y, 0f);
                p1MP.position = new Vector3(p1MP.position.x, p1MP.position.y, 0f);
            }

            transform.position = initialPos;
        }
    }
}
