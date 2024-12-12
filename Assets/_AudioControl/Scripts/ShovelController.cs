using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelController : MonoBehaviour
{
    [SerializeField] private GameObject[] _snappingObjects;
    private Dictionary<GameObject, bool> _checkSnapObjectDic = new();
    [SerializeField] private GameObject _excavatorCab;
    private bool _canDrop = false;
    private bool _isDropping;
    private int _objectSnappingCount = 0;
    private float _angle;
    [SerializeField] private Transform _shovel;

    public GameObject GetSnappingObject(int index)
    {
        if (_snappingObjects.Length <= index || _snappingObjects.Length < 0) return null;
        return _snappingObjects[index];
    }

    private void OnTriggerStay(Collider other)
    {
        if (_canDrop && _objectSnappingCount > 0) return;
       
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (!_checkSnapObjectDic.ContainsKey(other.gameObject))
            {
                _checkSnapObjectDic.Add(other.gameObject, false);

            }
            if (_checkSnapObjectDic[other.gameObject]) return;

            var rock = other.gameObject.GetComponent<ObjectSnappingController>();
            if (rock != null )
            {
                var index = int.Parse(other.gameObject.name);
                Debug.Log(index);
                rock.Snapping(gameObject, GetSnappingObject(index));
                _checkSnapObjectDic[other.gameObject] = true;
                ++_objectSnappingCount;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var check = _checkSnapObjectDic.TryGetValue(other.gameObject, out bool isAttach);
        if (other.gameObject.CompareTag("Obstacle") || check)
        {
            _checkSnapObjectDic[other.gameObject] = false;
            --_objectSnappingCount;

        }
    }

    private void Update()
    {
        if (_isDropping) return;
        // Calculate the anglebetween transform.up and Vector3.down
        _angle = _shovel.localRotation.eulerAngles.z;
     
        if (_angle >= 130)
        {
            _canDrop = true;
            Drop();
        }
        else
        {
            _canDrop = false;
        }

    }

    private void Drop()
    {
        _isDropping = true;
        foreach (var item in _checkSnapObjectDic)
        {
            if (item.Value)
            {
                Debug.Log(item.Key.name);
                item.Key.GetComponent<ObjectSnappingController>().ResetRigidBody();
            }
        }
        _isDropping = false;
    }



}
