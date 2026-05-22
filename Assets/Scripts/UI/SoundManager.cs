using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;
    private AudioSource ambienceSource;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip backgroundAmbience;

    // Volume properties
    private float currentMusicVolume = 0.5f;
    private float currentSFXVolume = 0.5f;
    private float currentAmbienceVolume = 0.013f;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        ambienceSource = transform.GetChild(1).GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        // Load saved volumes
        currentMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        currentSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        currentAmbienceVolume = PlayerPrefs.GetFloat("AmbienceVolume", 0.2f);

        // Apply volumes
        SetMusicVolume(currentMusicVolume);
        SetSFXVolume(currentSFXVolume);
        SetAmbienceVolume(currentAmbienceVolume);

        // Start background music
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        // Start background ambience
        if (backgroundAmbience != null && ambienceSource != null)
        {
            ambienceSource.clip = backgroundAmbience;
            ambienceSource.loop = true;
            ambienceSource.Play();
        }
    }


    public void PlaySound(AudioClip _sound)
    {
        if (soundSource != null && _sound != null)
            soundSource.PlayOneShot(_sound, currentSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        currentMusicVolume = volume;
        if (musicSource != null)
            musicSource.volume = currentMusicVolume;
        PlayerPrefs.SetFloat("MusicVolume", currentMusicVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        currentSFXVolume = volume;
        if (soundSource != null)
            soundSource.volume = currentSFXVolume;
        PlayerPrefs.SetFloat("SFXVolume", currentSFXVolume);
        PlayerPrefs.Save();
    }

    public void SetAmbienceVolume(float volume)
    {
        currentAmbienceVolume = volume;
        if (ambienceSource != null)
            ambienceSource.volume = currentAmbienceVolume;
        PlayerPrefs.SetFloat("AmbienceVolume", currentAmbienceVolume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume() => currentMusicVolume;
    public float GetSFXVolume() => currentSFXVolume;
    public float GetAmbienceVolume() => currentAmbienceVolume;
}