using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public GameObject Target;
    public Vector3 pointA, pointB;
    private float RotationSpeed = 10f;
    public float speedTarget;
    public Transform laserOrigin, shotOrigin;

    public GameObject prefabBolt;

    private GameObject player;
    private bool hitit;
    private LineRenderer laserLine;
    private LineRenderer shotLine;

    private float time;

    void Awake()
    {
        GameObject p1 = GameObject.Find("Player1");
        GameObject p2 = GameObject.Find("Player2");
        float distP1 = Vector3.Distance(transform.position, p1.transform.position);
        float distP2 = Vector3.Distance(transform.position, p2.transform.position);
        player = distP1 < distP2 ? p1 : p2;

        player.tag = "Player";
        hitit = false;

        laserLine = this.laserOrigin.GetComponent<LineRenderer>();
        shotLine = this.shotOrigin.GetComponent<LineRenderer>();

        //this.laserLine.SetWidth(0.02f, 0.02f);
        this.laserLine.startWidth = 0.02f;
        this.laserLine.endWidth = 0.02f;
        //this.shotLine.SetWidth(0.05f, 0.05f);
        this.shotLine.startWidth = 0.05f;
        this.shotLine.endWidth = 0.05f;

        this.laserLine.material.color = Color.red;
        this.shotLine.material.color = Color.blue;

        shotLine.enabled = false;

        Target.transform.position = pointA;  

    }

    void Update()
    {         
        rotateToTarget();
        checkIntersection();
    }

    private void rotateToTarget() //"this" view direction follow a target "Target"
    {
        //Compute target direction
        Vector3 targetDirection = Target.transform.position - transform.position;
        //targetDirection.y = 0f;
        //targetDirection.Normalize();

        //Rotate toward target direction
        float rotationStep = RotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, transform.up);
    }

    IEnumerator Start()
    {             
        while (true)
        {
            do yield return null; while (MoveTowards(pointA));
            do yield return null; while (MoveTowards(pointB));
        }
    }

    bool MoveTowards(Vector3 targetPoint) //move "Target" between 2 point A and B
    {
        Target.transform.position = Vector3.MoveTowards(
            Target.transform.position,
            targetPoint,
            speedTarget * Time.deltaTime);
        return Target.transform.position != targetPoint;
    }

    private void checkIntersection() //check if the raycast intersect tha player tagged "player"
    {
        laserLine.SetPosition(0, laserOrigin.position);
        shotLine.SetPosition(0, shotOrigin.position);
       
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            if (hit.collider.tag == "Player" && !hitit)
            {
                //Shot
                shotLine.SetPosition(1, hit.point);
                laserLine.SetPosition(1, hit.point);
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

                loseBolt();
                hitit = true;
                StartCoroutine(ShootLaser());
            }
            else if (hit.collider.tag != "Player")
            {
                //laserLine.SetPosition(1, Target.transform.position);
                laserLine.SetPosition(1, hit.point);
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //Debug.Log("No Hit");
                hitit = false;
            }
        }
    }

    private void loseBolt() //the player loses a bolt
    {
        if (player.GetComponent<RobotPowers>()._components.Larm > 0 && player.GetComponent<RobotPowers>()._components.Larm >= player.GetComponent<RobotPowers>()._components.Rarm 
            && player.GetComponent<RobotPowers>()._components.Larm >= player.GetComponent<RobotPowers>()._components.legs && player.GetComponent<RobotPowers>()._components.Larm >= player.GetComponent<RobotPowers>()._components.view 
            && player.GetComponent<RobotPowers>()._components.Larm >= player.GetComponent<RobotPowers>()._components.rocket)
        {
            player.GetComponent<RobotPowers>()._components.Larm--;
            //Debug.Log("Larm: " + player.GetComponent<RobotPowers>()._components.Larm);
        }
        else if (player.GetComponent<RobotPowers>()._components.Rarm > 0 && player.GetComponent<RobotPowers>()._components.Rarm >= player.GetComponent<RobotPowers>()._components.legs 
            && player.GetComponent<RobotPowers>()._components.Rarm >= player.GetComponent<RobotPowers>()._components.view && player.GetComponent<RobotPowers>()._components.Rarm >= player.GetComponent<RobotPowers>()._components.rocket)
        {
            player.GetComponent<RobotPowers>()._components.Rarm--;
            //Debug.Log("Rarm: " + player.GetComponent<RobotPowers>()._components.Rarm);
        }
        else if (player.GetComponent<RobotPowers>()._components.view > 0 && player.GetComponent<RobotPowers>()._components.view >= player.GetComponent<RobotPowers>()._components.legs 
            && player.GetComponent<RobotPowers>()._components.view >= player.GetComponent<RobotPowers>()._components.rocket)
        {
            player.GetComponent<RobotPowers>()._components.view--;
            //Debug.Log("view: " + player.GetComponent<RobotPowers>()._components.view);
        }
        else if (player.GetComponent<RobotPowers>()._components.legs > 0 
            && player.GetComponent<RobotPowers>()._components.legs >= player.GetComponent<RobotPowers>()._components.rocket)
        {
            player.GetComponent<RobotPowers>()._components.legs--;
            //Debug.Log("legs: " + player.GetComponent<RobotPowers>()._components.legs);
        }
        else if (player.GetComponent<RobotPowers>()._components.rocket > 0)
        {
            player.GetComponent<RobotPowers>()._components.rocket--;
            //Debug.Log("rocket: " + player.GetComponent<RobotPowers>()._components.rocket);
        }
        else
        {
            player.GetComponent<RobotPowers>()._components.Larm--;
        }

        // Camera Shake
        if (player.name == "Player1")
        {
            ManageCoop.player1.camera.GetComponent<CameraFollow>().enabled = false;
            StartCoroutine(Shake(ManageCoop.player1.camera, 0.15f, 0.4f)); //!
        }
        else
        {
            ManageCoop.player2.camera.GetComponent<CameraFollow>().enabled = false;
            StartCoroutine(Shake(ManageCoop.player2.camera, 0.15f, 0.4f)); //!
        }

        // Spawn lost bolt
        StartCoroutine(loseBoltAnimation(0.5f, player, prefabBolt));

        // Damage sound
        player.GetComponent<PlayerLogic>().audioSrc.PlayOneShot(player.GetComponent<PlayerLogic>().clipDamage);

        checkGameOver();

        player.GetComponent<Legs>().TookDamage();
    }

    private void checkGameOver()
    {
        if(player.GetComponent<RobotPowers>()._components.Larm < 0 && player.GetComponent<RobotPowers>()._components.Rarm == 0 
            && player.GetComponent<RobotPowers>()._components.view == 0 && player.GetComponent<RobotPowers>()._components.legs == 0 
            && player.GetComponent<RobotPowers>()._components.rocket == 0)
        {
            //Debug.Log("GAME OVER");
            GameObject.Find("SceneManager").GetComponent<SceneLoader>().ReloadCurrentScene();
            Destroy(gameObject);
        }
    }

    IEnumerator ShootLaser()
    {
        shotLine.enabled = true;
        Target.transform.position = player.transform.position;
        Target.transform.position = new Vector3(Target.transform.position.x, 0, Target.transform.position.z);
        laserLine.enabled = false;
        
        yield return new WaitForSeconds(0.3f);

        shotLine.enabled = false;
        laserLine.enabled = true;
    }

    public static IEnumerator Shake(Camera cam, float duration, float magnitude)
    {
        Vector3 originalPos = cam.transform.position;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPos.x + UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            cam.transform.position = new Vector3(x, originalPos.y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cam.transform.position = originalPos;
        cam.GetComponent<CameraFollow>().enabled = true;
    }

    IEnumerator loseBoltAnimation(float duration, GameObject player, GameObject prefab)
    {
        float elapsedTime = 0;
        float ratio = elapsedTime / duration;

        Vector3 startPos = player.transform.position + new Vector3(0f, 2f, 0f);
        startPos.x = Mathf.Floor(startPos.x);
        startPos.z = Mathf.Floor(startPos.z);
        Vector3 endPos1 = new Vector3(startPos.x, 0f, startPos.z);

        GameObject bolt = Instantiate(prefab, startPos, Quaternion.Euler(Vector3.zero));
        bolt.GetComponent<BoxCollider>().enabled = false;

        while (ratio < 1f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / duration;
            bolt.transform.position = Vector3.Lerp(startPos, endPos1, ratio);
            bolt.transform.Rotate(0f, 4f, 0f);
            yield return null;
        }

        Destroy(bolt);
        //Instantiate(prefab, endPos1, Quaternion.Euler(Vector3.zero));
    }
}
