using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleButtonHandler : MonoBehaviour
{
    [SerializeField] private Button _toggleBtn;
    [SerializeField] private GameObject _toggleContent;
    [SerializeField] private Image _iconImage;

    private bool _isToggle = true;

    private void OnEnable() => _toggleBtn.onClick.AddListener(PopupControl);

    private void OnDisable() => _toggleBtn.onClick?.RemoveListener(PopupControl);

    private void PopupControl()
    {
        _isToggle = !_isToggle;
        _toggleContent.SetActive(_isToggle);
        _iconImage.rectTransform.Rotate(new Vector3(0, 180, 0));
    }
}
