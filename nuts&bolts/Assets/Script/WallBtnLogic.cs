using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBtnLogic : MonoBehaviour
{
    public bool isActive = false;
    bool playOnce = true;

    void Update()
    {
        if (IsPressed())
        {
            isActive = true;

            if (playOnce)
            {
                var clip = GameObject.Find("Player1").GetComponent<PlayerLogic>().clipActiveBtn;
                GameObject.Find("Player1").GetComponent<AudioSource>().PlayOneShot(clip);
                playOnce = false;
            }
        }
        else
        {
            isActive = false;
            playOnce = true;
        }
    }

    bool IsPressed()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, 0.5f);
        foreach(var c in coll)
        {
            if (c.name == "Bottom") return true;
        }

        return false;
    }
}
