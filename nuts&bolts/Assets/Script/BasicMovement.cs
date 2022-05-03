using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stepSize = 1f;
    public Transform movePoint;
    public Transform mainCamera;

    public static Vector3 playerMovePointPos;
    public static Vector3 playerPos;

    void Start()
    {
        movePoint.parent = null;
        mainCamera.parent = null;
    }

    void Update()
    {
        playerMovePointPos = movePoint.position;
        playerPos = transform.position;

        if (CanStep((int)movePoint.position.x, (int)movePoint.position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            movePoint.position = transform.position;
        }
        

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.10f)
        {
            int xPos = (int) transform.position.x;
            int zPos = (int) transform.position.z;

            if (Input.GetKeyDown(KeyCode.W))
            {
                Vector3 vec = new Vector3(0, 0, stepSize);
                transform.rotation = Quaternion.LookRotation(vec);
                movePoint.position += vec;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Vector3 vec = new Vector3(-stepSize, 0, 0);
                transform.rotation = Quaternion.LookRotation(vec);
                movePoint.position += vec;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Vector3 vec = new Vector3(0, 0, -stepSize);
                transform.rotation = Quaternion.LookRotation(vec);
                movePoint.position += vec;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Vector3 vec = new Vector3(stepSize, 0, 0);
                transform.rotation = Quaternion.LookRotation(vec);
                movePoint.position += vec;
            }
        }
    }

    bool CanStep(int x, int z)
    {
        int zLen = GameManager.instance.room.Count;
        int xLen = GameManager.instance.room[0].Count;
        

        if ((x < xLen && x >= 0) && (z < zLen && z >= 0)) // check inside bounds
        {
            char cellValue = GameManager.instance.room[z][x];

            if (cellValue == '1')
            {
                return false;
            }


            return true;
        }

        return false;
    }
}
