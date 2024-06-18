using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //public enum Sound
    //{
    //    BackGround,
    //    WheelSpin,
    //    RewardWheelSpin,
    //    Win,
    //    Lose,
    //    MoveScrew,
        
    //}

    [Header("------ Audio Source ------")]
    public AudioSource m_Source;
    public AudioSource sfx_Source;

    [Header("-------- Audio Clip --------")]
    public List<AudioClip> soundEff = new List<AudioClip>();
    public List<AudioClip> bgSounds = new List<AudioClip>();

    //private SoundAudioClip[] soundAudioClipList; 
    int random;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameSong();
    }

    // Update is called once per frame
    private void GameSong()
    {
        random = Random.Range(0, bgSounds.Count);
        m_Source.clip = bgSounds[random];
        m_Source.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        sfx_Source.PlayOneShot(clip);
    }
}

//[System.Serializable]
//public class SoundAudioClip
//{
//    public AudioManager.Sound sound;
//    public AudioClip audioclip;
//}
