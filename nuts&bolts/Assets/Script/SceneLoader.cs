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

    // Audio
    [HideInInspector]
    public AudioSource audioSrc;
    public AudioClip musicTutorial;
    public AudioClip musicPowerupUI;
    public AudioClip musicLevel1;
    public AudioClip musicLevel2;
    public AudioClip musicLevel3;
    public AudioClip musicMenu;

    void Awake()
    {
        //TODO: Set them to zero when done debugging!
        _componentsP1.Larm = 0; // Max 3
        _componentsP1.Rarm = 0; // Max 4
        _componentsP1.legs = 0; // Max 5
        _componentsP1.view = 0; // Max 3
        _componentsP1.rocket = 0; // Max 3
        _componentsP1.bolts = 0;

        _componentsP2.Larm = 0;
        _componentsP2.Rarm = 0;
        _componentsP2.legs = 0;
        _componentsP2.view = 0;
        _componentsP2.rocket = 0;
        _componentsP2.bolts = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
         DontDestroyOnLoad(gameObject);
         DontDestroyOnLoad(loadingPanel.transform.parent);

        audioSrc = GetComponent<AudioSource>();
        audioSrc.loop = true;

        audioSrc.clip = musicMenu;
        audioSrc.Play();
    }

    public void LoadNextSceneWrap() //TODO: fix when finished debugging
    {
        if (currentScene == "Level3") // Restart from Menu
        {
            audioSrc.Stop();

            currentScene = "Menu";
            LoadSceneWrapper(currentScene);

            audioSrc.clip = musicMenu;
            audioSrc.Play();
        }
        else if (currentScene == "Menu")
        {
            audioSrc.Stop();

            currentScene = "Tutorial1"; // "Tutorial1"
            LoadSceneWrapper(currentScene);

            audioSrc.clip = musicTutorial;
            audioSrc.Play();
        }
        else if (currentScene == "Tutorial1")
        {
            currentScene = "Tutorial2";
            LoadSceneWrapper(currentScene);
        }
        else if (currentScene == "Tutorial2")
        {
            currentScene = "Tutorial3";
            LoadSceneWrapper(currentScene);
        }
        else if (currentScene == "Tutorial3")
        {
            currentScene = "Tutorial4";
            LoadSceneWrapper(currentScene);
        }
        else if (currentScene == "Tutorial4")
        {
            audioSrc.Stop();

            currentScene = "Tutorial5";
            LoadSceneWrapper(currentScene);

            audioSrc.clip = musicPowerupUI;
            audioSrc.Play();
        }
        else if (currentScene == "Tutorial5")
        {
            audioSrc.Stop();

            currentScene = "Tutorial6";
            LoadSceneWrapper(currentScene);

            audioSrc.clip = musicTutorial;
            audioSrc.Play();
        }
        else if (currentScene == "Tutorial6")
        {
            _componentsP1.Larm = 0; // Max 3
            _componentsP1.Rarm = 1; // Max 4
            _componentsP1.legs = 0; // Max 5
            _componentsP1.view = 0; // Max 3
            _componentsP1.rocket = 0; // Max 3
            _componentsP1.bolts = 0;

            _componentsP2.Larm = 0;
            _componentsP2.Rarm = 0;
            _componentsP2.legs = 0;
            _componentsP2.view = 0;
            _componentsP2.rocket = 3;
            _componentsP2.bolts = 0;
            currentScene = "Tutorial7";
            LoadSceneWrapper(currentScene);
        }
        // ========= //
        else if (currentScene == "Tutorial7" || currentScene == "Level0") // Last Tutorial
        {
            _componentsP1.Larm = 0; // Max 3
            _componentsP1.Rarm = 0; // Max 4
            _componentsP1.legs = 0; // Max 5
            _componentsP1.view = 1; // Max 3
            _componentsP1.rocket = 1; // Max 3
            _componentsP1.bolts = 0;

            _componentsP2.Larm = 0;
            _componentsP2.Rarm = 0;
            _componentsP2.legs = 0;
            _componentsP2.view = 1;
            _componentsP2.rocket = 1;
            _componentsP2.bolts = 0;

            audioSrc.Stop();

            currentLevel = 1;
            currentScene = "Level1";
            LoadSceneWrapper(currentScene);

            audioSrc.clip = musicLevel1;
            audioSrc.Play();
        }
        else if (currentScene == "BeforeLevel")
        {
            audioSrc.Stop();

            currentLevel++;
            currentScene = "Level" + currentLevel.ToString();
            LoadSceneWrapper(currentScene);

            if (currentLevel == 2)
                audioSrc.clip = musicLevel2;
            else if (currentLevel == 3)
                audioSrc.clip = musicLevel3;
            audioSrc.Play();
        }
        else if (currentScene.StartsWith("Level"))
        {
            audioSrc.Stop();

            currentScene = "BeforeLevel";
            LoadSceneWrapper(currentScene);

            audioSrc.clip = musicPowerupUI;
            audioSrc.Play();
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
