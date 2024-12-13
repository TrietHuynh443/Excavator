using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnappingController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _index;
    private Transform _parent;
    private GameObject _shovel;
    private GameObject _snappingObject;
    [SerializeField] private GameObject _colliderHoldObject;
    [SerializeField] private float _tweenDuration = 0.55f;
    [SerializeField] private Ease _moveEase = Ease.Linear;
    [SerializeField] private Ease _rotateEase = Ease.Linear;
    private bool _isSnapping;

    private void Awake()
    {

        DOTween.SetTweensCapacity(500, 200);

    }
    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _index = int.Parse(gameObject.name);
        _parent = transform.parent;
    }

    public void Snapping(GameObject shovel, GameObject snappingObject)
    {
        _shovel = shovel;
        _snappingObject = snappingObject;
        _isSnapping = true;
    }

    private IEnumerator DoSnap(GameObject shovel, GameObject snappingObject)
    {
        _isSnapping = false;
        _rigidbody.excludeLayers = LayerMask.GetMask("Rocks"); 
        _snappingObject = snappingObject;
        var pos = _snappingObject.transform.position;
        var rot = _snappingObject.transform.rotation;
        var rollSequence = MakeRollToPositionAndRotation(pos, _tweenDuration);
        Tween rotateTween = _rigidbody.transform.DORotate(rot.eulerAngles, 0.15f, RotateMode.Fast).SetEase(_rotateEase);

        rotateTween.OnComplete(() => rotateTween.Kill());
        rollSequence.Join(rotateTween);
        rollSequence.Play();
        while (Vector3.Distance(_rigidbody.transform.position, snappingObject.transform.position) > 0.003f)
        {
            if (rollSequence.IsPlaying())
            {
                yield return null;
            }
            pos = _snappingObject.transform.position;
            rollSequence = MakeRollToPositionAndRotation(pos, 0.05f);
            rollSequence.Play();
        }
        
        yield return new WaitForEndOfFrame();

        
        transform.SetParent(shovel.transform);
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;


    }

    private Sequence MakeRollToPositionAndRotation(Vector3 targetPosition, float duration)
    {
        Sequence rollSequence = DOTween.Sequence();

        Tween moveTween = transform.DOMove(targetPosition, duration).SetEase(_moveEase);
        rollSequence.Append(moveTween);
        _rigidbody.excludeLayers = LayerMask.GetMask("Rocks");


        return rollSequence;
    }

    public void ResetRigidBody()
    {
        transform.SetParent(_parent);
        _rigidbody.useGravity = true;
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.excludeLayers = LayerMask.GetMask();
    }

    private void FixedUpdate()
    {
        if (_isSnapping)
        {
            StartCoroutine(DoSnap(_shovel, _snappingObject));
        }
    }
}

