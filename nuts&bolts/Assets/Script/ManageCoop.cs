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

    public GameObject padCanvas1;
    public GameObject padCanvas2;

    //private PlayerInputManager playerInputManager;

    [HideInInspector]
    public PlayerInput player1;
    [HideInInspector]
    public PlayerInput player2;

    public GameObject playerPrefab;

    bool loadingNext = false;

    public bool isLastLevel = false;
    bool doneLastLevel = false;

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

    void Update()
    {
        if (loadingNext) return;

        if (isLastLevel) // Used to end last level
        {
            if (doneLastLevel) return;

            if (player1.transform.position.x > 27)
            {
                GameObject.Find("WallMovingP1").transform.position = new Vector3(27, 2, 2);
                var laserP1 = GameObject.Find("LaserP1");
                if (laserP1 != null) laserP1.SetActive(false);
            }

            if (player2.transform.position.x < 33)
            {
                GameObject.Find("WallMovingP2").transform.position = new Vector3(33, 2, 2);
                var laserP2 = GameObject.Find("LaserP2");
                if (laserP2 != null) laserP2.SetActive(false);
            }

            if (player1.transform.position.x > 27 && player2.transform.position.x < 33)
            { // END GAME
                doneLastLevel = true;

                StartCoroutine(TargetFollower.Shake(player1.camera, 0.15f, 0.4f)); //!
                StartCoroutine(TargetFollower.Shake(player2.camera, 0.15f, 0.4f)); //!

                GameObject.Find("Light2").SetActive(false);
                GameObject.Find("DangerLight").GetComponent<Light>().intensity = 50;

                GameObject.Find("SceneManager").GetComponent<SceneLoader>().LoadNextSceneWrap();
            }

            return;
        }

        bool door1Open = padCanvas1.GetComponent<PadManager>().displayText.text == "OK";
        bool door2Open = padCanvas2.GetComponent<PadManager>().displayText.text == "OK";

        if (door1Open && door2Open) // Check if stage is cleared
        {
            bool onExit1_x = player1.transform.position.x == GameObject.Find("P1Map/DoorP1").transform.position.x;
            bool onExit1_z = player1.transform.position.z == GameObject.Find("P1Map/DoorP1").transform.position.z;
            bool onExit2_x = player2.transform.position.x == GameObject.Find("P2Map/DoorP2").transform.position.x;
            bool onExit2_z = player2.transform.position.z == GameObject.Find("P2Map/DoorP2").transform.position.z;

            if (!(onExit1_x && onExit1_z && onExit2_x && onExit2_z)) return;

            loadingNext = true;

            var p1 = player1.GetComponent<RobotPowers>()._components;
            var p2 = player2.GetComponent<RobotPowers>()._components;

            SceneLoader._componentsP1.bolts = p1.bolts;
            SceneLoader._componentsP1.Larm = p1.Larm;
            SceneLoader._componentsP1.Rarm = p1.Rarm;
            SceneLoader._componentsP1.view = p1.view;
            SceneLoader._componentsP1.legs = p1.legs;
            SceneLoader._componentsP1.rocket = p1.rocket;

            SceneLoader._componentsP2.bolts = p2.bolts;
            SceneLoader._componentsP2.Larm = p2.Larm;
            SceneLoader._componentsP2.Rarm = p2.Rarm;
            SceneLoader._componentsP2.view = p2.view;
            SceneLoader._componentsP2.legs = p2.legs;
            SceneLoader._componentsP2.rocket = p2.rocket;

            GameObject.Find("SceneManager").GetComponent<SceneLoader>().LoadNextSceneWrap();
        }
    }
}
