using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLogic : MonoBehaviour
{
    public Material grey;
    public Material blue;
    public Material yellow;

    public Transform pad1;
    public Transform pad2;

    public string pin1;
    public string pin2;

    public float waitFor = 3f;
    float elapsed = 0f;

    [HideInInspector]
    public bool highlighted = false;

    public Transform decal1;
    public Transform decal2;
    public Transform decal4;
    public Transform decal9;

    public Transform decal0;
    public Transform decal3;
    public Transform decal8;
    public Transform decal5;

    void Start()
    {
        foreach (var c in pin1.ToCharArray())
        {
            var name = c.ToString();
            var btn = pad1.Find(name);
            var btnOther = pad2.Find(name);

            btn.gameObject.AddComponent<PressurePlate>();
            btn.GetComponent<PressurePlate>().other = btnOther;
            btn.GetComponent<PressurePlate>().logic = transform;

            if (name == "1") btn.GetComponent<PressurePlate>().decal = decal1;
            else if (name == "2") btn.GetComponent<PressurePlate>().decal = decal2;
            else if (name == "4") btn.GetComponent<PressurePlate>().decal = decal4;
            else if (name == "9") btn.GetComponent<PressurePlate>().decal = decal9;
        }

        foreach (var c in pin2.ToCharArray())
        {
            var name = c.ToString();
            var btn = pad2.Find(name);
            var btnOther = pad1.Find(name);

            btn.gameObject.AddComponent<PressurePlate>();
            btn.GetComponent<PressurePlate>().other = btnOther;
            btn.GetComponent<PressurePlate>().logic = transform;

            if (name == "0") btn.GetComponent<PressurePlate>().decal = decal0;
            else if (name == "3") btn.GetComponent<PressurePlate>().decal = decal3;
            else if (name == "8") btn.GetComponent<PressurePlate>().decal = decal8;
            else if (name == "5") btn.GetComponent<PressurePlate>().decal = decal5;
        }
    }

    void Update()
    {
        if (elapsed >= waitFor)
        {
            elapsed = 0f;
            HighlightBtn();
        }

        elapsed += Time.deltaTime;
    }

    void HighlightBtn()
    {
        if (highlighted)
        {
            foreach(Transform child in pad1)
            {
                child.GetComponent<Renderer>().material.SetColor("_Color", grey.color);
            }
            foreach(Transform child in pad2)
            {
                child.GetComponent<Renderer>().material.SetColor("_Color", grey.color);
            }
            highlighted = false;
        }
        else
        {
            foreach (Transform child in pad1)
            {
                if (pin1.Contains(child.name))
                {
                    child.GetComponent<Renderer>().material.SetColor("_Color", blue.color);
                }
                else if (pin2.Contains(child.name))
                {
                    child.GetComponent<Renderer>().material.SetColor("_Color", yellow.color);
                }
                else
                {
                    child.GetComponent<Renderer>().material.SetColor("_Color", blue.color);
                }
            }

            foreach (Transform child in pad2)
            {
                if (pin1.Contains(child.name))
                {
                    child.GetComponent<Renderer>().material.SetColor("_Color", blue.color);
                }
                else if (pin2.Contains(child.name))
                {
                    child.GetComponent<Renderer>().material.SetColor("_Color", yellow.color);
                }
                else
                {
                    child.GetComponent<Renderer>().material.SetColor("_Color", yellow.color);
                }
            }

            highlighted = true;
        }
    }
}
