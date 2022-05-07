using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManageCoop : MonoBehaviour
{
    public Vector3 player1StartingPos = new Vector3(6, 1, 0);
    public Vector3 player2StartingPos = new Vector3(56, 1, 0);

    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = gameObject.GetComponent<PlayerInputManager>();

        var p1 = playerInputManager.JoinPlayer(0, -1, null);
        p1.transform.position = player1StartingPos;

        var p2 = playerInputManager.JoinPlayer(1, -1, null);
        p2.transform.position = player2StartingPos;
    }
}
