using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManageCoop : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = gameObject.GetComponent<PlayerInputManager>();

        var p1 = playerInputManager.JoinPlayer(0, -1, null);
        p1.transform.position = new Vector3(6, 1, 0);

        var p2 = playerInputManager.JoinPlayer(1, -1, null);
        p2.transform.position = new Vector3(56, 1, 0);
    }
}
