using System;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[CreateAssetMenu(fileName = "DefaultAudioSoundSO", menuName = "ScriptableObjects/DefaultAudioSoundSO")]
public class DefaultAudioSoundSO : ScriptableObject
{
    [SerializeField] private AudioData[] _defaultSounds;

    public AudioData[] DefaultSounds => _defaultSounds;
}
