using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class BeforeLevelUI : MonoBehaviour
{
    public Transform P1_UI;
    public Transform P2_UI;

    int boltsP1;
    int leftArmP1;
    int rightArmP1;
    int viewP1;
    int legsP1;
    int backP1;    
    
    int boltsP2;
    int leftArmP2;
    int rightArmP2;
    int viewP2;
    int legsP2;
    int backP2;

    public void OnWASD(InputAction.CallbackContext context)
    {
        Vector2 WASD = context.ReadValue<Vector2>();

        if (WASD.y > 0)
        {
           
        }
        else if (WASD.y < 0)
        {
            
        }
    }    
    
    public void OnIJKL(InputAction.CallbackContext context)
    {
        Vector2 IJKL = context.ReadValue<Vector2>();

        if (IJKL.y > 0)
        {
            
        }
        else if (IJKL.y < 0)
        {
            
        }
    }

    public void OnLShift(InputAction.CallbackContext context)
    {
        bool pressed = context.action.triggered;

        if (pressed)
        {

        }
    }    
    
    public void OnRShift(InputAction.CallbackContext context)
    {
        bool pressed = context.action.triggered;

        if (pressed)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetBolts();
    }

    void ResetBolts()
    {
        //TODO: obtain it from StateManager script (RobotPowers for P1 and P2)
        boltsP1 = 5;
        leftArmP1 = 2;
        rightArmP1 = 1;
        viewP1 = 0;
        legsP1 = 4;
        backP1 = 0;

        boltsP2 = 3;
        leftArmP2 = 0;
        rightArmP2 = 2;
        viewP2 = 3;
        legsP2 = 5;
        backP2 = 0;

        // TODO: save this initial state when pushing "Reset"
        ////

        // Set current Bolts P1
        P1_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP1.ToString());
        P1_UI.Find("LeftArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEFT ARM: " + leftArmP1.ToString());
        P1_UI.Find("RightArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("RIGHT ARM: " + rightArmP1.ToString());
        P1_UI.Find("View").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("VIEW: " + viewP1.ToString());
        P1_UI.Find("Legs").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEGS: " + legsP1.ToString());
        P1_UI.Find("Back").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("BACK: " + backP1.ToString());

        // Set current Bolts P2
        P2_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP2.ToString());
        P2_UI.Find("LeftArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEFT ARM: " + leftArmP2.ToString());
        P2_UI.Find("RightArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("RIGHT ARM: " + rightArmP2.ToString());
        P2_UI.Find("View").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("VIEW: " + viewP2.ToString());
        P2_UI.Find("Legs").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEGS: " + legsP2.ToString());
        P2_UI.Find("Back").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("BACK: " + backP2.ToString());

        UpdateUnlockedHighlight();
    }

    void UpdateUnlockedHighlight()
    {
        // TODO: obtain from RobotPowers.cs for P1 and P2
        /* P1 */
        PowerLock(P1_UI, "LeftArm", 2, leftArmP1 >= 1);
        PowerLock(P1_UI, "LeftArm", 3, leftArmP1 >= 3);

        PowerLock(P1_UI, "RightArm", 1, rightArmP1 >= 1);
        PowerLock(P1_UI, "RightArm", 2, rightArmP1 >= 4);

        PowerLock(P1_UI, "View", 1, viewP1 >= 3);

        PowerLock(P1_UI, "Legs", 1, legsP1 >= 1);
        PowerLock(P1_UI, "Legs", 2, legsP1 >= 3);
        PowerLock(P1_UI, "Legs", 3, legsP1 >= 5);

        PowerLock(P1_UI, "Back", 1, backP1 >= 3);
    }

    void PowerLock(Transform player, string bodyPart, int powerNr, bool interactable)
    {
        player.Find(bodyPart).Find("Unlock" + powerNr.ToString()).Find("UnlockBar").GetComponent<Button>().interactable = interactable;
        player.Find(bodyPart).Find("Unlock" + powerNr.ToString()).Find("Unlock").GetComponent<Button>().interactable = interactable;
        player.Find(bodyPart).Find("Power" + powerNr.ToString()).GetComponent<Button>().interactable = interactable;
    }

    void AssignBolt(Transform player, string bodyPart, string bodyPartTxt)
    {
        
    }
}
