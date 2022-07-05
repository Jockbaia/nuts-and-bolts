using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public bool isActive = false;

    GameObject player;

    void Awake()
    {
        // Determine if in map1 or map2 to know which player to listen to
        player = GameObject.Find(transform.parent.name == "P1Map" ? "Player1" : "Player2");
    }

    void Update()
    {
        if (player.transform.position.x == transform.position.x
            && player.transform.position.z == transform.position.z)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }
}