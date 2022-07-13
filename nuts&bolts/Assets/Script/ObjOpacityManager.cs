using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjOpacityManager : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    List<Opacity> currentlyInTheWay;
    List<Opacity> alreadyTransparent;

    float yDisplacement = 0.35f;

    private void Awake()
    {
        currentlyInTheWay = new List<Opacity>();
        alreadyTransparent = new List<Opacity>();
    }

    private void Update()
    {
        GetAllObjectsInTheWay();

        MakeObjectsSolid();
        MakeObjectsTransparent();
    }

    void GetAllObjectsInTheWay()
    {
        currentlyInTheWay.Clear();

        // PLAYER
        Vector3 cPos = playerCamera.transform.position;
        Vector3 pPos = transform.position + new Vector3(0f, yDisplacement, 0f);

        float cameraPlayerDistance = Vector3.Magnitude(cPos - pPos);
        Ray rayForward = new Ray(cPos, pPos - cPos);

        RaycastHit[] hits = Physics.RaycastAll(rayForward, cameraPlayerDistance);

        bool someHit = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out Opacity inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                    someHit = true;
                }
            }
        }
        
        if (someHit)
        {
            // -X
            cPos = playerCamera.transform.position;
            pPos = transform.position + new Vector3(-1f, yDisplacement, -1f);

            cameraPlayerDistance = Vector3.Magnitude(cPos - pPos);
            rayForward = new Ray(cPos, pPos - cPos);

            hits = Physics.RaycastAll(rayForward, cameraPlayerDistance);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.TryGetComponent(out Opacity inTheWay))
                {
                    if (!currentlyInTheWay.Contains(inTheWay))
                    {
                        currentlyInTheWay.Add(inTheWay);
                    }
                }
            }
            
            // +X
            cPos = playerCamera.transform.position;
            pPos = transform.position + new Vector3(1f, yDisplacement, -1f);

            cameraPlayerDistance = Vector3.Magnitude(cPos - pPos);
            rayForward = new Ray(cPos, pPos - cPos);

            hits = Physics.RaycastAll(rayForward, cameraPlayerDistance);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.TryGetComponent(out Opacity inTheWay))
                {
                    if (!currentlyInTheWay.Contains(inTheWay))
                    {
                        currentlyInTheWay.Add(inTheWay);
                    }
                }
            }
        }
    }

    void MakeObjectsTransparent()
    {
        for (int i = 0; i < currentlyInTheWay.Count; i++)
        {
            Opacity inTheWay = currentlyInTheWay[i];

            if (!alreadyTransparent.Contains(inTheWay))
            {
                if (inTheWay != null)
                {
                    inTheWay.MakeTransparent();
                    alreadyTransparent.Add(inTheWay);
                }
            }
        }
    }

    void MakeObjectsSolid()
    {
        for (int i = alreadyTransparent.Count - 1; i >= 0; i--)
        {
            Opacity wasInTheWay = alreadyTransparent[i];

            if (!currentlyInTheWay.Contains(wasInTheWay))
            {
                if (wasInTheWay != null)
                {
                    wasInTheWay.MakeSolid();
                    alreadyTransparent.Remove(wasInTheWay);
                }
            }
        }
    }
}
