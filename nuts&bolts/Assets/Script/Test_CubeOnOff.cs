using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CubeOnOff : MonoBehaviour
{
    [SerializeField]
    private GameObject trigger;

    void Update()
    {
        if (trigger.GetComponent<ButtonLogic>().isActive)
        {
            Debug.Log("Button Active");
        }
    }
}
