using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    private float moveSpeed = 5f;

    private Vector2 movementInput = Vector2.zero;
    private bool specialAction = false;

    private Transform movePoint;

    private void Start()
    {
        movePoint = transform.Find("Player Move Point");
        movePoint.parent = null;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnSpecialAction(InputAction.CallbackContext context)
    {
        specialAction = context.action.triggered;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(movePoint.position, transform.position) == 0f)
        {
            if (movementInput.y > 0.9f)
            {
                movePoint.position += new Vector3(0f, 0f, 1f);
            }
            else if (movementInput.y < -0.9f)
            {
                movePoint.position += new Vector3(0f, 0f, -1f);
            }
            else if (movementInput.x > 0.9f)
            {
                movePoint.position += new Vector3(1f, 0f, 0f);
            }
            else if (movementInput.x < -0.9f)
            {
                movePoint.position += new Vector3(-1f, 0f, 0f);
            }
        }
    }
}
