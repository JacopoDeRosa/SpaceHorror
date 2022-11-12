using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveWhenHiddenAI : MonoBehaviour
{
    [SerializeField] private LayerMask _occlusionMask;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _moveSpeed;

    private Camera _mainCamera;

    [SerializeField]
    private bool _visible;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if(CameraIsOccluded())
        {
            _visible = false;
        }
        else
        {
            Vector3 screenPoint = _mainCamera.WorldToViewportPoint(transform.position + Vector3.up);
            _visible = screenPoint.z >= -0.1 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }

        _agent.speed = _visible ? 0 : _moveSpeed;
    }

    private bool CameraIsOccluded()
    {
        return Physics.Linecast(transform.position + transform.up, _mainCamera.transform.position, _occlusionMask);
    }
}
