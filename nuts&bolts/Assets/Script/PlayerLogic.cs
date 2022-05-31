using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    // In-game Menù logic
    public static bool menuOpen = false;
    public static GameObject menuCanvas;

    [SerializeField]
    private float moveSpeed = 5f;

    public Vector2 movementInput = Vector2.zero;
    public bool specialAction = false;

    public Transform movePoint;

    MapGenerator mapGenerator;
    private int mapZlen = 0;
    private int mapXlen = 0;
    private int mapXoffset = 0;

    private void Awake()
    {
        menuCanvas = GameObject.Find("MenuCanvas");
    }

    private void Start()
    {
        movePoint = transform.Find("Player Move Point");
        movePoint.parent = null;

        var cam = transform.Find("Player Camera");
        cam.parent = null;

        (mapZlen, mapXlen, mapXoffset) = GetMapBounds();

        menuCanvas.SetActive(false);
    }

    public void OnESC(InputAction.CallbackContext context)
    {
        bool escPressed = context.action.triggered;

        if (escPressed)
        {
            if (menuOpen)
            {
                menuOpen = false;
                Time.timeScale = 1f;
            }
            else
            {
                menuOpen = true;
                Time.timeScale = 0f;
            }
            menuCanvas.SetActive(menuOpen);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (menuOpen) return;
        if (this.GetComponent<HandleNumpadNav>().padOpen) return;

        movementInput = context.ReadValue<Vector2>();
    }

    public void OnSpecialAction(InputAction.CallbackContext context)
    {
        if (menuOpen) return;
        if (this.GetComponent<HandleNumpadNav>().padOpen) return;

        specialAction = context.action.triggered;
    }

    void Update()
    {
        GetMapBounds();

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(movePoint.position, transform.position) == 0f)
        {
            if (movementInput.y > 0.9f)
            { // Move Up
                Vector3 vec = new Vector3(0f, 0f, 1f);

                if (!IsGrabbing()) transform.rotation = Quaternion.LookRotation(vec);

                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0
                    && transform.position.z < mapZlen - 1
                    && !isWall(transform.position.z + vec.z, transform.position.x + vec.x))
                {
                    movePoint.position += vec;
                }
            }
            else if (movementInput.y < -0.9)
            { // Move Down
                Vector3 vec = new Vector3(0f, 0f, -1f);

                if (!IsGrabbing()) transform.rotation = Quaternion.LookRotation(vec);

                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0
                    && transform.position.z > 0
                    && !isWall(transform.position.z + vec.z, transform.position.x + vec.x))
                {
                    movePoint.position += vec;
                }
            }
            else if (movementInput.x > 0.9f)
            { // Move Right
                Vector3 vec = new Vector3(1f, 0f, 0f);

                if (!IsGrabbing()) transform.rotation = Quaternion.LookRotation(vec);

                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0
                    && transform.position.x < mapXlen + mapXoffset - 1
                    && !isWall(transform.position.z + vec.z, transform.position.x + vec.x))
                {
                    movePoint.position += vec;
                }
            }
            else if (movementInput.x < -0.9f)
            { // Move Left
                Vector3 vec = new Vector3(-1f, 0f, 0f);

                if (!IsGrabbing()) transform.rotation = Quaternion.LookRotation(vec);

                if (Physics.OverlapSphere(transform.position + vec, 0.01f).Length == 0
                     && transform.position.x > mapXoffset
                     && !isWall(transform.position.z + vec.z, transform.position.x + vec.x))
                {
                    movePoint.position += vec;
                }
            }
        }
    }

    bool isWall(float z, float x)
    {
        if (Map(z, x) == 'W' || Map(z, x) == 'w' || Map(z, x) == '^')
            return true;
        return false;
    }

    char Map(float z, float x)
    {
        return mapGenerator.room[(int)z][(int)x - mapXoffset];
    }

    bool IsGrabbing() // Checks if player is grabbing a box
    {
        var selectedPower = transform.GetComponent<RobotPowers>().selectedPower;
        if ((selectedPower == RobotPowers.PowerSelector.PushPull || selectedPower == RobotPowers.PowerSelector.PushPullHeavy) && specialAction)
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

        GameObject map;
        map = GameObject.Find(mapName);
        mapGenerator = map.GetComponent<MapGenerator>();
        int zLen = mapGenerator.room.Count;
        int xLen = mapGenerator.room[0].Count;
        int xOff = mapGenerator.XOffset;

        return (zLen, xLen, xOff);
    }
}
