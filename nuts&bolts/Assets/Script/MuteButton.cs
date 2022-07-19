using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    AudioSource musicSrc;
    bool muted = false;

    public Image sprite;
    public Sprite musicOn;
    public Sprite musicOff;

    void Start()
    {
        musicSrc = GameObject.Find("SceneManager").GetComponent<AudioSource>();
        muted = musicSrc.volume == 0.0f;

        if (muted)
        {
            sprite.sprite = musicOff;
        }
        else
        {
            sprite.sprite = musicOn;
        }
    }

    public void MuteUnmute()
    {
        if (muted)
        {
            musicSrc.volume = 0.3f;
            sprite.sprite = musicOn;
            muted = false;
        }
        else
        {
            musicSrc.volume = 0.0f;
            sprite.sprite = musicOff;
            muted = true;
        }
    }
    
}
