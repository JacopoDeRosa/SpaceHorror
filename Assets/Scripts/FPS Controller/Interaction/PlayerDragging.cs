    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



namespace FPS.Interaction
{
    public class PlayerDragging : MonoBehaviour
    {
        [SerializeField] private Transform _viewPivot;
        [SerializeField] private float _pickUpDistance = 4;
        [SerializeField] private float _holdingForce = 3;
        [SerializeField] private float _maxDistance = 5;
        [SerializeField] private float _holdingDistance = 1.5f;
        [SerializeField] private float _holdingSmoothing = 5;
        [SerializeField] private LayerMask _draggingMask;

        private Vector3 HoldingPoint { get => _viewPivot.position + (_viewPivot.forward * _holdingDistance); }
        private Vector3 _holdingPosition;
        private float _hSmooth;

        private PlayerInput _input;

        private Draggable _draggedObject;

        private void Start()
        {
            _input = FindObjectOfType<PlayerInput>();

            if (_input)
            {
                _input.actions["Interact"].started += OnInteractStart;
                _input.actions["Interact"].canceled += OnInteractEnd;
            }
        }

        private void OnDisable()
        {
            if (_input)
            {
                _input.actions["Interact"].started -= OnInteractStart;
                _input.actions["Interact"].canceled -= OnInteractEnd;
            }
        }

        private void LateUpdate()
        {
           if(_draggedObject)
           {
                DragTarget();
           }
        }

        private void DragTarget()
        {
            float distanceFromHold = Vector3.Distance(_draggedObject.transform.position, HoldingPoint);

            if (distanceFromHold > _maxDistance)
            {
                StopDragging();
                return;  
            }

            _hSmooth = Mathf.Clamp01(_holdingSmoothing * Time.deltaTime);
            _holdingPosition = Vector3.Lerp(_holdingPosition, HoldingPoint, _hSmooth);


            _draggedObject.DragController.Move(_holdingPosition - _draggedObject.transform.position)  ;
        }
        private void StopDragging()
        {
            if (_draggedObject != null)
            {
                _draggedObject.EndDrag();
                _draggedObject = null;
            }
        }

        private bool FoundValidTarget(Ray ray, out Draggable draggable)
        {
            draggable = null;
            if (_draggedObject != null) return false;
            return Physics.Raycast(ray, out RaycastHit hitInfo, _pickUpDistance, _draggingMask) && hitInfo.transform.TryGetComponent(out draggable);
        }

        private void OnInteractStart(InputAction.CallbackContext context)
        {
           Ray ray = new Ray(_viewPivot.position, _viewPivot.forward);
           if(FoundValidTarget(ray, out _draggedObject))
           {
                _draggedObject.StartDrag();
                _holdingPosition = _draggedObject.transform.position;
           }
        }

        private void OnInteractEnd(InputAction.CallbackContext context)
        {
            StopDragging();
        }      
    }
}

