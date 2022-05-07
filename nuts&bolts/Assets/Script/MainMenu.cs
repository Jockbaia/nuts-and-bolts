using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneLoader sceneManager;

    private void Start()
    {
        sceneManager = FindObjectOfType<SceneLoader>();
    }

    public void PlayGame()
    {
        sceneManager.GetComponent<SceneLoader>().LoadSceneWrapper("SampleScene");
    }

    public void QuitGame()
    {

        Application.Quit();
    }
}
