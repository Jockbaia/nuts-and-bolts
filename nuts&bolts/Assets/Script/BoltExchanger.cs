using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoltExchanger : MonoBehaviour
{
    public GameObject boltPrefab;
    public Transform other;

    public AudioClip clipSendBolt;
    public AudioClip clipError;

    Transform p1;
    Transform p2;

    AudioSource audioSrc;

    const float duration = 1f;
    float cooldown = duration;
    static bool sending = false;
    private float elapsed;

    void OnInteract()
    {
        if (PlayerLogic.menuOpen) return;

        if (cooldown < duration)
        {
            cooldown += Time.deltaTime;
            return;
        }
        if (sending) return;

        if (p1.GetComponent<PlayerLogic>().interactBtn &&
            Vector3.Distance(transform.position, p1.position) <= 1f)
        {
            cooldown = 0f;
            // P1 is sending
            if (p1.GetComponent<RobotPowers>()._components.bolts > 0 && OutputNotBlocked())
            {
                sending = true;
                p1.GetComponent<RobotPowers>()._components.bolts -= 1;
                audioSrc.PlayOneShot(clipSendBolt);

                Transform parent = GameObject.Find("P2Map").transform.Find("Generated Map");
                StartCoroutine(DropBolt(parent));
            }
            else
            {
                // Play error sound
                audioSrc.PlayOneShot(clipError);
            }
        }
        else if (p2.GetComponent<PlayerLogic>().interactBtn &&
                 Vector3.Distance(transform.position, p2.position) <= 1f)
        {
            cooldown = 0f;
            // P2 is sending
            if (p2.GetComponent<RobotPowers>()._components.bolts > 0 && OutputNotBlocked())
            {
                sending = true;
                p2.GetComponent<RobotPowers>()._components.bolts -= 1;
                audioSrc.PlayOneShot(clipSendBolt);

                Transform parent = GameObject.Find("P1Map").transform.Find("Generated Map");
                StartCoroutine(DropBolt(parent));
            }
            else
            {
                // Play error sound
                audioSrc.PlayOneShot(clipError);
            }
        }
    }

    bool OutputNotBlocked()
    {
        // Check if front of vending machine is not blocked
        Collider[] collisions = Physics.OverlapSphere(other.position + other.forward, 0.4f);

        if (collisions.Length > 0 && !collisions[0].name.StartsWith("Player"))
            return false;
  
        return true;
    }

    void Start()
    {
        p1 = GameObject.Find("Player1").transform;
        p2 = GameObject.Find("Player2").transform;

        audioSrc = GameObject.Find("UICanvas").GetComponent<AudioSource>();
    }

    void Update()
    {
        OnInteract();
    }

    IEnumerator DropBolt(Transform parent)
    {
        //Vector3 startPos = other.position;
        Vector3 endPos = other.position + other.forward;
        endPos.y = 0f;

        GameObject bolt = Instantiate(boltPrefab, endPos, Quaternion.Euler(Vector3.zero));
        bolt.transform.parent = parent;
        bolt.GetComponent<BoxCollider>().enabled = false;

        //TODO: animation?

        bolt.transform.position = endPos;
        bolt.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1f);
        sending = false;
    }
}
