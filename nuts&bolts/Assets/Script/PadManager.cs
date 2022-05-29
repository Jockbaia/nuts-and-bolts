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

    public void takeNumber(string number)
    {
        
        if (number == "cancel")
        {
            if(displayText.text.Length != 0 && displayText.text != "ERROR" && displayText.text != "CORRECT")
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
                    //fare qualcosa
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
                    //fare qualcosa
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
            else if(displayText.text.Length != 4)
            {
                displayText.text += number;
            }
        }
    }
}
