using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovingExch : MonoBehaviour
{
    public GameObject wallButton;
    public GameObject vendingMachine;

    bool active = false;

    void Update()
    {
        if (!active && IsEngaged())
        {
            active = true;
            transform.Translate(new Vector3(0, -100f, 0));
        }
    }

    bool IsEngaged()
    {
        return wallButton.GetComponent<WallBtnLogic>().isActive || vendingMachine.GetComponent<BoltExchanger>().sent;
    }
}
