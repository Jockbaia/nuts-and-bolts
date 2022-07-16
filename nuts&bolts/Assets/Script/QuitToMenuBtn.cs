using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitToMenuBtn : MonoBehaviour
{
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        PlayerLogic.menuOpen = false;
        GameObject.Find("SceneManager").GetComponent<SceneLoader>().currentScene = "Menu";
        GameObject.Find("SceneManager").GetComponent<SceneLoader>().LoadSceneWrapper("Menu");
    }
}
