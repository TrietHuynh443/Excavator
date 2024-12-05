using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DirectionUIHandler : MonoBehaviour
{
    [SerializeField] private Image _arrowImage;
    [SerializeField] private TextMeshProUGUI _descriptionHolder;
    [SerializeField] private TaskInfoSO _taskInfo;
    [SerializeField] private OnTaskTriggerSO _onTaskTrigger;
    private string _taskName;

    private void OnEnable()
    {
        _onTaskTrigger.AddListener(DisplayDescription);
    }

    private void OnDisable()
    {
        _onTaskTrigger.RemoveListener(DisplayDescription);
    }

    private void DisplayDescription(TaskInfo info)
    {
        (string, Sprite) des = _taskInfo.GetDescription(info.Description);
        _descriptionHolder.text = $"{info.Name}\n {des.Item1}";
        _arrowImage.sprite = des.Item2 as Sprite;
    }
}
