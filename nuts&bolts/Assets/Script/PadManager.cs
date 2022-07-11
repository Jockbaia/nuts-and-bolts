using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PadManager : MonoBehaviour
{
    public string password_1 = "1234";
    public string password_2 = "2341";

    [SerializeField]
    private TMP_Text displayText;

    public Animator animator;

    public event EventHandler OnPassCorrect;

    public void takeNumber(string number)
    {

        if (number == "cancel")
        {
            if (displayText.text.Length != 0 && displayText.text != "ERROR" && displayText.text != "CORRECT")
            {
                string c = displayText.text.Remove(displayText.text.Length - 1);
                displayText.text = c;
            }
        }
        else if (number == "enter")
        {
            if (this.gameObject.name == "PadCanvas_1")
            {
                if (displayText.text == password_1)
                {
                    displayText.text = "OK";
                    animator.SetBool("isOpen", true);
                    StartCoroutine("StopDoor");
                }
                else
                {
                    displayText.text = "ERROR";
                }
            }
            else
            {
                if (displayText.text == password_2)
                {
                    displayText.text = "OK";
                    animator.SetBool("isOpen", true);
                    StartCoroutine("StopDoor");
                }
                else
                {
                    displayText.text = "ERROR";
                }
            }
        }
        else
        {
            if (displayText.text == "ERROR" || displayText.text == "CORRECT")
            {
                displayText.text = number;
            }
            else if (displayText.text.Length != 4)
            {
                displayText.text += number;
            }
        }
    }

    IEnumerator StopDoor()
    {
        OnPassCorrect?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("isOpen", false);

    }
}