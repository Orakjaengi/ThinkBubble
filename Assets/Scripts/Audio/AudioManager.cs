using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer audioMixer;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

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

    public void SetAudioVolume(string audioType, float volume)
    {
        audioMixer.SetFloat(audioType, Mathf.Log10(volume) * 20);
    }
    
    public void PlayBGM(AudioResource audioResource)
    {
        bgmSource.Stop();
        bgmSource.resource = audioResource;
        bgmSource.Play();
    }

    public void PlaySFX(AudioResource audioResource)
    {
        sfxSource.resource = audioResource;
        sfxSource.Play();
    }
}