using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightTransform : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 0.02f;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _targetCamera;
 

    void Update()
    {
       UpdateTransform();    
    }

    private void FixedUpdate()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(_targetCamera.rotation.eulerAngles.x, _player.rotation.eulerAngles.y, 0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed);
        
    }

    private void UpdateTransform()
    {
        transform.position = _targetCamera.position;
    }
}
