using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLaser : MonoBehaviour
{
    public GameObject button;

    bool deactivated = false;

    void Update()
    {
        if (deactivated) return;

        if (button.GetComponent<ButtonLogic>() != null)
        {
            if (button.GetComponent<ButtonLogic>().isActive)
            {
                Deactivate();
            }

        }
        else if (button.GetComponent<WallBtnLogic>() != null)
        {
            if (button.GetComponent<WallBtnLogic>().isActive)
            {
                Deactivate();
            }
        }
    }

    void Deactivate()
    {
        deactivated = true;
        gameObject.SetActive(false);
        //GetComponent<Laser>().enabled = false;
    }
}
