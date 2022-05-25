using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingPanel;
    public CanvasGroup canvasGroup;  

    // Start is called before the first frame update
    void Start()
    {
         DontDestroyOnLoad(gameObject);
         DontDestroyOnLoad(loadingPanel.transform.parent);
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
