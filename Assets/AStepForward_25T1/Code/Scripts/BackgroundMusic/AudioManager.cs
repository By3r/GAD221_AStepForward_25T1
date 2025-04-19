using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Default Music")]
    public AudioClip defaultMusic; // Plays outside zones
    public float defaultVolume = 0.7f;

    [Header("Audio Source")]
    public AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudioSource();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetupAudioSource()
    {
        musicSource.clip = defaultMusic;
        musicSource.volume = defaultVolume;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Call this to change music (no fade)
    public void PlayNewMusic(AudioClip newClip, float volume)
    {
        if (newClip == null) return;

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    // Revert to default music
    public void PlayDefaultMusic()
    {
        PlayNewMusic(defaultMusic, defaultVolume);
    }
}
