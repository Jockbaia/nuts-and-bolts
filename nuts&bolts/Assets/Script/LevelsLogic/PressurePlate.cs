using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Transform other;
    public Transform decal;
    public Transform logic;

    bool activated = false;

    void Update()
    {
        if (activated) return;
        if (!logic.gameObject.GetComponent<PadLogic>().highlighted) return;

        if (IsPressed() && IsPressedOther())
        {
            activated = true;
            decal.gameObject.SetActive(true);
        }
    }

    bool IsPressed()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, 0.4f);
        foreach (var c in coll)
        {
            if (c.name.StartsWith("Player")) return true;
        }

        return false;
    }

    bool IsPressedOther()
    {
        Collider[] coll = Physics.OverlapSphere(other.position, 0.4f);
        foreach (var c in coll)
        {
            if (c.name.StartsWith("Player")) return true;
        }

        return false;
    }
}
