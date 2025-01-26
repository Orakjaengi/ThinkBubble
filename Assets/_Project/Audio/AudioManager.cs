using UnityEngine;
using UnityEngine.Audio;
using AudioMixerType;
using System;
using UnityEditor;
using UnityEngine.InputSystem;

namespace AudioMixerType
{
    public enum AudioMixerGroupType
    {
        Master,
        BGM,
        SFX
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer audioMixer;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private GameObject audioPanel;

    [SerializeField] private AudioResource bgm;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for(int i = 0; i < Enum.GetValues(typeof(AudioMixerGroupType)).Length; i++)
        {
            if (Enum.IsDefined(typeof(AudioMixerGroupType), i))
            {
                AudioMixerGroupType type = (AudioMixerGroupType)i;

                if (PlayerPrefs.HasKey(type.ToString()))
                {
                    SetAudioVolume(type.ToString(), PlayerPrefs.GetFloat(type.ToString()));
                }
                else
                {
                    SetAudioVolume(type.ToString(), 0.5f);
                }
            }
               
        }

        PlayBGM(bgm);
    }

    private void Update()
    {
        if (audioPanel != null && Input.GetKeyDown(KeyCode.Escape))
        {
            audioPanel.SetActive(true);
        }
    }

    public void SetAudioVolume(string audioType, float volume)
    {
        audioMixer.SetFloat(audioType, Mathf.Log10(volume) * 20);
    }
    
    public void PlayBGM(AudioResource audioResource)
    {
        bgmSource?.Stop();
        bgmSource.resource = audioResource;
        bgmSource?.Play();
    }

    public void StopBGM()
    {
        bgmSource?.Stop();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource?.PlayOneShot(audioClip);
    }

    public void StopSFX()
    {
        sfxSource?.Stop();
    }

    public void PlaySFXLoop(AudioResource audioResource)
    {
        sfxSource.resource = audioResource;
        sfxSource.loop = true;
        sfxSource?.Play();
    }

    public void StopSFXLoop()
    {
        sfxSource.loop = false;
    }
}