using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public GameObject upBtn;
    public GameObject downBtn;
    public GameObject leftBtn;
    public GameObject rightBtn;

    public GameObject pinToActivate;

    public Vector2 targetPos;

    bool solved = false;

    bool doItOnce = true;

    void Update()
    {
        // Check if it arrived to destination
        if (transform.position.x == targetPos.x && transform.position.z == targetPos.y)
        {
            if (!solved)
            {
                pinToActivate.SetActive(true);
                solved = true;
            }
            return;
        }

        bool upActive = upBtn.GetComponent<ButtonLogic>().isActive;
        bool downActive = downBtn.GetComponent<ButtonLogic>().isActive;
        bool leftActive = leftBtn.GetComponent<ButtonLogic>().isActive;
        bool rightActive = rightBtn.GetComponent<ButtonLogic>().isActive;

        if (upActive && IsFree(Vector3.forward))
        {
            if (doItOnce)
            {
                transform.position += Vector3.forward;
                doItOnce = false;
            }
        }
        else if (downActive && IsFree(Vector3.back))
        {
            if (doItOnce)
            {
                transform.position += Vector3.back;
                doItOnce = false;
            }
        }
        else if (leftActive && IsFree(Vector3.left))
        {
            if (doItOnce)
            {
                transform.position += Vector3.left;
                doItOnce = false;
            }
        }
        else if (rightActive && IsFree(Vector3.right))
        {
            if (doItOnce)
            {
                transform.position += Vector3.right;
                doItOnce = false;
            }
        }
        else
        {
            doItOnce = true;
        }
    }

    bool IsFree(Vector3 position)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position + position, 0.1f);
        foreach(var c in colls)
        {
            if (c.name.StartsWith("Wall")) return false;
            else if (c.name.StartsWith("Obstacle")) return false;
        }

        return true;
    }
}
