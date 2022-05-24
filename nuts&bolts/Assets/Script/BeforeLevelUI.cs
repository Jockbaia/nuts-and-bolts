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
    
    // Backup for "Reset" call
    int bk_boltsP1;
    int bk_leftArmP1;
    int bk_rightArmP1;
    int bk_viewP1;
    int bk_legsP1;
    int bk_backP1;    
    
    int bk_boltsP2;
    int bk_leftArmP2;
    int bk_rightArmP2;
    int bk_viewP2;
    int bk_legsP2;
    int bk_backP2;

    private int NavIdxP1 = 0;
    private int NavIdxP2 = 0;

    private float cooldownP1 = 0f;
    private float cooldownP2 = 0f;

    private bool readyModalOpenP1 = false;
    private bool readyModalOpenP2 = false;

    private bool resetModalOpenP1 = false;
    private bool resetModalOpenP2 = false;

    private int NavIdxModalP1 = 0;
    private int NavIdxModalP2 = 0;

    private bool P1ready = false;
    private bool P2ready = false;

    public void OnWASD(InputAction.CallbackContext context)
    {
        if (P1ready) return; 
        
        if (cooldownP1 < 0.005)
        {
            cooldownP1 += Time.deltaTime;
            return;
        }
        cooldownP1 = 0;

        Vector2 WASD = context.ReadValue<Vector2>();

        if (!(readyModalOpenP1 || resetModalOpenP1)) 
        {
            if (WASD.y == 1)
            {
                if (NavIdxP1 == 0 || NavIdxP1 > 5) return;
                NavIdxP1--;
                UpdateButtonHighlight();

            }
            else if (WASD.y == -1)
            {
                if (NavIdxP1 >= 5) return;
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
        else if (readyModalOpenP1)
        {
            if (WASD.x > 0)
            {
                if (NavIdxModalP1 == 1) return;
                NavIdxModalP1++;
                P1_UI.Find("ModalReady").Find("ButtonYes").GetComponent<Button>().interactable = true;
                P1_UI.Find("ModalReady").Find("ButtonNo").GetComponent<Button>().interactable = false;
            }
            else if (WASD.x < 0)
            {
                if (NavIdxModalP1 == 0) return;
                NavIdxModalP1--;
                P1_UI.Find("ModalReady").Find("ButtonYes").GetComponent<Button>().interactable = false;
                P1_UI.Find("ModalReady").Find("ButtonNo").GetComponent<Button>().interactable = true;
            }
        }        
        else if (resetModalOpenP1)
        {
            if (WASD.x > 0)
            {
                if (NavIdxModalP1 == 1) return;
                NavIdxModalP1++;
                P1_UI.Find("ModalReset").Find("ButtonYes").GetComponent<Button>().interactable = true;
                P1_UI.Find("ModalReset").Find("ButtonNo").GetComponent<Button>().interactable = false;
            }
            else if (WASD.x < 0)
            {
                if (NavIdxModalP1 == 0) return;
                NavIdxModalP1--;
                P1_UI.Find("ModalReset").Find("ButtonYes").GetComponent<Button>().interactable = false;
                P1_UI.Find("ModalReset").Find("ButtonNo").GetComponent<Button>().interactable = true;
            }
        }
    }    
    
    public void OnIJKL(InputAction.CallbackContext context)
    {
        if (P2ready) return;

        if (cooldownP2 < 0.005)
        {
            cooldownP2 += Time.deltaTime;
            return;
        }
        cooldownP2 = 0;

        Vector2 IJKL = context.ReadValue<Vector2>();

        if (!(readyModalOpenP2 || resetModalOpenP2))
        {
            if (IJKL.y == 1)
            {
                if (NavIdxP2 == 0 || NavIdxP2 > 5) return;
                NavIdxP2--;
                UpdateButtonHighlight();

            }
            else if (IJKL.y == -1)
            {
                if (NavIdxP2 >= 5) return;
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
        else if (readyModalOpenP2)
        {
            if (IJKL.x > 0)
            {
                if (NavIdxModalP2 == 1) return;
                NavIdxModalP2++;
                P2_UI.Find("ModalReady").Find("ButtonYes").GetComponent<Button>().interactable = true;
                P2_UI.Find("ModalReady").Find("ButtonNo").GetComponent<Button>().interactable = false;
            }
            else if (IJKL.x < 0)
            {
                if (NavIdxModalP2 == 0) return;
                NavIdxModalP2--;
                P2_UI.Find("ModalReady").Find("ButtonYes").GetComponent<Button>().interactable = false;
                P2_UI.Find("ModalReady").Find("ButtonNo").GetComponent<Button>().interactable = true;
            }
        }
        else if (resetModalOpenP2)
        {
            if (IJKL.x > 0)
            {
                if (NavIdxModalP2 == 1) return;
                NavIdxModalP2++;
                P2_UI.Find("ModalReset").Find("ButtonYes").GetComponent<Button>().interactable = true;
                P2_UI.Find("ModalReset").Find("ButtonNo").GetComponent<Button>().interactable = false;
            }
            else if (IJKL.x < 0)
            {
                if (NavIdxModalP2 == 0) return;
                NavIdxModalP2--;
                P2_UI.Find("ModalReset").Find("ButtonYes").GetComponent<Button>().interactable = false;
                P2_UI.Find("ModalReset").Find("ButtonNo").GetComponent<Button>().interactable = true;
            }
        }
    }

    public void OnLShift(InputAction.CallbackContext context)
    {
        if (P1ready) return;

        bool pressed = context.action.triggered;

        if (pressed)
        {
            if (NavIdxP1 >= 0 && NavIdxP1 <= 4)
            {
                AssignBolt(1, NavIdxP1);
            }
            else if (NavIdxP1 == 5 && !readyModalOpenP1)
            {
                readyModalOpenP1 = true;
                P1_UI.Find("ModalReady").gameObject.SetActive(true);
                P1_UI.Find("ModalReady").Find("ButtonYes").GetComponent<Button>().interactable = false;
                P1_UI.Find("ModalReady").Find("ButtonNo").GetComponent<Button>().interactable = true;
                NavIdxModalP1 = 0;
            }
            else if (NavIdxP1 == 6 && !resetModalOpenP1)
            {
                resetModalOpenP1 = true;
                P1_UI.Find("ModalReset").gameObject.SetActive(true);
                P1_UI.Find("ModalReset").Find("ButtonYes").GetComponent<Button>().interactable = false;
                P1_UI.Find("ModalReset").Find("ButtonNo").GetComponent<Button>().interactable = true;
                NavIdxModalP1 = 0;
            }
            else if (readyModalOpenP1)
            {
                if (NavIdxModalP1 == 0) // Ready -> Yes
                {
                    P1ready = true;
                    readyModalOpenP1 = false;
                    P1_UI.Find("ModalReady").gameObject.SetActive(false);
                    P1_UI.Find("ReadyLogo").gameObject.SetActive(true);

                    if (P1ready && P2ready)
                    {
                        AllPlayersReady();
                    }
                }
                else if (NavIdxModalP1 == 1) // Ready -> No
                {
                    readyModalOpenP1 = false;
                    P1_UI.Find("ModalReady").gameObject.SetActive(false);
                }
            }
            else if (resetModalOpenP1)
            {
                if (NavIdxModalP1 == 0) // Reset -> Yes
                {
                    boltsP1 = bk_boltsP1;
                    leftArmP1 = bk_leftArmP1;
                    rightArmP1 = bk_rightArmP1;
                    viewP1 = bk_viewP1;
                    legsP1 = bk_legsP1;
                    backP1 = bk_backP1;

                    // Set current Bolts P1
                    P1_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP1.ToString());
                    P1_UI.Find("LeftArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEFT ARM: " + leftArmP1.ToString());
                    P1_UI.Find("RightArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("RIGHT ARM: " + rightArmP1.ToString());
                    P1_UI.Find("View").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("VIEW: " + viewP1.ToString());
                    P1_UI.Find("Legs").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEGS: " + legsP1.ToString());
                    P1_UI.Find("Back").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("BACK: " + backP1.ToString());

                    NavIdxP1 = 0;

                    UpdateUnlockedHighlight();
                    UpdateButtonHighlight();

                    resetModalOpenP1 = false;
                    P1_UI.Find("ModalReset").gameObject.SetActive(false);
                }
                else if (NavIdxModalP1 == 1) // Reset -> No
                {
                    resetModalOpenP1 = false;
                    P1_UI.Find("ModalReset").gameObject.SetActive(false);
                }
            }
        }
    }    
    
    public void OnRShift(InputAction.CallbackContext context)
    {
        if (P2ready) return;

        bool pressed = context.action.triggered;

        if (pressed)
        {
            if (NavIdxP2 >= 0 && NavIdxP2 <= 4)
            {
                AssignBolt(2, NavIdxP2);
            }
            else if (NavIdxP2 == 5 && !readyModalOpenP2)
            {
                readyModalOpenP2 = true;
                P2_UI.Find("ModalReady").gameObject.SetActive(true);
                P2_UI.Find("ModalReady").Find("ButtonYes").GetComponent<Button>().interactable = false;
                P2_UI.Find("ModalReady").Find("ButtonNo").GetComponent<Button>().interactable = true;
                NavIdxModalP2 = 0;
            }
            else if (NavIdxP2 == 6 && !resetModalOpenP2)
            {
                resetModalOpenP2 = true;
                P2_UI.Find("ModalReset").gameObject.SetActive(true);
                P2_UI.Find("ModalReset").Find("ButtonYes").GetComponent<Button>().interactable = false;
                P2_UI.Find("ModalReset").Find("ButtonNo").GetComponent<Button>().interactable = true;
                NavIdxModalP2 = 0;
            }
            else if (readyModalOpenP2)
            {
                if (NavIdxModalP2 == 0) // Ready -> Yes
                {
                    P2ready = true;
                    readyModalOpenP2 = false;
                    P2_UI.Find("ModalReady").gameObject.SetActive(false);
                    P2_UI.Find("ReadyLogo").gameObject.SetActive(true);

                    if (P1ready && P2ready)
                    {
                        AllPlayersReady();
                    }
                }
                else if (NavIdxModalP2 == 1) // Ready -> No
                {
                    readyModalOpenP2 = false;
                    P2_UI.Find("ModalReady").gameObject.SetActive(false);
                }
            }
            else if (resetModalOpenP2)
            {
                if (NavIdxModalP2 == 0) // Reset -> Yes
                {
                    boltsP2 = bk_boltsP2;
                    leftArmP2 = bk_leftArmP2;
                    rightArmP2 = bk_rightArmP2;
                    viewP2 = bk_viewP2;
                    legsP2 = bk_legsP2;
                    backP2 = bk_backP2;

                    // Set current Bolts P2
                    P2_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP2.ToString());
                    P2_UI.Find("LeftArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEFT ARM: " + leftArmP2.ToString());
                    P2_UI.Find("RightArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("RIGHT ARM: " + rightArmP2.ToString());
                    P2_UI.Find("View").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("VIEW: " + viewP2.ToString());
                    P2_UI.Find("Legs").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEGS: " + legsP2.ToString());
                    P2_UI.Find("Back").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("BACK: " + backP2.ToString());

                    NavIdxP2 = 0;

                    UpdateUnlockedHighlight();
                    UpdateButtonHighlight();

                    resetModalOpenP2 = false;
                    P2_UI.Find("ModalReset").gameObject.SetActive(false);
                }
                else if (NavIdxModalP2 == 1) // Reset -> No
                {
                    resetModalOpenP2 = false;
                    P2_UI.Find("ModalReset").gameObject.SetActive(false);
                }
            }
        }
    }

    void AllPlayersReady()
    {
        //TODO: pass bolts state to next scene and transition to next level...
        Debug.Log("All players are ready! TODO!!");
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO: obtain it from StateManager script (RobotPowers for P1 and P2)
        Debug.Log("Bolts acquired! TODO!!");
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
        ////

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
        bk_boltsP1 = boltsP1;
        bk_leftArmP1 = leftArmP1;
        bk_rightArmP1 = rightArmP1;
        bk_viewP1 = viewP1;
        bk_legsP1 = legsP1;
        bk_backP1 = backP1;

        bk_boltsP2 = boltsP2;
        bk_leftArmP2 = leftArmP2;
        bk_rightArmP2 = rightArmP2;
        bk_viewP2 = viewP2;
        bk_legsP2 = legsP2;
        bk_backP2 = backP2;

        // Set current Bolts P1
        P1_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP1.ToString());
        P1_UI.Find("LeftArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEFT ARM: " + leftArmP1.ToString());
        P1_UI.Find("RightArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("RIGHT ARM: " + rightArmP1.ToString());
        P1_UI.Find("View").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("VIEW: " + viewP1.ToString());
        P1_UI.Find("Legs").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEGS: " + legsP1.ToString());
        P1_UI.Find("Back").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("BACK: " + backP1.ToString());

        NavIdxP1 = 0;

        // Set current Bolts P2
        P2_UI.Find("Bolts").Find("Text").GetComponent<TMP_Text>().SetText("BOLTS: " + boltsP2.ToString());
        P2_UI.Find("LeftArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEFT ARM: " + leftArmP2.ToString());
        P2_UI.Find("RightArm").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("RIGHT ARM: " + rightArmP2.ToString());
        P2_UI.Find("View").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("VIEW: " + viewP2.ToString());
        P2_UI.Find("Legs").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("LEGS: " + legsP2.ToString());
        P2_UI.Find("Back").Find("NameBox").Find("Text").GetComponent<TMP_Text>().SetText("BACK: " + backP2.ToString());

        NavIdxP2 = 0;

        UpdateUnlockedHighlight();
    }

    void UpdateUnlockedHighlight()
    {
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
