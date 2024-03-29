using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    // Audio playback logic
    public AudioSource audioSrc;
    public AudioClip clipDamage;
    public AudioClip clipBoltObtained;
    public AudioClip clipMagnetic;
    public AudioClip clipActiveBtn;
    public AudioClip clipDestroyNum;
    public AudioClip clipRocket;
    public AudioClip clipExtendArm;
    public AudioClip clipExtendLegs;

    // In-game Men? logic
    public static bool menuOpen = false;
    public static GameObject menuCanvas;

    [SerializeField]
    private float moveSpeed = 5f;

    public Vector2 movementInput = Vector2.zero;
    public bool specialAction = false;
    public bool interactBtn = false;

    public Transform movePoint;

    MapGenerator mapGenerator;
    private int mapZlen = 0;
    private int mapXlen = 0;
    private int mapXoffset = 0;

    //ROCKET
    private bool rocket = true;
    private float fuelRocketPosition = 2f;
    private float cooldownP1 = 0f;
    private bool restartRocket = true;
    private float reachMaxFuel = 10f;
    private float maxFuel = 10f;
    private bool playRocket = false;
    public Slider slider;

    //ARM
    public Animator animator;
    public bool isExtendingArm = false;

    //MOVE
    //public bool canMove = true;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
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
        if (isExtendingArm) return;

        movementInput = context.ReadValue<Vector2>();
    }

    public void OnSpecialAction(InputAction.CallbackContext context)
    {
        if (menuOpen) return;
        if (this.GetComponent<HandleNumpadNav>().padOpen) return;
        if (isExtendingArm)
        {
            specialAction = false;
            return;
        }

        // BUGFIX for boxes
        if (GetComponent<RobotPowers>().selectedPower.ToString().StartsWith("Push") &&
            !IsBoxProximity() &&
            Vector3.Distance(transform.position, movePoint.position) > 0f)
        {
            return;
        }
        //

        specialAction = context.action.triggered;

        // Animate push/pull
        if (GetComponent<RobotPowers>().selectedPower == RobotPowers.PowerSelector.PushPull ||
            GetComponent<RobotPowers>().selectedPower == RobotPowers.PowerSelector.PushPullHeavy)
        {
            if (specialAction)
            {
                transform.Find("Model/Arm_Left").localRotation = Quaternion.Euler(0, -70, 90);
                transform.Find("Model/Arm_Right").localRotation = Quaternion.Euler(0, 70, -90);
            }
            else
            {
                transform.Find("Model/Arm_Left").localRotation = Quaternion.Euler(0, -70, 0);
                transform.Find("Model/Arm_Right").localRotation = Quaternion.Euler(0, 70, 0);
            }
        }

    }
    
    bool IsBoxProximity() // BUGFIX for boxes
    {
        Collider[] colls = Physics.OverlapSphere(transform.position + transform.forward, 0.3f);
        foreach (var c in colls)
        {
            if (c.name.StartsWith("TallBox") && GetComponent<Legs>().canPush == true) return true;
        }
        return false;
    }

    public void OnInteractBtn(InputAction.CallbackContext context)
    {
        if (menuOpen) return;
        if (this.GetComponent<HandleNumpadNav>().padOpen) return;
        if (isExtendingArm) return;

        interactBtn = context.action.triggered;
    }

    void Update()
    {
        GetMapBounds();

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (movePoint.position.y == 1f)
        {
            if (fuelRocketPosition < 2f)
            {
                if (reachMaxFuel < maxFuel)
                {
                    reachMaxFuel += Time.deltaTime;
                    fuelRocketPosition += Time.deltaTime / 5;
                    restartRocket = true;

                }
                else
                {
                    fuelRocketPosition = 2f;
                    reachMaxFuel = 10f;
                }

            }
            else
            {
                restartRocket = true;
            }
        }

        // BUGFIX for boxes
        if (GetComponent<RobotPowers>().selectedPower.ToString().StartsWith("Push") &&  !IsBoxProximity())
        {
            if (gameObject.name == "Player1" && Keyboard.current.leftShiftKey.isPressed)
            {
                specialAction = false;
            }
            else if (gameObject.name == "Player2" && Keyboard.current.rightShiftKey.isPressed)
            {
                specialAction = false;
            }
        }
        if (GetComponent<RobotPowers>().selectedPower.ToString().StartsWith("Push") && !specialAction)
        {
            transform.Find("Model/Arm_Left").localRotation = Quaternion.Euler(0, -70, 0);
            transform.Find("Model/Arm_Right").localRotation = Quaternion.Euler(0, 70, 0);
        }
        //

        rocketUpdate();

        checkLeftArm();

        if (menuOpen) return;
        if (this.GetComponent<HandleNumpadNav>().padOpen) return;
        if (isExtendingArm) return;
        if (!GetComponent<Legs>().canMove) return;

        if (Vector3.Distance(movePoint.position, transform.position) == 0f)
        {
            if (movementInput.y > 0.9f)
            { // Move Up
                Vector3 vec = new Vector3(0f, 0f, 1f);

                if (!IsGrabbing()) transform.rotation = Quaternion.LookRotation(vec);

                if (checkIfFreeOrBolt(transform.position + vec)
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

                if (checkIfFreeOrBolt(transform.position + vec)
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

                if (checkIfFreeOrBolt(transform.position + vec)
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

                if (checkIfFreeOrBolt(transform.position + vec)
                     && transform.position.x > mapXoffset
                     && !isWall(transform.position.z + vec.z, transform.position.x + vec.x))
                {
                    movePoint.position += vec;
                }
            }
        }
    }

    bool checkIfFreeOrBolt(Vector3 vec)
    {
        Collider[] colliders = Physics.OverlapSphere(vec, 0.01f);
        if (colliders.Length == 0)
        {
            return true; // Free
        }
        else if (colliders.Length == 1 && colliders[0].gameObject.name.StartsWith("Bolt"))
        {
            return true;
        }
        else if (colliders.Length == 1)
        {
            var coll = colliders[0];
            if (coll.gameObject.name == "doorTop") return false;

            if (coll.gameObject.name.StartsWith("Door") && GetComponent<HandleNumpadNav>().correct.displayText.text == "OK")
            {
                return true;
            }
        }
        else if (colliders.Length > 1)
        {
            foreach (Collider coll in colliders)
            {
                if (coll.gameObject.name == "doorTop") return false;

                if (coll.gameObject.name.StartsWith("Door") && GetComponent<HandleNumpadNav>().correct.displayText.text == "OK")
                {
                    return true;
                }

                if (!coll.gameObject.name.StartsWith("Bolt")) return false;
            }
            return true;
        }
        return false;
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

    void rocketUpdate()
    {
        if (transform.GetComponent<RobotPowers>().selectedPower.ToString() != "Rocket")
        {
            if (GetComponent<Legs>().state == Legs.LegPos.None)
            {
                movePoint.position = new Vector3(movePoint.position.x, 1f, movePoint.position.z);
            }
            return;
        }

        if (this.gameObject.name == "Player1")
        {
            slider = GameObject.Find("PowerP1/Rocket/Slider").GetComponent<Slider>();
        }
        else
        {
            slider = GameObject.Find("PowerP2/Rocket/Slider").GetComponent<Slider>();
        }

        float x;

        if (movePoint.position.y == 1)
        {
            x = (reachMaxFuel / 10);
            slider.value = (float)x;
        }
        else
        {
            x = (fuelRocketPosition * 5 / 10);
            slider.value = (float)x;
        }

        if (specialAction)
        {
            GetComponent<Legs>().TookDamage();

            // Check collision with door
            string doorName = gameObject.name == "Player1" ? "P1Map/DoorP1" : "P2Map/DoorP2";
            var door = GameObject.Find(doorName);
            if (door != null)
            {
                if (door.transform.position.y >= 0)
                {
                    if (door.transform.position.x == transform.position.x && door.transform.position.z == transform.position.z)
                    {
                        return;
                    }
                }
            }
            //

            if (fuelRocketPosition > 0f)
            {
                fuelRocketPosition -= Time.deltaTime;
                reachMaxFuel -= Time.deltaTime * 5;
            }
            else
            {
                Collider[] coll = Physics.OverlapSphere(new Vector3(movePoint.position.x, 0.1f, movePoint.position.z), 0.01f);
                if (coll.Length == 1 && (
                    coll[0].name.StartsWith("TallBox") ||
                    coll[0].name.StartsWith("MagneticB") ||
                    coll[0].name.StartsWith("ExchB") ||
                    coll[0].name.StartsWith("Wall") //!
                    ))
                {
                    movePoint.position -= transform.forward;
                }

                reachMaxFuel = 0f;
                restartRocket = false;
                movePoint.position = new Vector3(movePoint.position.x, 1f, movePoint.position.z);
                rocket = false;
                playRocket = false;
            }

            if (rocket && restartRocket)
            {
                if (!playRocket)
                {
                    audioSrc.PlayOneShot(clipRocket);
                    playRocket = true;
                }

                movePoint.position = new Vector3(movePoint.position.x, 3f, movePoint.position.z);
            }
            else if (restartRocket)
            {
                if (movementInput.y > 0.9f && Physics.OverlapSphere(transform.position + new Vector3(0f, 0f, 1f), 0.01f).Length == 0
                    && transform.position.z < mapZlen - 1)
                {
                    if (cooldownP1 < 0.6)
                    {
                        cooldownP1 += Time.deltaTime;
                        return;
                    }
                    cooldownP1 = 0;
                    transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f));
                    movePoint.position += new Vector3(0f, 0f, 1f);
                }
                else if (movementInput.y < -0.9 && Physics.OverlapSphere(transform.position + new Vector3(0f, 0f, -1f), 0.01f).Length == 0
                        && transform.position.z > 0)
                {
                    if (cooldownP1 < 0.6)
                    {
                        cooldownP1 += Time.deltaTime;
                        return;
                    }
                    cooldownP1 = 0;
                    transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, -1f));
                    movePoint.position += new Vector3(0f, 0f, -1f);
                }
                else if (movementInput.x > 0.9f && Physics.OverlapSphere(transform.position + new Vector3(1f, 0f, 0f), 0.01f).Length == 0
                        && transform.position.x < mapXlen + mapXoffset - 1)
                {
                    if (cooldownP1 < 0.6)
                    {
                        cooldownP1 += Time.deltaTime;
                        return;
                    }
                    cooldownP1 = 0;
                    transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0f, 0f));
                    movePoint.position += new Vector3(1f, 0f, 0f);
                }
                else if (movementInput.x < -0.9f && Physics.OverlapSphere(transform.position + new Vector3(-1f, 0f, 0f), 0.01f).Length == 0
                        && transform.position.x > mapXoffset)
                {
                    if (cooldownP1 < 0.6)
                    {
                        cooldownP1 += Time.deltaTime;
                        return;
                    }
                    cooldownP1 = 0;
                    transform.rotation = Quaternion.LookRotation(new Vector3(-1f, 0f, 0f));
                    movePoint.position += new Vector3(-1f, 0f, 0f);
                }
            }
        }
        else
        {
            Collider[] coll = Physics.OverlapSphere(new Vector3(movePoint.position.x, 0.1f, movePoint.position.z), 0.01f);
            if (coll.Length == 1 && (
                coll[0].name.StartsWith("TallBox") ||
                coll[0].name.StartsWith("MagneticB") ||
                coll[0].name.StartsWith("ExchB")
                ))
            {
                
                movePoint.position -= transform.forward;
            }

            var legState = GetComponent<Legs>().state;
            if (legState == Legs.LegPos.None)
            {
                movePoint.position = new Vector3(movePoint.position.x, 1f, movePoint.position.z);
            }
            else if (legState == Legs.LegPos.Up)
            {
                movePoint.position = new Vector3(movePoint.position.x, 1f + Legs.upValue, movePoint.position.z);
            }
            else if (legState == Legs.LegPos.Down)
            {
                movePoint.position = new Vector3(movePoint.position.x, Legs.dwValue, movePoint.position.z);
            }
            
            rocket = true;
            playRocket = false;
        }
    }

    void checkLeftArm()
    {
        if (movementInput.x != 0 || movementInput.y != 0) return;

        if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "ArmExtend" && specialAction && !isExtendingArm)
        {
            animator.enabled = true;
            Collider[] collisions = Physics.OverlapSphere(
                transform.position + transform.forward + Vector3.up, 0.49f,
                LayerMask.GetMask("Default"));
            if (collisions.Length > 0) return;

            isExtendingArm = true;
            animator.SetBool("startLeftArm", true);
            transform.Find("Model/Arm_Left/Bottom").GetComponent<SphereCollider>().enabled = true;
            StartCoroutine("StartLeftArm");
            audioSrc.PlayOneShot(clipExtendArm);
            specialAction = false;
        }
        else if (transform.GetComponent<RobotPowers>().selectedPower.ToString() != "ArmExtend")
        {
            animator.enabled = false;
        }
    }

    public IEnumerator StartLeftArm()
    {
        yield return new WaitForSeconds(1.50f);
        transform.Find("Model/Arm_Left/Bottom").GetComponent<SphereCollider>().enabled = false;
        animator.SetBool("startLeftArm", false);
        isExtendingArm = false;
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
        if (gameObject.name == "Player1") // Belongs to P1Map
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
