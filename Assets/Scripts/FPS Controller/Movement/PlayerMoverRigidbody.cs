using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GizmosExtensions;
using RaycastExtensions;

namespace FPS.Movement
{
    public class PlayerMoverRigidbody : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _movementCollider;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private PlayerStanceHandler _stanceManager;
        [SerializeField] private CharacterControllerData _controllerData;

        [SerializeField] private float _maxSpeed = 5;
        [SerializeField] private float _acceleration = 10;
        [SerializeField] private float _breakingForce = 10;
        [SerializeField] private float _inertiaDampenForce = 1.5f;

        [Tooltip("Stops performing speed realated actions if speed is under this value")]
        [SerializeField] private float _speedBias = 0.01f;
        [Tooltip("Max slope in degrees after which the controller starts lowering acceleration according to ground slope")]
        [SerializeField] private float _maxSlope = 30f;

        [SerializeField] private float _jumpForce = 15;
        [SerializeField] private float _jumpCooldown = 1;


        [SerializeField] private float _sprintMultiplier = 2;


        [SerializeField] private float _crouchMultiplier = 0.5f;
        [SerializeField] private float _crouchedHeight;
        [SerializeField] private float _normalHeight;


        private float _actualAcceleration;
        private float _actualMaxSpeed;
        private float _actualIntertiaDampen;
        private float _timeSinceLastJump;
        private bool _lockedControls;

        private Vector3 PlanarVelocity
        {
            get { return new Vector3(_rigidBody.velocity.x, 0, _rigidBody.velocity.z); }
        }


        private void Awake()
        {
            _stanceManager.onStanceChange += SwitchControllerValues;
        }
        private void Start()
        {
            _actualAcceleration = _acceleration;
            _actualMaxSpeed = _maxSpeed;
            _actualIntertiaDampen = _inertiaDampenForce;
        }

        private void FixedUpdate()
        {
            BreakPlayer();
            ClampPlayerSpeed();
            MovePlayer();
        }

        private void BreakPlayer()
        {
            if (_controllerData.IsCenterGrounded == false) return;
            if (_controllerData.PlanarInput.magnitude == 0)
            {
                _rigidBody.AddForce(-PlanarVelocity * _breakingForce);
            }
        }
        private void MovePlayer()
        {

            if (_lockedControls) return;
            // Divide acceleration by slope dampen to add a max slope variable to controller

            if (_controllerData.IsCenterGrounded == false) return;
            if (_controllerData.ControlsLocked) return;
            float slopeDampen = 1;
            float surfaceAngle = ExtendedRaycast.GetSurfaceAngle(transform.position + Vector3.up, Vector3.down, 1.5f, ~LayerMask.GetMask("Player"));
            if (surfaceAngle > _maxSlope)
            {
                slopeDampen = surfaceAngle / _maxSlope;
            }

            Debug.Log(_controllerData.PlanarInput.x);
            float moveX = _controllerData.PlanarInput.x * (_actualAcceleration / slopeDampen);
            float moveZ = _controllerData.PlanarInput.z * (_actualAcceleration / slopeDampen);
            Vector3 move = new Vector3(moveX, 0f, moveZ);
            move = Vector3.ClampMagnitude(move, _actualAcceleration);
            move = transform.TransformDirection(move);
            _rigidBody.AddForce(move);
        }
        private void ClampPlayerSpeed()
        {
            if (_controllerData.IsCenterGrounded == false) return;
            // Clamp rigidbody velocity by creating a zero sum vector when velocity is too high (while ignoring vertical velocity)
            if (PlanarVelocity.magnitude >= _actualMaxSpeed)
            {
                float breakForce = 1;
                if (PlanarVelocity.magnitude > 0)
                {
                    breakForce = PlanarVelocity.magnitude / _actualMaxSpeed;
                }

                _rigidBody.AddForce(-PlanarVelocity * (breakForce * _actualIntertiaDampen));
            }
        }

        private void Update()
        {
         //   Jump();
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _controllerData.IsCenterGrounded && _timeSinceLastJump > _jumpCooldown)
            {
                _timeSinceLastJump = 0;
                _rigidBody.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
            }
            else
            {
                _timeSinceLastJump += Time.deltaTime;
            }
        }
        private void SetPlayerHeight(float height)
        {
            _movementCollider.height = height;
            _movementCollider.center = new Vector3(0, height / 2, 0);
        }
        private void SwitchControllerValues(Stance stance)
        {
            switch (stance)
            {
                case Stance.Standing:
                    SetNormalMove();
                    break;

                case Stance.Running:
                    SetSprintMove();
                    break;

                case Stance.Prone:
                    SetCrouchMove();
                    break;
            }
        }
        private void SetSprintMove()
        {
            _actualAcceleration = _acceleration * _sprintMultiplier;
            _actualMaxSpeed = _maxSpeed * _sprintMultiplier;
            SetPlayerHeight(_normalHeight);
        }
        private void SetCrouchMove()
        {
            _actualAcceleration = _acceleration * _crouchMultiplier;
            _actualMaxSpeed = _maxSpeed * _crouchMultiplier;
            SetPlayerHeight(_crouchedHeight);
        }
        private void SetNormalMove()
        {
            _actualAcceleration = _acceleration;
            _actualMaxSpeed = _maxSpeed;
            SetPlayerHeight(_normalHeight);
        }
    }
}
