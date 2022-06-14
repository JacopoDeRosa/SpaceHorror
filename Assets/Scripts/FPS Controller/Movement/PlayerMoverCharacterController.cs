using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Movement
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerStanceHandler))]

    public class PlayerMoverCharacterController : MonoBehaviour
    {
        [Header("Components:")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _groundOrientation;
        [SerializeField] private CharacterControllerData _controllerData;
        [SerializeField] private CharacterControllerWrapper _controllerWrapper;
        [SerializeField] private PlayerStanceHandler _stanceManager;

        [Header("Speed settings:")]
        [SerializeField] private float _acceleration = 2f;
        [SerializeField] private float _maxWalkSpeed = 5f;
        [SerializeField] private float _maxSprintSpeed = 10f;
        [SerializeField] private float _maxCrouchSpeed = 2f;


        [Header("Jump Settings:")]
        [SerializeField] private float _jumpForce = 16f;
        [SerializeField] private float _jumpRate = 0.7f;

        [Header("Stance Settings:")]
        [SerializeField] private float _normalHeight = 2f;
        [SerializeField] private float _crouchedHeight = 0.8f;

        private float _timeSinceLastJump;

        private PlayerInput _input;

        private bool CanJump
        {
            get { return _timeSinceLastJump >= _jumpRate && _controllerData.CrouchForced == false; }
        }

        void Awake()
        {
            if(!_characterController) _characterController = GetComponent<CharacterController>();
            _stanceManager.onStanceChange += SwitchControllerValues;     
        }

        private void Start()
        {
            _controllerWrapper.SetMaxSpeed(_maxWalkSpeed);

            _input = FindObjectOfType<PlayerInput>();
            if(_input)
            {
                _input.actions["Jump"].started += OnJump;
            }
        }

        private void OnDisable()
        {
            if (_input)
            {
                _input.actions["Jump"].started -= OnJump;
            }
        }

        private void FixedUpdate()
        {
            _timeSinceLastJump += Time.fixedDeltaTime;
            MovePlayer();
        }

        private void Jump()
        {
            if (CanJump)
            {
                _timeSinceLastJump = 0;
                _controllerWrapper.AddImpulse(new Vector3(0, _jumpForce, 0));
            }
        }

        // Change Speed and height based on stance enum
        private void SwitchControllerValues(Stance stance)
        {
            switch (stance)
            {
                case Stance.Standing:
                    _controllerWrapper.SetMaxSpeed(_maxWalkSpeed);
                    SetControllerHeight(_normalHeight);
                    break;

                case Stance.Running:
                    _controllerWrapper.SetMaxSpeed(_maxSprintSpeed);
                    break;

                case Stance.Prone:
                    _controllerWrapper.SetMaxSpeed(_maxCrouchSpeed);
                    SetControllerHeight(_crouchedHeight);
                    break;
            }
        }

        private void MovePlayer()
        {
            if (_controllerData.ControlsLocked) return;
            if (_controllerData.IsGrounded == false) return;
            _controllerWrapper.AddForce(_controllerData.PlanarInput * _acceleration);
        }

        private void SetControllerHeight(float height)
        {
            if(height <= _characterController.radius)
            {
                Debug.LogError("Warning character controller height can't be set lower than Radius*2");
                return;
            }
            _characterController.height = height;
            _characterController.center = new Vector3(0, height / 2, 0);
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            Jump();
        }
    }
}

