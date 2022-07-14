using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingPanel;
    public CanvasGroup canvasGroup;

    public static RobotPowers.components _componentsP1;
    public static RobotPowers.components _componentsP2;

    public string currentScene;
    int currentLevel = 0;

    void Awake()
    {
        //TODO: Set them to zero when done debugging!
        _componentsP1.Larm = 5;
        _componentsP1.Rarm = 1;
        _componentsP1.legs = 0;
        _componentsP1.view = 3;
        _componentsP1.rocket = 0;
        _componentsP1.bolts = 2;

        _componentsP2.Larm = 5;
        _componentsP2.Rarm = 1;
        _componentsP2.legs = 0;
        _componentsP2.view = 3;
        _componentsP2.rocket = 0;
        _componentsP2.bolts = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
         DontDestroyOnLoad(gameObject);
         DontDestroyOnLoad(loadingPanel.transform.parent);
    }

    public void LoadNextSceneWrap()
    {
        if (currentScene == "Menu")
        {
            currentScene = "SampleScene";//"Level" + currentLevel.ToString(); !!!!!
            LoadSceneWrapper(currentScene);
        }
        else if (currentScene == "BeforeLevel")
        {
            currentLevel++;
            currentScene = "Level" + currentLevel.ToString();
            LoadSceneWrapper(currentScene);
        }
        else if (currentScene.StartsWith("Level"))
        {
            currentScene = "BeforeLevel";
            LoadSceneWrapper(currentScene);
        }
    }

    public void ReloadCurrentScene() // Eg. used when Game Over
    {
        LoadSceneWrapper(currentScene);
    }

    public void LoadSceneWrapper (string sceneName) //quando chiamata si inserisce il nome della scena e si apre quella scena
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene (string sceneName)
    {
        float step = 0.01f;

        yield return StartCoroutine(LoadingScreenFadeIn(step));

        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return StartCoroutine(LoadingScreenFadeOut(step));
    }

    IEnumerator LoadingScreenFadeIn(float step)
    {
        canvasGroup.alpha = 0f;
        loadingPanel.SetActive(true);

        for (float alpha = canvasGroup.alpha; alpha <= 2.0f; alpha += step)
        {
            canvasGroup.alpha = Mathf.Min(alpha, 1.0f);
            yield return null;
        }
    }
    
    IEnumerator LoadingScreenFadeOut(float step)
    {
        //yield return new WaitForSeconds(1f);
        
        for (float alpha = canvasGroup.alpha; alpha >= -1.0f; alpha -= step)
        {
            canvasGroup.alpha = Mathf.Max(alpha, 0f); ;
            yield return null;
        }
        loadingPanel.SetActive(false);
    }
}
