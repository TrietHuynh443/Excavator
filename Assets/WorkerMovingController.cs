using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMovingController : MonoBehaviour
{
    [SerializeField] private OnTaskTriggerSO onTaskTriggerSO;
    [SerializeField] private string _taskName;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDistance;
    private bool _isMoving = false;
    private Vector3 _originalPos;

    private void OnEnable()
    {
        _originalPos = transform.position;
        onTaskTriggerSO.AddListener(Moving);
    }

    private void Moving(TaskInfo info)
    {
        if (_taskName == info.Name)
        {
            Debug.Log(info.Name);
            onTaskTriggerSO.AssignMovingObject(gameObject);
            _isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if ((transform.position - _originalPos).magnitude > _maxDistance)
        {
            _originalPos = transform.position;
            transform.Rotate(new Vector3(0, 180, 0));
        }
        //float speed = 0.08f;
        //if (_isMoving)
        //{
        //    speed = _speed;
        //}
        Vector3 dir = transform.forward;
        transform.position += _speed * Time.deltaTime * dir;
    }

    private void OnDisable()
    {
        onTaskTriggerSO.RemoveListener(Moving);
    }
}
