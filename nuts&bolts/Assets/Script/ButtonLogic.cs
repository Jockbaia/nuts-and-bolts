using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public bool isActive = false;

    GameObject player;

    bool playOnce = true;

    void Awake()
    {
        // Determine if in map1 or map2 to know which player to listen to
        player = GameObject.Find(transform.parent.name == "P1Map" ? "Player1" : "Player2");
    }

    void Update()
    {
        if (IsPressed())
        {
            isActive = true;

            if (playOnce)
            {
                var clip = player.GetComponent<PlayerLogic>().clipActiveBtn;
                player.GetComponent<AudioSource>().PlayOneShot(clip);
                playOnce = false;
            }
        }
        else
        {
            isActive = false;
            playOnce = true;
        }
    }

    bool IsPressed()
    {
        if (player.transform.position.x == transform.position.x
            && player.transform.position.z == transform.position.z
            && player.transform.position.y <= 1f)
        {
            return true;
        }

        Collider[] coll = Physics.OverlapSphere(new Vector3(transform.position.x, 1f, transform.position.z), 0.01f);
        if (coll.Length == 1 &&
            (coll[0].name.StartsWith("TallBox") || coll[0].name.Equals("MagneticBox")))
        {
            return true;
        }

        return false;
    }
}
