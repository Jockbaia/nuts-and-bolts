using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
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
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.1f && Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.1f)
            {
                return;
            }

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.1f)
            {
                movePoint.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
            }
        }
    }
}
