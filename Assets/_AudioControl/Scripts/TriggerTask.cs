using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;

public class TriggerTask : MonoBehaviour
{
    [SerializeField] private TaskInfoSO _taskSO;
    [SerializeField] private OnTaskTriggerSO _onTaskTriggerSO;
    [SerializeField] private string _mainObjectTag = "Excavator";
    private ParticleSystem _circleFX;
    [SerializeField] private string _obstacleObjectTag = "Obstacle";
    [SerializeField] private string _shovelTag = "Shovel";
    private TaskInfo _task;
    private static Queue<string> _tasksQueue;
    private static int _count = 0;
    private static ETaskDescription _lastDescription;
    private static string _nextTask;
    private static Transform _targetDestination;
    public static Transform TargetDestination => _targetDestination;
    private static ParticleSystem currentParticle;
    private void OnEnable()
    {
        string name = gameObject.name;
        _circleFX = GetComponentInChildren<ParticleSystem>();
        _task = _taskSO.GetTask(name);
        if (_count == 0)
        {
            _tasksQueue = _taskSO.GetTaskNameQueue();
            _tasksQueue.TryPeek(out _nextTask);
            if (_nextTask == _task.Name)
            {
                _targetDestination = transform;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_lastDescription == ETaskDescription.LOADING || _lastDescription == ETaskDescription.UNLOADING)
        {
            return;
        }
        if (_tasksQueue.Count > 0 && _task.Name == _tasksQueue.Peek())
        {
            if (other.CompareTag(_mainObjectTag))
            {
                if (_circleFX != null)
                {
                    currentParticle?.Stop();
                    _circleFX.Play();
                    currentParticle = _circleFX;
                }
                RaiseEvent();
            }
        }
    }

    private void Update()
    {
        if (_nextTask == _task.Name)
        {
            _targetDestination = transform;
        }
    }

    //Lifting Task
    private void OnTriggerStay(Collider other)
    {
        if (_tasksQueue.Count > 0 && _task.Name == _tasksQueue.Peek())
        {
            if ((other.CompareTag(_shovelTag) && _lastDescription == ETaskDescription.LOADING) || (other.CompareTag(_obstacleObjectTag) && _lastDescription == ETaskDescription.UNLOADING))
            {
                RaiseEvent();
                currentParticle?.Stop();
                gameObject.SetActive(false);
            }
        }

    }

    private void RaiseEvent()
    {
        _tasksQueue.Dequeue();
        _tasksQueue.TryPeek(out _nextTask);
        if (string.IsNullOrEmpty(_nextTask)) _targetDestination = null;
        _lastDescription = _task.Description;
        _onTaskTriggerSO?.RaiseEvent(_task);
    }

}
