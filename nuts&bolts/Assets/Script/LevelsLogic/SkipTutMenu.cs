using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTutMenu : MonoBehaviour
{
    public void SkipTutorial()
    {
        var loader = GameObject.Find("SceneManager").GetComponent<SceneLoader>();
        loader.currentScene = "Level0";
        loader.LoadNextSceneWrap();
    }

    public void PlayTutorial()
    {
        var loader = GameObject.Find("SceneManager").GetComponent<SceneLoader>();
        loader.currentScene = "Tutorial0";
        loader.LoadNextSceneWrap();
    }
}
