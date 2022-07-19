using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public AudioClip resumeSound;

    public void OnResume()
    {
        transform.GetComponent<AudioSource>().PlayOneShot(resumeSound);
        PlayerLogic.menuOpen = false;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
