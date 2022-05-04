using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Transform movePoint;

    private bool dragKeyPressed = false;

    void Awake()
    {
        BasicMovement.OnDragKeyPressed += OnDragKeyPressed;
    }

    void OnDestroy()
    {
        BasicMovement.OnDragKeyPressed -= OnDragKeyPressed;
    }

    private void OnDragKeyPressed(bool keyPressed)
    {
        dragKeyPressed = keyPressed;
    }

    // Start is called before the first frame update
    void Start()
    {
        movePoint = transform.Find("Box Move Point");
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (dragKeyPressed) // Drag Box around
        {
            if (Vector3.Distance(BasicMovement.playerPos, movePoint.position) == 1f &&
                Vector3.Distance(BasicMovement.playerMovePointPos, movePoint.position) == 2f)
            {
                // Direction detection
                int xOff = (int)(BasicMovement.playerPos.x - movePoint.position.x);
                int zOff = (int)(BasicMovement.playerPos.z - movePoint.position.z);

                if (CanBeDragged((int)movePoint.position.x + xOff, (int)movePoint.position.z + zOff, xOff, zOff))
                {
                    GameManager.instance.room[(int)movePoint.position.z][(int)movePoint.position.x] = '0';
                    movePoint.position += new Vector3(xOff, 0f, zOff);
                    GameManager.instance.room[(int)movePoint.position.z][(int)movePoint.position.x] = '1';
                }
            }
        }
        else // Push box around
        {
            // Collision detection
            if (BasicMovement.playerMovePointPos.x == movePoint.position.x &&
                BasicMovement.playerMovePointPos.z == movePoint.position.z)
            {
                // Direction detection
                int xOff = (int)(movePoint.position.x - BasicMovement.playerPos.x);
                int zOff = (int)(movePoint.position.z - BasicMovement.playerPos.z);

                if (CanBePushed((int)movePoint.position.x + xOff, (int)movePoint.position.z + zOff))
                {
                    GameManager.instance.room[(int)movePoint.position.z][(int)movePoint.position.x] = '0';
                    movePoint.position += new Vector3(xOff, 0f, zOff);
                    GameManager.instance.room[(int)movePoint.position.z][(int)movePoint.position.x] = '1';
                }
            }
        }
    }

    bool CanBeDragged(int x, int z, int xOff, int zOff)
    {
        int zLen = GameManager.instance.room.Count;
        int xLen = GameManager.instance.room[0].Count;

        // Boundary check
        if (xOff == -1 || xOff == 1)
        {
            if (x < xLen - 1 && x > 0)
                return true;
        }
        else if (zOff == -1 || zOff == 1)
        {
            if (z < zLen - 1 && z > 0)
                return true;
        }
        return false;
    }

    bool CanBePushed(int x, int z)
    {
        int zLen = GameManager.instance.room.Count;
        int xLen = GameManager.instance.room[0].Count;

        // Boundary check
        if ((x < xLen && x >= 0) && (z < zLen && z >= 0))
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
