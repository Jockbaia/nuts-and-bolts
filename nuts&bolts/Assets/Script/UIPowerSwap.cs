using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPowerSwap : MonoBehaviour
{
    public Transform BoltsP1;
    public Transform BoltsP2;

    void Start()
    {
        RobotPowers.PowerSwitched += HandlePowerSwitched;
    }

    private void OnDestroy()
    {
        RobotPowers.PowerSwitched -= HandlePowerSwitched;
    }

    private void Update()
    {
        int p1Bolts = GameObject.Find("Player1").GetComponent<RobotPowers>()._components.bolts;
        int p2Bolts = GameObject.Find("Player2").GetComponent<RobotPowers>()._components.bolts;

        BoltsP1.Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + p1Bolts.ToString());
        BoltsP2.Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + p2Bolts.ToString());
    }

    void HandlePowerSwitched(object sender, EventArgs e)
    {
        string player;
        Transform powerContainer;

        if (sender.ToString().StartsWith("Player1"))
        {
            player = "Player1";
            powerContainer = transform.Find("PowerP1");
        }
        else if (sender.ToString().StartsWith("Player2"))
        {
            player = "Player2";
            powerContainer = transform.Find("PowerP2");
        }
        else
        {
            throw new Exception("UIPowerSwap sender invalid!");
        }

        RobotPowers.PowerSelector selectedPower = GameObject.Find(player).GetComponent<RobotPowers>().selectedPower;

        powerContainer.Find("PushPull").gameObject.SetActive(false);
        powerContainer.Find("ArmExtend").gameObject.SetActive(false);
        powerContainer.Find("PushPullHeavy").gameObject.SetActive(false);
        powerContainer.Find("Magnetic").gameObject.SetActive(false);
        powerContainer.Find("DestroyPad").gameObject.SetActive(false);
        powerContainer.Find("Xray").gameObject.SetActive(false);
        powerContainer.Find("Up").gameObject.SetActive(false);
        powerContainer.Find("Down").gameObject.SetActive(false);
        powerContainer.Find("UpDownMove").gameObject.SetActive(false);
        powerContainer.Find("Rocket").gameObject.SetActive(false);

        powerContainer.Find(selectedPower.ToString()).gameObject.SetActive(true);
    }
}
