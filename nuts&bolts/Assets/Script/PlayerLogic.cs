using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    public Vector2 movementInput = Vector2.zero;
    public bool specialAction = false;

    public Transform movePoint;

    private bool draggingBox = false;

    private void Start()
    {
        movePoint = transform.Find("Player Move Point");
        movePoint.parent = null;

        var cam = transform.Find("Player Camera");
        cam.parent = null;
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

        if (Vector3.Distance(movePoint.position, transform.position) == 0f) //TODO: prevent out-of-bounds
        {
            CheckCollisions();

            if (movementInput.y > 0.9f)
            {
                Vector3 vec = new Vector3(0f, 0f, 1f);
                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0)
                {
                    movePoint.position += vec;
                }
                
                if (!specialAction) transform.rotation = Quaternion.LookRotation(vec);
            }
            else if (movementInput.y < -0.9f)
            {
                Vector3 vec = new Vector3(0f, 0f, -1f);
                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0)
                {
                    movePoint.position += vec;
                }

                if (!specialAction) transform.rotation = Quaternion.LookRotation(vec);
            }
            else if (movementInput.x > 0.9f)
            {
                Vector3 vec = new Vector3(1f, 0f, 0f);
                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0)
                {
                    movePoint.position += vec;
                }

                if (!specialAction) transform.rotation = Quaternion.LookRotation(vec);
            }
            else if (movementInput.x < -0.9f)
            {
                Vector3 vec = new Vector3(-1f, 0f, 0f);
                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0)
                {
                    movePoint.position += vec;
                }
                
                if (!specialAction) transform.rotation = Quaternion.LookRotation(vec);
                // TODO: change here instead of !specialAction to "grab state"
            }
        }
    }

    void CheckCollisions()
    {
        //TODO: implement collisions to check if in "grab state"
    }
}
