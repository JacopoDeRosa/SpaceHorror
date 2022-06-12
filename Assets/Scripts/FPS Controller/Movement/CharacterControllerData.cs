﻿using UnityEngine;
using GizmosExtensions;
using UnityEngine.InputSystem;
using System.Collections;

namespace FPS.Movement
{
    public class CharacterControllerData : MonoBehaviour
    {
        [SerializeField] private PlayerStanceHandler _stanceHandler;

        [SerializeField] private bool _lockControls;
        [SerializeField] private float _sphereRadius = 0.39f;
        [SerializeField] private float _sphereDistance = 0.62f;

        [SerializeField] private float _crouchCheckerRadious = 0.35f;
        [SerializeField] private float _checkerHeight = 2f;
        [SerializeField] private float _checkerOffset = 0;

        [SerializeField] private float _inputHardness = 10;
        [SerializeField] private Vector3 _minInput;

        private PlayerInput _input;

        private Vector3 _targetPlanarInput;

        public Ray GroundCheckRay
        {
            get { return new Ray(transform.position + transform.up, -transform.up); }
        }
        public bool CanSprint
        {
            get { return _stanceHandler.CurrentStance != Stance.Prone; }
        }
        public bool CanCrouch
        {
            get { return _stanceHandler.CurrentStance != Stance.Running && IsGrounded; }
        }
        public bool IsGrounded
        {
            get { return Physics.SphereCast(GroundCheckRay, _sphereRadius, _sphereDistance, ~LayerMask.GetMask("Player")); }
        }
        public bool CrouchForced
        {
            get
            {
                Vector3 capsuleStart = transform.position + new Vector3(0, _crouchCheckerRadious + _checkerOffset, 0);
                Vector3 capsuleEnd = transform.position + new Vector3(0, _checkerHeight - _crouchCheckerRadious, 0);
                return Physics.CheckCapsule(capsuleStart, capsuleEnd, _crouchCheckerRadious, ~LayerMask.GetMask("Player"));
            }
        }
        public Vector3 PlanarInput { get; private set; }
        public bool ControlsLocked { get => _lockControls; }


        private void Start()
        {
            _input = FindObjectOfType<PlayerInput>();

            if (_input)
            {
                _input.actions["Move"].performed += OnMove;
            }
            
        }

        private void OnDestroy()
        {
            if (_input)
            {
                _input.actions["Move"].performed -= OnMove;
            }

        }

        private void Update()
        {
            float smooth = Mathf.Clamp01(_inputHardness * Time.deltaTime);
            PlanarInput = Vector3.Lerp(PlanarInput, _targetPlanarInput, smooth);
            if(_targetPlanarInput.magnitude == 0 && PlanarInput.magnitude < _minInput.magnitude)
            {
                PlanarInput = Vector3.zero;
            }
        }

        public void SetControlLock(bool value)
        {
            _lockControls = value;
        }

        public void LockControls()
        {
            SetControlLock(true);
        }

        public void UnlockControls()
        {
            SetControlLock(false);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 move = context.ReadValue<Vector2>();

            _targetPlanarInput = new Vector3(move.x, 0, move.y);
        }



        #region Draw Gizmos
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {

            Gizmos.color = Color.cyan;
            ExtendedGizmos.DrawWireCapsule(transform.position + new Vector3(0,_checkerOffset, 0), _crouchCheckerRadious, _checkerHeight);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GroundCheckRay.GetPoint(_sphereDistance), _sphereRadius);
        }
#endif
        #endregion

    }
}