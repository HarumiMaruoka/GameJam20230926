using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    //BGM
    [SerializeField] AudioSource _bgmAudioSource;
    //å¯â âπ
    [SerializeField] AudioSource _seAudioSource;

    public float BgmVolume
    {
        get
        {
            return _bgmAudioSource.volume;
        }
        set
        {
            _bgmAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    public float SeVolume
    {
        get
        {
            return _seAudioSource.volume;
        }
        set
        {
            _seAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    void Start()
    {
        GameObject soundManager = CheckOtherSoundManager();
        bool checkResult = soundManager != null && soundManager != gameObject;

        if (checkResult)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    GameObject CheckOtherSoundManager()
    {
        return GameObject.FindGameObjectWithTag("SoundController");Å@
    }

    public void PlayBgm(AudioClip clip)
    {
        _bgmAudioSource.clip = clip;

        if (clip == null)
        {
            return;
        }

        _bgmAudioSource.Play();
    }

    public void PlaySe(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        _seAudioSource.PlayOneShot(clip);
    }

}
