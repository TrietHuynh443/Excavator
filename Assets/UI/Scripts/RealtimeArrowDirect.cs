using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeArrowDirect : MonoBehaviour
{
    private Transform _targetObject;

    private void Start()
    {
        _targetObject = TriggerTask.TargetDestination;
    }

    private void Update()
    {
        _targetObject = TriggerTask.TargetDestination;
        if (_targetObject == null) gameObject.SetActive(false);
        transform.LookAt(_targetObject);
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.eulerAngles.z));
    }
}
