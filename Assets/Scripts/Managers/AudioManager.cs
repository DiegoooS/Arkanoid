using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        SetInstance();
        SetAudioSource();
    }

    private void SetAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void SetInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void PlayGameSoundtrack()
    {
        audioSource.Play();
    }

    public void StopGameSoundtrack()
    {
        audioSource.Stop();
    }
}
