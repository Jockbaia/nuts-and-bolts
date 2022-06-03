using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    public Transform movePoint;

    void Start()
    {
        movePoint = transform.Find("MovePoint");
        movePoint.parent = transform.parent;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }
}
