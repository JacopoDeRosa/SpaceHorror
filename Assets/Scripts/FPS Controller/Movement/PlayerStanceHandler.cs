using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Movement
{
    public class PlayerStanceHandler : MonoBehaviour
    {
        [Header("Components:")]
        [SerializeField] private CharacterControllerData _controllerData;

        [Header("Data:")]
        [SerializeField] private Stance _currentStance;

        private PlayerInput _input;
        private bool _wantsToSprint;
        private bool _wantsToCrouch;

        public Stance CurrentStance { get => _currentStance; }

        public delegate void StanceChangeHandler(Stance stance);

        public event StanceChangeHandler onStanceChange;

        private void Start()
        {
            SetStance(Stance.Standing);

            _input = FindObjectOfType<PlayerInput>();

            if(_input)
            {
                _input.actions["Crouch"].started += OnCrouchStart;
                _input.actions["Crouch"].canceled += OnCrouchEnd;
                _input.actions["Sprint"].started += OnSprintStart;
                _input.actions["Sprint"].canceled += OnSprintEnd;
            }
        }

        private void OnDestroy()
        {
            if (_input)
            {
                _input.actions["Crouch"].started -= OnCrouchStart;
                _input.actions["Crouch"].canceled -= OnCrouchEnd;
                _input.actions["Sprint"].started -= OnSprintStart;
                _input.actions["Sprint"].canceled -= OnSprintEnd;
            }
        }


        private void Update()
        {
            if (_controllerData.ControlsLocked) return;
            ControlStance();
        }

        private void ControlStance()
        {
           
            if (_controllerData.CrouchForced || _wantsToCrouch && _controllerData.CanCrouch)
            {
                SetStance(Stance.Prone);
                return;
            }

            else if (_controllerData.CanSprint && _wantsToSprint)
            {
                SetStance(Stance.Running);
            }

            else
            {
                SetStance(Stance.Standing);
            }
        }

        private void SetStance(Stance stance)
        {
            if (_currentStance == stance) return;
            _currentStance = stance;
            onStanceChange?.Invoke(stance);
        }

        private void OnCrouchStart(InputAction.CallbackContext context)
        {
            _wantsToCrouch = true;
        }

        private void OnCrouchEnd(InputAction.CallbackContext context)
        {
            _wantsToCrouch = false;
        }

        private void OnSprintStart(InputAction.CallbackContext context)
        {
            _wantsToSprint = true;
        }

        private void OnSprintEnd(InputAction.CallbackContext context)
        {
            _wantsToSprint = false;
        }

    }
}

