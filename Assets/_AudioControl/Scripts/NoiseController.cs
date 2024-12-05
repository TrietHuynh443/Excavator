using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public struct AudioSourceData
{
    public string name;

    public AudioSource audioSource;

}
public class NoiseController : MonoBehaviour
{
    [SerializeField] private TaskInfoSO _taskInfoSO;
    [SerializeField] private OnTaskTriggerSO _onTaskTriggerSO;
    [SerializeField] AudioSourceData[] _audioSourceData;



    public void MakeNoise(bool isQuiet, string name)
    {
        if (isQuiet)
            _taskInfoSO.StopSound(name);
        else
            StartCoroutine(_taskInfoSO.PlaySound(name));
    }

    private void Start()
    {
    }

    public void HandleTask(TaskInfo info)
    {
        MakeNoise(info.IsEnviromentQuiet, "Enviroment");
        MakeNoise(info.IsExcavatorQuiet, "Excavator");
    }

    private void OnEnable()
    {
        foreach (var data in _audioSourceData)
        {
            _taskInfoSO.SetSource(data.name, data.audioSource);
        }
        _onTaskTriggerSO.AddListener(HandleTask);
    }


    private void OnDisable()
    {
        _onTaskTriggerSO.RemoveListener(HandleTask);
    }
}