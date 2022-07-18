using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHints : MonoBehaviour
{
    public GameObject[] obstaclesToShow;
    public GameObject[] obstaclesToHide;

    public GameObject movingBox;
    public Vector3 desiredPosForBox;

    WallBtnLogic button;

    bool done = false;

    void Start()
    {
        button = GetComponent<WallBtnLogic>();
    }

    void Update()
    {
        if (done) return;

        if (button.isActive)
        {
            done = true;

            foreach (var obj in obstaclesToHide)
            {
                obj.transform.position += new Vector3(0f, -1.01f, 0f);
            }

            foreach (var obj in obstaclesToShow)
            {
                obj.transform.position += new Vector3(0f, 1.01f, 0f);
            }

            movingBox.transform.position = desiredPosForBox;
        }
    }
}
