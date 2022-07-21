using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
{
    public Transform movePoint;

    public int startingX;
    public int endingX;

    public float moveSpeed = 5f;

    int x;
    bool goingLeft;

    void Start() // Moves sideways [-X, +X]
    {
        transform.position = new Vector3(startingX, transform.position.y, transform.position.z);

        if (startingX > endingX)
        {
            goingLeft = true;
        }
        else
        {
            goingLeft = false;
        }

        x = startingX;

        movePoint.position = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) < 0.01f)
        {
            if (goingLeft)
            {
                x--;
                if (x == endingX || x == startingX) goingLeft = false;

                movePoint.position = new Vector3(x, movePoint.position.y, movePoint.position.z);
            }
            else
            {
                x++;
                if (x == endingX || x == startingX) goingLeft = true;

                movePoint.position = new Vector3(x, movePoint.position.y, movePoint.position.z);
            }
        }
    }
}
