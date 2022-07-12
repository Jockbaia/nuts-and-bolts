using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class ManageCoop : MonoBehaviour
{
    public Vector3 player1StartingPos = new Vector3(6, 1, 0);
    public Vector3 player2StartingPos = new Vector3(56, 1, 0);

    public Material p1Material;
    public Material p2Material;

    //private PlayerInputManager playerInputManager;

    [HideInInspector]
    public static PlayerInput player1;
    [HideInInspector]
    public static PlayerInput player2;

    public GameObject playerPrefab;

    private void Awake()
    {
        //playerInputManager = gameObject.GetComponent<PlayerInputManager>();

        //var p1 = playerInputManager.JoinPlayer(0, -1, null);
        player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard", pairWithDevice: Keyboard.current);
        player1.transform.position = player1StartingPos;
        player1.gameObject.name = "Player1";

        player1.transform.Find("Model").GetComponent<Renderer>().materials[1].color = p1Material.color;

        //var p2 = playerInputManager.JoinPlayer(1, -1, null);
        player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
        player2.transform.position = player2StartingPos;
        player2.gameObject.name = "Player2";

        // Set color for P2
        Transform p2Model = player2.transform.Find("Model");
        p2Model.GetComponent<Renderer>().materials[1].color = p2Material.color;
        Transform p2Larm = p2Model.Find("Arm_Left");
        p2Larm.GetComponent<Renderer>().materials[0].color = p2Material.color;
        p2Larm.Find("Bottom").GetComponent<Renderer>().materials[0].color = p2Material.color;
        Transform p2Rarm = p2Model.Find("Arm_Right");
        p2Rarm.GetComponent<Renderer>().materials[0].color = p2Material.color;
        p2Rarm.Find("Bottom").GetComponent<Renderer>().materials[0].color = p2Material.color;
        Transform p2Legs = p2Model.Find("Legs");
        p2Legs.GetComponent<Renderer>().materials[0].color = p2Material.color;
        p2Legs.Find("Bottom").GetComponent<Renderer>().materials[0].color = p2Material.color;

        //TODO: error message in case there aren't at least 2 control devices available
        //TODO: in caso non ci siano 2 device di input -> WASD / IJKL
    }
}
