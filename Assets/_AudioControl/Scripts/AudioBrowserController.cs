using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioBrowserController : MonoBehaviour
{
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;

    private Action<string> _onSuccessCb;

    private void Start()
    {
    }

    public void AddListenner(Action<string> cb)
    {
        _onSuccessCb += cb;
    }





}
