using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovingOneShot : MonoBehaviour
{
    public Transform button;
    bool doItOnce = true;

    void Update()
    {
        if (!doItOnce) return;

        if (button.gameObject.GetComponent<WallBtnLogic>().isActive)
        {
            doItOnce = false;
            transform.Translate(new Vector3(0, -4.01f, 0));
        }
    }
}
