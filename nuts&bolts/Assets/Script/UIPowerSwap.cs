using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPowerSwap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RobotPowers.PowerSwitched += HandlePowerSwitched;
    }

    private void OnDestroy()
    {
        RobotPowers.PowerSwitched -= HandlePowerSwitched;
    }

    static void HandlePowerSwitched(object sender, EventArgs e)
    {
        Debug.Log("Switched");
    }
}
