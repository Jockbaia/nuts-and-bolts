using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour
{
    AudioSource musicSrc;
    bool muted = false;

    void Start()
    {
        musicSrc = GameObject.Find("SceneManager").GetComponent<AudioSource>();
        muted = musicSrc.volume == 0.0f;
    }

    public void MuteUnmute()
    {
        if (muted)
        {
            musicSrc.volume = 0.3f;
            muted = false;
        }
        else
        {
            musicSrc.volume = 0.0f;
            muted = true;
        }
    }
    
}
