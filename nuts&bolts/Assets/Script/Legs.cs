using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    enum LegPos { None, Up, Down }

    [HideInInspector]
    public bool canMove = true;

    bool doItOnce = true;

    LegPos state = LegPos.None;

    Transform movePoint;
    Transform legs;
    Transform legsChild;

    void Start()
    {
        movePoint = GetComponent<PlayerLogic>().movePoint;
        legs = transform.Find("Model/Legs");
        legsChild = legs.transform.Find("Bottom");
    }

    void Update()
    {
        bool specialAction = GetComponent<PlayerLogic>().specialAction;
        
        if (specialAction)
        {
            if (!doItOnce) return;
            doItOnce = false;

            string powerSelected = GetComponent<RobotPowers>().selectedPower.ToString();

            if (powerSelected == "Up")
            {
                if (state == LegPos.None)
                {
                    canMove = false;
                    LegsUp();
                }
                else
                {
                    LegsReset();
                    canMove = true;
                }
            }
            else if (powerSelected == "Down")
            {
                if (state == LegPos.None)
                {
                    canMove = false;
                    LegsDown();
                }
                else
                {
                    LegsReset();
                    canMove = true;
                }
            }
            else if (powerSelected == "UpDownMove")
            {
                if (state == LegPos.None)
                {
                    LegsUp();
                }
                else if (state == LegPos.Up)
                {
                    LegsDown();
                }
                else if (state == LegPos.Down)
                {
                    LegsReset();
                }
            }
        }
        else
        {
            doItOnce = true;
        }
    }

    public void TookDamage()
    {
        LegsReset();
        canMove = true;
    }

    void LegsReset()
    {
        state = LegPos.None;
        legsChild.GetComponent<CapsuleCollider>().enabled = false;

        legsChild.transform.localPosition = Vector3.zero;
        movePoint.position = new Vector3(movePoint.position.x, 1f, movePoint.position.z);
    }

    void LegsUp()
    {
        state = LegPos.Up;
        legsChild.GetComponent<CapsuleCollider>().enabled = true;

        legsChild.transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);
        movePoint.position = new Vector3(movePoint.position.x, 1.25f, movePoint.position.z);
    }

    void LegsDown()
    {
        Vector3 newPos = new Vector3(transform.position.x, 0f, transform.position.z);
        if (state == LegPos.None)
        {
            newPos.y = 0.5f;
        }
        else if (state == LegPos.Up)
        {
            newPos.y = 0.75f;
        }

        state = LegPos.Down;
        legsChild.GetComponent<CapsuleCollider>().enabled = false;

        legsChild.transform.position = newPos;
        movePoint.position = new Vector3(movePoint.position.x, 0.5f, movePoint.position.z);
    }
}
