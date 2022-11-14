using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveWhenHiddenAI : MonoBehaviour
{
    [SerializeField] private GameObject[] _poses;
    [SerializeField] private LayerMask _occlusionMask;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _moveSpeed;

    private Camera _mainCamera;

    [SerializeField]
    private bool _visible;

    private bool _inFrustum, _occluded;

    private event System.Action onBecameVisible;
    private event System.Action onBecameInvisible;


    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        _occluded = CameraIsOccluded();
        _inFrustum = IsInFrustum();

        if (_occluded || _inFrustum == false)
        {
            if(_visible)
            {
                OnInvisible();
                _visible = false;
            }
        }
        else
        {
            if(_visible == false)
            {
                OnVisible();
                _visible = true;
            }
        }

        _agent.speed = _visible ? 0 : _moveSpeed;
    }

    private bool CameraIsOccluded()
    {
        return Physics.Linecast(transform.position + transform.up, _mainCamera.transform.position, _occlusionMask);
    }

    private bool IsInFrustum()
    {
        Vector3 screenPoint = _mainCamera.WorldToViewportPoint(transform.position + Vector3.up);
        return screenPoint.z >= -0.1 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    private void OnVisible()
    {
        onBecameVisible?.Invoke();
    }

    private void OnInvisible()
    {
        SetRandomPose();
        onBecameInvisible?.Invoke();
    }

    private void SetRandomPose()
    {
        foreach (GameObject pose in _poses)
        {
            pose.SetActive(false);
        }

        _poses[Random.Range(0, _poses.Length)].SetActive(true);

    }
}
