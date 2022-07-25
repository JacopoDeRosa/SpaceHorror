using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



namespace FPS.Interaction
{
    public class PlayerDragging : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _pickUpDistance = 4;
        [SerializeField] private float _maxDistance = 5;
        [SerializeField] private float _holdingDistance = 1.5f;
        [SerializeField] private float _liftingForce = 5;


        private RaycastHit _hitInfo;
        private Transform _activeTransform;
        private PlayerInput _input;

        private Draggable _activeObject;
        private Draggable _currentTarget;

        private void Start()
        {
            _input = FindObjectOfType<PlayerInput>();

            if (_input)
            {
                _input.actions["Interact"].started += OnInteract;
            }
        }

        private void OnDisable()
        {
            if (_input)
            {
                _input.actions["Interact"].started -= OnInteract;
            }
        }

        private void Update()
        {
            //clamp object distance

            if (_activeTransform != null)
            {
                if (Vector3.Distance(transform.position, _activeTransform.position) > _maxDistance)
                {
                    _activeObject.Drag(_camera.gameObject, _liftingForce, _holdingDistance);
                    _activeTransform = null;
                    _activeObject = null;
                }
            }

            SetTraget();
        }

        private void DragObject()
        {                 
                if(_currentTarget != null)
                {
                    _currentTarget.Drag(_camera.gameObject, _liftingForce, _holdingDistance);
                    _activeTransform = _hitInfo.transform;
                    _activeObject = _currentTarget;
                }
                else if(_currentTarget == null && _activeObject != null)
                {
                    _activeObject.Drag(_camera.gameObject, _liftingForce, _holdingDistance);
                    _activeTransform = null;
                    _activeObject = null;
                }
        }

        private void SetTraget()
        {
            
            Ray ray = new Ray(_camera.position + _camera.forward / 4, _camera.forward);

            if (CanSelect(out _hitInfo, ray))
            {
                _currentTarget = _hitInfo.transform.GetComponent<Draggable>();
                _currentTarget.Select();
            }
            else
            {
                if (_currentTarget != null)
                {
                    _currentTarget.DeSelect();
                    _currentTarget = null;
                }
            }

        }

        private bool CanSelect(out RaycastHit hitInfo, Ray ray)
        {
            return Physics.Raycast(ray, out hitInfo, _pickUpDistance) && hitInfo.transform.GetComponent<Draggable>() != null && _activeObject == null;
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            DragObject();
        }
    }
}

