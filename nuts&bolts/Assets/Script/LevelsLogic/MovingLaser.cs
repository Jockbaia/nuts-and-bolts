using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
{
    public Transform movePoint;

    public float moveSpeed = 5f;

    int z = 4;
    bool goingDown = true;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) < 0.01f)
        {
            if (goingDown)
            {
                z--;
                if (z == 0) goingDown = false;

                movePoint.position = new Vector3(movePoint.position.x, movePoint.position.y, z);
            }
            else
            {
                z++;
                if (z == 4) goingDown = true;

                movePoint.position = new Vector3(movePoint.position.x, movePoint.position.y, z);
            }
        }
    }
}
