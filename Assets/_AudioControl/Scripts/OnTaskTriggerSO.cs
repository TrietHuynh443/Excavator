using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OnTaskTriggerSO", menuName = "ScriptableObject/OnTaskTriggerSO", order = 0)]
public class OnTaskTriggerSO : ScriptableObject
{

    private Action<TaskInfo> _cb;

    private Queue<GameObject> _movingObjectQueue = new Queue<GameObject>();

    public void RaiseEvent(TaskInfo task)
    {
        //while (_movingObjectQueue.Count > 0)
        //{
        //    _movingObjectQueue.Dequeue().SetActive(false);
        //}
        _cb?.Invoke(task);
    }

    public void AddListener(Action<TaskInfo> cb)
    {
        _cb += cb;
    }
    public void RemoveListener(Action<TaskInfo> cb)
    {
        _cb -= cb;
    }

    public void AssignMovingObject(GameObject movingObject)
    {
        //_movingObjectQueue.Enqueue(movingObject);
    }

}
