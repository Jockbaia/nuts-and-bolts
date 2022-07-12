using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalLoader : MonoBehaviour
{
    public Texture decalTex;

    void Awake()
    {
        GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", decalTex);
    }
}
