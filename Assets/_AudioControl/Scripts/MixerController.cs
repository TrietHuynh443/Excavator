using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private string _volumeName;
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(_volumeName, 0.2f);
        _audioMixer.SetFloat(_volumeName, Mathf.Log10(_slider.value) * 20);
        _slider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged?.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat(_volumeName, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(_volumeName, volume);
    }
}
