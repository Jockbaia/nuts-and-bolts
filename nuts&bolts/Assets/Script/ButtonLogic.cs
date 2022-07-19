using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public bool isActive = false;
    public bool canBoxActivate = true;

    GameObject player;

    bool playOnce = true;

    void Awake()
    {
        // Determine if in map1 or map2 to know which player to listen to
        player = GameObject.Find(transform.parent.name == "P1Map" ? "Player1" : "Player2");
    }

    void Update()
    {
        if (player == null) return;

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
        Collider[] coll = Physics.OverlapSphere(transform.position, 0.4f);
        foreach (var c in coll)
        {
            if (c.name.StartsWith("Player")) return true;
            else if (c.name.StartsWith("TallBox") && canBoxActivate) return true;
            else if (c.name == "MagneticBox" && canBoxActivate) return true;
        }

        return false;
    }
}
