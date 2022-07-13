using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PadManager : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip clipOk;
    public AudioClip clipError;
    public AudioClip clipNavigate;

    public string password_1 = "1234";
    public string password_2 = "2341";

    [SerializeField]
    public TMP_Text displayText;

    public Animator animator;

    public event EventHandler OnPassCorrect;

    [HideInInspector]
    public bool isOpen1 = false;
    [HideInInspector]
    public bool isOpen2 = false;

    public void takeNumber(string number)
    {
        if (displayText.text == "OK") return;

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

                    isOpen1 = true;
                }
                else
                {
                    displayText.text = "ERROR";
                    audioSrc.PlayOneShot(clipError);
                }
            }
            else
            {
                if (displayText.text == password_2)
                {
                    displayText.text = "OK";
                    animator.SetBool("isOpen", true);

                    StartCoroutine("StopDoor");

                    isOpen2 = true;
                }
                else
                {
                    displayText.text = "ERROR";
                    audioSrc.PlayOneShot(clipError);
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

    public IEnumerator StopDoor()
    {
        OnPassCorrect?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isOpen", false);
    }
}