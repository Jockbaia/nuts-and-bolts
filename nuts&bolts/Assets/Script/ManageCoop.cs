using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class ManageCoop : MonoBehaviour
{
    public Vector3 player1StartingPos = new Vector3(6, 1, 0);
    public Vector3 player2StartingPos = new Vector3(56, 1, 0);

    //private PlayerInputManager playerInputManager;

    [HideInInspector]
    public PlayerInput player1;
    [HideInInspector]
    public PlayerInput player2;

    public GameObject playerPrefab;

    private void Awake()
    {
        //playerInputManager = gameObject.GetComponent<PlayerInputManager>();

        //var p1 = playerInputManager.JoinPlayer(0, -1, null);
        player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard", pairWithDevice: Keyboard.current);
        player1.transform.position = player1StartingPos;
        player1.gameObject.name = "Player1";

        //var p2 = playerInputManager.JoinPlayer(1, -1, null);
        player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
        player2.transform.position = player2StartingPos;
        player2.gameObject.name = "Player2";

        //TODO: error message in case there aren't at least 2 control devices available
        //TODO: in caso non ci siano 2 device di input -> WASD / IJKL
    }
}
