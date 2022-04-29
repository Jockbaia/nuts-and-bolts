using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stepSize = 1f;
    public Transform movePoint;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {

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
}
