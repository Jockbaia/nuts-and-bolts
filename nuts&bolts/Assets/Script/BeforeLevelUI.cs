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

    private int NavIdxP1 = 0;
    private int NavIdxP2 = 0;

    private float cooldownP1 = 0f;
    private float cooldownP2 = 0f;

    public void OnWASD(InputAction.CallbackContext context)
    {
        if (cooldownP1 < 0.005)
        {
            cooldownP1 += Time.deltaTime;
            return;
        }
        cooldownP1 = 0;

        Vector2 WASD = context.ReadValue<Vector2>();

        if (WASD.y == 1)
        {
            if (NavIdxP1 == 0) return;
            NavIdxP1--;
            UpdateButtonHighlight();

        }
        else if (WASD.y == -1)
        {
            if (NavIdxP1 == 5) return;
            NavIdxP1++;
            UpdateButtonHighlight();
        }
        else if (WASD.x > 0 && NavIdxP1 == 5)
        {
            NavIdxP1++;
            UpdateButtonHighlight();
        }
        else if (WASD.x < 0 && NavIdxP1 == 6)
        {
            NavIdxP1--;
            UpdateButtonHighlight();
        }
    }    
    
    public void OnIJKL(InputAction.CallbackContext context)
    {
        if (cooldownP2 < 0.005)
        {
            cooldownP2 += Time.deltaTime;
            return;
        }
        cooldownP2 = 0;

        Vector2 IJKL = context.ReadValue<Vector2>();

        if (IJKL.y == 1)
        {
            if (NavIdxP2 == 0) return;
            NavIdxP2--;
            UpdateButtonHighlight();

        }
        else if (IJKL.y == -1)
        {
            if (NavIdxP2 == 5) return;
            NavIdxP2++;
            UpdateButtonHighlight();
        }
        else if (IJKL.x < 0 && NavIdxP2 == 5)
        {
            NavIdxP2++;
            UpdateButtonHighlight();
        }
        else if (IJKL.x > 0 && NavIdxP2 == 6)
        {
            NavIdxP2--;
            UpdateButtonHighlight();
        }
    }

    public void OnLShift(InputAction.CallbackContext context)
    {
        bool pressed = context.action.triggered;

        if (pressed)
        {
            if (NavIdxP1 >= 0 && NavIdxP1 <= 4)
            {
                AssignBolt(1, NavIdxP1);
            }
        }
    }    
    
    public void OnRShift(InputAction.CallbackContext context)
    {
        bool pressed = context.action.triggered;

        if (pressed)
        {
            if (NavIdxP2 >= 0 && NavIdxP2 <= 4)
            {
                AssignBolt(2, NavIdxP2);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetBolts();

        // Set First Highlighted button
        UpdateButtonHighlight();
    }

    void UpdateButtonHighlight()
    {
        // P1
        SetButtonHighlight(P1_UI, "LeftArm", NavIdxP1 == 0);
        SetButtonHighlight(P1_UI, "RightArm", NavIdxP1 == 1);
        SetButtonHighlight(P1_UI, "View", NavIdxP1 == 2);
        SetButtonHighlight(P1_UI, "Legs", NavIdxP1 == 3);
        SetButtonHighlight(P1_UI, "Back", NavIdxP1 == 4);

        P1_UI.Find("Ready").GetComponent<Button>().interactable = !(NavIdxP1 == 5);
        P1_UI.Find("Reset").GetComponent<Button>().interactable = !(NavIdxP1 == 6);

        // P2
        SetButtonHighlight(P2_UI, "LeftArm", NavIdxP2 == 0);
        SetButtonHighlight(P2_UI, "RightArm", NavIdxP2 == 1);
        SetButtonHighlight(P2_UI, "View", NavIdxP2 == 2);
        SetButtonHighlight(P2_UI, "Legs", NavIdxP2 == 3);
        SetButtonHighlight(P2_UI, "Back", NavIdxP2 == 4);

        P2_UI.Find("Ready").GetComponent<Button>().interactable = !(NavIdxP2 == 5);
        P2_UI.Find("Reset").GetComponent<Button>().interactable = !(NavIdxP2 == 6);
    }

    void SetButtonHighlight(Transform player, string bodyPart, bool highlighted)
    {
        player.Find(bodyPart).Find("NameBox").GetComponent<Button>().interactable = !highlighted;
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
        EnableUIElem(P1_UI, "LeftArm", 2, leftArmP1 >= 1);
        EnableUIElem(P1_UI, "LeftArm", 3, leftArmP1 >= 3);

        EnableUIElem(P1_UI, "RightArm", 1, rightArmP1 >= 1);
        EnableUIElem(P1_UI, "RightArm", 2, rightArmP1 >= 4);

        EnableUIElem(P1_UI, "View", 1, viewP1 >= 3);

        EnableUIElem(P1_UI, "Legs", 1, legsP1 >= 1);
        EnableUIElem(P1_UI, "Legs", 2, legsP1 >= 3);
        EnableUIElem(P1_UI, "Legs", 3, legsP1 >= 5);

        EnableUIElem(P1_UI, "Back", 1, backP1 >= 3);        
        
        /* P2 */
        EnableUIElem(P2_UI, "LeftArm", 2, leftArmP2 >= 1);
        EnableUIElem(P2_UI, "LeftArm", 3, leftArmP2 >= 3);
                   
        EnableUIElem(P2_UI, "RightArm", 1, rightArmP2 >= 1);
        EnableUIElem(P2_UI, "RightArm", 2, rightArmP2 >= 4);
                   
        EnableUIElem(P2_UI, "View", 1, viewP2 >= 3);
                   
        EnableUIElem(P2_UI, "Legs", 1, legsP2 >= 1);
        EnableUIElem(P2_UI, "Legs", 2, legsP2 >= 3);
        EnableUIElem(P2_UI, "Legs", 3, legsP2 >= 5);
                   
        EnableUIElem(P2_UI, "Back", 1, backP2 >= 3);
    }

    void EnableUIElem(Transform player, string bodyPart, int powerNr, bool interactable)
    {
        player.Find(bodyPart).Find("Unlock" + powerNr.ToString()).Find("UnlockBar").GetComponent<Button>().interactable = interactable;
        player.Find(bodyPart).Find("Unlock" + powerNr.ToString()).Find("Unlock").GetComponent<Button>().interactable = interactable;
        player.Find(bodyPart).Find("Power" + powerNr.ToString()).GetComponent<Button>().interactable = interactable;
    }

    void AssignBolt(int playerId, int bodyPart)
    {
        if (playerId == 1) // P1
        {
            if (boltsP1 == 0)
            {
                return;
            }

            if (bodyPart == 0)
            {
                if (leftArmP1 == 3)
                {
                    return;
                }

                boltsP1--;
                P1_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP1.ToString());
                leftArmP1++;
                P1_UI.Find("LeftArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEFT ARM: " + leftArmP1.ToString());
                UpdateUnlockedHighlight();
            }
            else if (bodyPart == 1)
            {
                if (rightArmP1 == 4)
                {
                    return;
                }

                boltsP1--;
                P1_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP1.ToString());
                rightArmP1++;
                P1_UI.Find("RightArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("RIGHT ARM: " + rightArmP1.ToString());
                UpdateUnlockedHighlight();
            }
            else if (bodyPart == 2)
            {
                if (viewP1 == 3)
                {
                    return;
                }

                boltsP1--;
                P1_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP1.ToString());
                viewP1++;
                P1_UI.Find("View").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("VIEW: " + viewP1.ToString());
                UpdateUnlockedHighlight();
            }
            else if (bodyPart == 3)
            {
                if (legsP1 == 5)
                {
                    return;
                }

                boltsP1--;
                P1_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP1.ToString());
                legsP1++;
                P1_UI.Find("Legs").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEGS: " + legsP1.ToString());
                UpdateUnlockedHighlight();
            }
            else if (bodyPart == 4)
            {
                if (backP1 == 3)
                {
                    return;
                }

                boltsP1--;
                P1_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP1.ToString());
                backP1++;
                P1_UI.Find("Back").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("BACK: " + backP1.ToString());
                UpdateUnlockedHighlight();
            }
        }
        else if (playerId == 2) // P2
        {
            if (boltsP2 == 0)
            {
                return;
            }

            if (bodyPart == 0)
            {
                if (leftArmP2 == 3)
                {
                    return;
                }

                boltsP2--;
                P2_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP2.ToString());
                leftArmP2++;
                P2_UI.Find("LeftArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEFT ARM: " + leftArmP2.ToString());
                UpdateUnlockedHighlight();
            }
            else if (bodyPart == 1)
            {
                if (rightArmP2 == 4)
                {
                    return;
                }

                boltsP2--;
                P2_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP2.ToString());
                rightArmP2++;
                P2_UI.Find("RightArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("RIGHT ARM: " + rightArmP2.ToString());
                UpdateUnlockedHighlight();
            }
            else if (bodyPart == 2)
            {
                if (viewP2 == 3)
                {
                    return;
                }

                boltsP2--;
                P2_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP2.ToString());
                viewP2++;
                P2_UI.Find("View").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("VIEW: " + viewP2.ToString());
                UpdateUnlockedHighlight();
            }
            else if (bodyPart == 3)
            {
                if (legsP2 == 5)
                {
                    return;
                }

                boltsP2--;
                P2_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP2.ToString());
                legsP2++;
                P2_UI.Find("Legs").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEGS: " + legsP2.ToString());
                UpdateUnlockedHighlight();
            }
            else if (bodyPart == 4)
            {
                if (backP2 == 3)
                {
                    return;
                }

                boltsP2--;
                P2_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP2.ToString());
                backP2++;
                P2_UI.Find("Back").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("BACK: " + backP2.ToString());
                UpdateUnlockedHighlight();
            }
        }
    }
}
