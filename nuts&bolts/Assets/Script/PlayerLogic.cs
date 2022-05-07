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

    private int mapZlen = 0;
    private int mapXlen = 0;
    private int mapXoffset = 0;

    public enum PowerSelector
    {
        PushPullBox
    }
    public PowerSelector selectedPower = PowerSelector.PushPullBox;

    private void Start()
    {
        movePoint = transform.Find("Player Move Point");
        movePoint.parent = null;

        var cam = transform.Find("Player Camera");
        cam.parent = null;

        (mapZlen, mapXlen, mapXoffset) = GetMapBounds();
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
            { // Move Up
                Vector3 vec = new Vector3(0f, 0f, 1f);

                if (!IsGrabbing()) transform.rotation = Quaternion.LookRotation(vec);

                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0
                    && transform.position.z < mapZlen - 1)
                {
                    movePoint.position += vec;
                }
            }
            else if (movementInput.y < -0.9)
            { // Move Down
                Vector3 vec = new Vector3(0f, 0f, -1f);

                if (!IsGrabbing()) transform.rotation = Quaternion.LookRotation(vec);

                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0
                    && transform.position.z > 0)
                {
                    movePoint.position += vec;
                }
            }
            else if (movementInput.x > 0.9f)
            { // Move Right
                Vector3 vec = new Vector3(1f, 0f, 0f);

                if (!IsGrabbing()) transform.rotation = Quaternion.LookRotation(vec);

                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0
                    && transform.position.x < mapXlen + mapXoffset - 1)
                {
                    movePoint.position += vec;
                }
            }
            else if (movementInput.x < -0.9f)
            { // Move Left
                Vector3 vec = new Vector3(-1f, 0f, 0f);

                if (!IsGrabbing()) transform.rotation = Quaternion.LookRotation(vec);

                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0
                     && transform.position.x > mapXoffset)
                {
                    movePoint.position += vec;
                }
            }
        }
    }

    bool IsGrabbing() // Checks if player is grabbing a box
    {
        if (selectedPower == PowerSelector.PushPullBox && specialAction)
        {
            float rotation = transform.rotation.eulerAngles.y;

            if (rotation == 0f)
            {
                Collider[] coll = Physics.OverlapSphere(transform.position + new Vector3(0, 0, 1), 0.01f);
                if (coll.Length == 1 && coll[0].name.StartsWith("TallBox") && movementInput.y < -0.9f)
                {
                    return true;
                }
            }
            else if (rotation == 90f)
            {
                Collider[] coll = Physics.OverlapSphere(transform.position + new Vector3(1, 0, 0), 0.01f);
                if (coll.Length == 1 && coll[0].name.StartsWith("TallBox") && movementInput.x < -0.9f)
                {
                    return true;
                }
            }
            else if (rotation == 180f)
            {
                Collider[] coll = Physics.OverlapSphere(transform.position + new Vector3(0, 0, -1), 0.01f);
                if (coll.Length == 1 && coll[0].name.StartsWith("TallBox") && movementInput.y > 0.9f)
                {
                    return true;
                }
            }
            else if (rotation == 270f)
            {
                Collider[] coll = Physics.OverlapSphere(transform.position + new Vector3(-1, 0, 0), 0.01f);
                if (coll.Length == 1 && coll[0].name.StartsWith("TallBox") && movementInput.x > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    (int, int, int) GetMapBounds()
    {
        GameObject map;
        string mapName;

        // Since P1Map and P2Map have 50 Xoffset
        if (transform.position.x < 25) // Belongs to P1Map
        {
            mapName = "P1Map";
        }
        else // Belongs to P2Map
        {
            mapName = "P2Map";
        }

        map = GameObject.Find(mapName);
        MapGenerator mapGenerator = map.GetComponent<MapGenerator>();
        int zLen = mapGenerator.room.Count;
        int xLen = mapGenerator.room[0].Count;
        int xOff = mapGenerator.XOffset;

        return (zLen, xLen, xOff);
    }
}
