using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HandleNumpadNav : MonoBehaviour
{
    private string[,] padM = { { "1", "2", "3" },
                               { "4", "5", "6" },
                               { "7", "8", "9" },
                          { "delete", "0", "enter" }};

    private string[] DisHigh = { "1", "1" };

    // Vector2 navPad = Vector2.zero;

    public bool padOpen = false;

    PadManager correct;

    GameObject pad;
    private int i = 0;
    private int j = 0;


    float cooldown = 0f;

    private void Start()
    {

        pad = GameObject.Find(this.gameObject.name == "Player1" ? "PadCanvas_1" : "PadCanvas_2");
        correct = pad.GetComponent<PadManager>();
        pad.SetActive(false);
        correct.OnPassCorrect += Correct_OnPassCorrect;


    }

    private void Correct_OnPassCorrect(object sender, EventArgs e)
    {
        padOpen = false;
        pad.SetActive(false);
        correct.OnPassCorrect -= Correct_OnPassCorrect;

    }

    public void OnInteractNumpad(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;

        if (false && context.action.triggered) // TODO: add logic to check if player in Numpad cell --> otherwise: false
        {
            if (padOpen)
            {
                padOpen = false;
            }
            else
            {
                padOpen = true;
                UpdateButtonHighlight(DisHigh);
            }
            pad.SetActive(padOpen);
        }
    }

    public void OnNavigation(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;
        if (!padOpen) return;

        if (cooldown < 0.005)
        {
            cooldown += Time.deltaTime;
            return;
        }
        cooldown = 0;

        //navPad = context.ReadValue<Vector2>();
        // TODO: navigazione // W: (0, 1) A: (-1, 0) S: (0 -1) D: (1, 0)


        if (context.control.displayName == "w" || context.control.displayName == "i")
        {
            if (i == 0)
            {
                i = 3;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
            else
            {
                i--;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }

        }
        else if (context.control.displayName == "a" || context.control.displayName == "j")
        {
            if (j == 0)
            {
                j = 2;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
            else
            {
                j--;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
        }
        else if (context.control.displayName == "s" || context.control.displayName == "k")
        {
            if (i == 3)
            {
                i = 0;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);

            }
            else
            {
                i++;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
        }
        else if (context.control.displayName == "d" || context.control.displayName == "l")
        {
            if (j == 2)
            {
                j = 0;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
            else
            {
                j++;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
        }
        //navPad = Vector2.zero;

    }

    public void OnEnterNum(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;

        if (context.action.triggered && padOpen)
        {
            if (padM[i, j] == "delete")
                pad.GetComponent<PadManager>().takeNumber("cancel");
            else
                pad.GetComponent<PadManager>().takeNumber(padM[i, j]);
        }
    }

    void UpdateButtonHighlight(string[] array)
    {
        pad.transform.Find("pad").Find(array[0]).GetComponent<Button>().interactable = true;
        pad.transform.Find("pad").Find(array[1]).GetComponent<Button>().interactable = false;
    }



}
