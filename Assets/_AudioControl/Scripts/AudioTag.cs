using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Serializable]
public class AudioData
{
    [SerializeField] private string name;
    private AudioClip _audioClip;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _defaultSoundClip;

    public AudioClip DefaultAudioClip => _defaultSoundClip;
    public AudioClip Clip
    {
        get => _audioClip;
        set
        {
            _audioClip = value;
            _audioClip.name = name;
        }
    }

    public AudioSource Source { get => _audioSource; set { _audioSource = value; } }

    public string Name => name;
}

