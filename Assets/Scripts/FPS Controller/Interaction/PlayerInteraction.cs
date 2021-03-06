using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

namespace FPS.Interaction
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float _interactionRange;
        [SerializeField] private float _interactionTime;
        [SerializeField] private Transform _viewPivot;
        [SerializeField] private LayerMask _interactionMask;

        private PlayerInput _input;
        private IInteractable _target;

        public event Action<IInteractable> onSelected;
        public event Action<IInteractable> onDeSelected;

        private float _nextInteractionViable;
       
        private void Start()
        {
            _input = FindObjectOfType<PlayerInput>();

            if (_input)
            {
                _input.actions["Interact"].started += OnInteractDown;
                _input.actions["Interact"].canceled += OnInteractUp;
            }
        }

        private void OnDisable()
        {
            if (_input)
            {
                _input.actions["Interact"].started -= OnInteractDown;
                _input.actions["Interact"].canceled -= OnInteractUp;
            }
        }

        void Update()
        {
            SetTarget();
        }

        private void SetTarget()
        {
            Ray ray = new Ray(_viewPivot.position, _viewPivot.forward);

            if (UnityEngine.Physics.Raycast(ray, out RaycastHit hitInfo, _interactionRange, _interactionMask, QueryTriggerInteraction.Collide))
            {

                if (hitInfo.transform.TryGetComponent(out IInteractable interaction) && interaction != _target)
                {
                    _target = interaction;
                    _target.Select();
                    onSelected?.Invoke(_target);
                }

                else if(interaction == null)
                {
                    DeSelect();
                }
            }
            else
            {
                DeSelect();
            }
        }

        private void DeSelect()
        {
            if (_target != null)
            {
                _target.DeSelect();
                onDeSelected?.Invoke(_target);
                _target = null;
            }
        }

        private void Interact()
        {
            if (_target == null) return;

            _target.Interact(gameObject);
            _target = null; // Setting target to null prevents conflicts if target is destroyed in use        
        }

        private void OnInteractDown(InputAction.CallbackContext context)
        {
            _nextInteractionViable = Time.time + _interactionTime;
        }
        private void OnInteractUp(InputAction.CallbackContext context)
        {
            if (_nextInteractionViable >= Time.time)
            {
                Interact();
            }
        }
    }
}

