using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnappingController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _index;
    private GameObject _snappingObject;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _index = int.Parse(gameObject.name);
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Shovel")
        {
            Debug.Log($"{other.gameObject.name}");

            var shovel = other.gameObject.GetComponent<ShovelController>();
            if (_snappingObject != null)
            {
                _snappingObject = shovel.GetSnappingObject(_index);
                StartCoroutine(Snapping(shovel.gameObject, _snappingObject));

            }
        }
    }


    private IEnumerator Snapping(GameObject shovel, GameObject snappingObject)
    {
        var pos = _snappingObject.transform.position;
        var rot = _snappingObject.transform.rotation;
        var rollSequence = MakeRollToPositionAndRotation(snappingObject.transform, pos, rot.eulerAngles, 0.1f);
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        // Optionally, add a callback when animation completes
        rollSequence.OnComplete(() =>
        {
            transform.SetParent(shovel.transform);
        });
        rollSequence.Play();
        while(!rollSequence.IsComplete())
        {
            if (!(Vector3.Distance(transform.position, _snappingObject.transform.position) > 5f))
            {
                yield return null;
            }
            else
            {
                rollSequence.Kill();
                _rigidbody.useGravity = true;
                _rigidbody.isKinematic = false;
                break;
            }
        }
    }

    private Sequence MakeRollToPositionAndRotation(Transform target, Vector3 targetPosition, Vector3 targetRotation, float duration)
    {
        // Create a sequence to chain position and rotation animations
        Sequence rollSequence = DOTween.Sequence();

        // Add position animation
        rollSequence.Append(target.DOMove(targetPosition, duration).SetEase(Ease.InOutQuad));

        // Add rotation animation (Euler angles)
        rollSequence.Join(target.DORotate(targetRotation, duration, RotateMode.FastBeyond360).SetEase(Ease.InOutQuad));

        return rollSequence;
    }
}
