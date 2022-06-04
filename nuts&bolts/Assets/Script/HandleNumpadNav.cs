using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandleNumpadNav : MonoBehaviour
{
    public bool padOpen = false;

    Vector2 navPos = Vector2.zero;

    GameObject pad;

    float cooldown = 0f;

    private void Start()
    {
        pad = GameObject.Find(this.gameObject.name == "Player1" ? "PadCanvas_1" : "PadCanvas_2");
        pad.SetActive(false);
    }

    public void OnInteractNumpad(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;

        if (context.action.triggered && false) // TODO: add logic to check if player in Numpad cell --> otherwise: false
        {
            if (padOpen)
            {
                padOpen = false;
            }
            else
            {
                padOpen = true;
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

        navPos = context.ReadValue<Vector2>();

        // TODO: navigazione // W: (0, 1) A: (-1, 0) S: (0 -1) D: (1, 0)

    }

    public void OnEnterNum(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;

        if (context.action.triggered && padOpen)
        {
            pad.GetComponent<PadManager>().takeNumber(null /* TODO */);
        }
    }
}
