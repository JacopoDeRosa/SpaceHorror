using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _head;

    [SerializeField] private bool _followRotation;


    private void Update()
    {
        _camera.position = _head.position;
        if(_followRotation)
        {
            _camera.rotation = _head.rotation;
        }
    }


    public void FollowRotation()
    {
        _followRotation = true;
    }

    public void UnFollowRotation()
    {
        _followRotation = false;
        _camera.localRotation = Quaternion.identity;
    }
}

