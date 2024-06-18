using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public static VolumeSetting instance;
    public AudioMixer myMixer;
    public Button m_Btn;
    public Button sfx_Btn;

    private float mus;
    private float sfx;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        mus++;
        if (mus < 2)
        {
            AudioManager.Instance.m_Source.UnPause();
        }
        else
        {
            AudioManager.Instance.m_Source.Pause();
            mus = 0;
        }
    }
    public void SetSFXVolume()
    {
        sfx = 0.2f;
        myMixer.SetFloat("SFX", sfx);
        PlayerPrefs.SetFloat("sfxVolume", sfx);
    }
    public void LoadVolume()
    {
        mus = PlayerPrefs.GetFloat("musicVolume");
        sfx = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
    }
}
