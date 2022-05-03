using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Transform movePoint;

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
