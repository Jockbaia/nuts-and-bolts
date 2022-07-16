using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    public enum LegPos { None, Up, Down }

    [HideInInspector]
    public bool canMove = true;
    public LegPos state = LegPos.None;
    public static float upValue = 0.25f;
    public static float dwValue = 0.5f;

    bool doItOnce = true;

    Transform movePoint;
    Transform legs;
    Transform legsChild;

    CapsuleCollider capsule;

    AudioSource asrc;
    AudioClip clip;

    void Start()
    {
        movePoint = GetComponent<PlayerLogic>().movePoint;
        legs = transform.Find("Model/Legs");
        legsChild = legs.transform.Find("Bottom");
        capsule = GetComponent<CapsuleCollider>();

        asrc = GetComponent<PlayerLogic>().audioSrc;
        clip = GetComponent<PlayerLogic>().clipExtendLegs;
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
                asrc.PlayOneShot(clip);
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
                asrc.PlayOneShot(clip);
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
                asrc.PlayOneShot(clip);
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

        legsChild.transform.localPosition = Vector3.zero;
        movePoint.position = new Vector3(movePoint.position.x, 1f, movePoint.position.z);

        capsule.height = 2f;
        capsule.center = Vector3.zero;
    }

    void LegsUp()
    {
        state = LegPos.Up;

        legsChild.transform.position = new Vector3(transform.position.x, -upValue, transform.position.z);
        movePoint.position = new Vector3(movePoint.position.x, 1f + upValue, movePoint.position.z);

        capsule.height = 2.25f;
        capsule.center = new Vector3(0f, -0.25f, 0f);
    }

    void LegsDown()
    {
        Vector3 newPos = new Vector3(transform.position.x, 0f, transform.position.z);
        if (state == LegPos.None)
        {
            newPos.y = dwValue;
        }
        else if (state == LegPos.Up)
        {
            newPos.y = dwValue + upValue;
        }

        state = LegPos.Down;

        legsChild.transform.position = newPos;
        movePoint.position = new Vector3(movePoint.position.x, dwValue, movePoint.position.z);

        capsule.height = 1.5f;
        capsule.center = new Vector3(0f, 0.25f, 0f);
    }
}
