using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenuTBC : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("SceneManager").GetComponent<SceneLoader>().LoadNextSceneWrap();
    }
}
