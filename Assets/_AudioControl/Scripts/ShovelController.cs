using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelController : MonoBehaviour
{
    [SerializeField] private GameObject[] _snappingObjects;

    public GameObject GetSnappingObject(int index)
    {
        if (_snappingObjects.Length > index || _snappingObjects.Length < 0) return null;
        return _snappingObjects[index];
    }
}
