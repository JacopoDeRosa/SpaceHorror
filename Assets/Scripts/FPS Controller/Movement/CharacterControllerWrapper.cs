using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaycastExtensions;

namespace FPS.Movement
{
    public class CharacterControllerWrapper : MonoBehaviour
    {
        [Header("Components:")]
        [SerializeField] private CharacterControllerData _controllerData;
        [SerializeField] private CharacterController _target;
        [SerializeField] private Transform _groundOrientation;

        [Header("Settings:")]
        [SerializeField] private Vector3 _gravity;
        [SerializeField] private bool _useGravity;
        [SerializeField] private float _slopeLimit = 30f;
        [SerializeField] private float _maxSpeed = 5;
        [SerializeField] private float _stepOffset = 0.3f;
        [SerializeField] private float _stoppingPower;
        [SerializeField] private float _stoppingDead = 0.40f;

        [Header("Data:")]
        [SerializeField] private Vector3 _velocity;

        private Vector3 planarMoveOriented;

        public Vector3 Velocity { get => _velocity; }
        private Vector3 PlanarVelocity { get => new Vector3(_velocity.x, 0, _velocity.z); }

        public void AddForce(Vector3 force)
        {
            _velocity += force * Time.fixedDeltaTime;
        }
        public void AddImpulse(Vector3 impulse)
        {
            _velocity += impulse;
        }
        public void SetGravity(bool useGravity)
        {
            if (_useGravity == useGravity) return;

            _useGravity = useGravity;
            _velocity = new Vector3(_velocity.x, 0, _velocity.z);

        }
        public void GravityOn()
        {
            SetGravity(true);
        }
        public void GravityOff()
        {
            SetGravity(false);
        }

        public void SetMaxSpeed(float speed)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeMaxSpeed(speed, 0.5f));
        }

        private void FixedUpdate()
        {
            ChangeStepOffset();
            OrientMoverToSlope();
            AddGravity();
            ClampMaxSpeed();
            TransormPlanarMovement();
            MoveController();
        }
        private void ChangeStepOffset()
        {
            if (_controllerData.AllSidedsGrounded == false)
            {
                _target.stepOffset = 0;
            }
            else
            {
                _target.stepOffset = _stepOffset;
            }
        }
        private void MoveController()
        {
            Vector3 moveVector = new Vector3(0, _velocity.y, 0) + planarMoveOriented;
            _target.Move(moveVector * Time.fixedDeltaTime);
        }
        private void TransormPlanarMovement()
        {
            // Orient Movement to slope
            planarMoveOriented = _groundOrientation.TransformDirection(new Vector3(_velocity.x, 0, _velocity.z));

            // Stop player from moving onto slopes
            Ray ray = new Ray(transform.position + (planarMoveOriented.normalized * _target.radius) + Vector3.up, Vector3.down);
            if (ExtendedRaycast.GetSurfaceAngle(ray, 1.5f, ~LayerMask.GetMask("Player")) >= _slopeLimit) _velocity = new Vector3(_velocity.x, 0, _velocity.y);
        }
        private void ClampMaxSpeed()
        {
            if (_controllerData.IsCenterGrounded == false) return;
            _velocity = Vector3.ClampMagnitude(new Vector3(_velocity.x, 0, _velocity.z), _maxSpeed) + new Vector3(0, _velocity.y);
            if (_controllerData.PlanarInput.magnitude == 0)
            {
                if (Mathf.Abs(_velocity.x) > _stoppingDead || Mathf.Abs(_velocity.z) > _stoppingDead)
                {
                    AddForce(-new Vector3(_velocity.normalized.x, 0, _velocity.normalized.z) * _stoppingPower);
                }
                else
                {
                    _velocity = new Vector3(0, _velocity.y, 0);
                }
            }
        }
        private void AddGravity()
        {
            if (_useGravity && _controllerData.IsCenterGrounded == false)
            {
                AddForce(_gravity);
            }
            
            if(_velocity.y <= 0 && _controllerData.IsCenterGrounded == true)
            {
                _velocity = new Vector3(_velocity.x, 0, _velocity.z);
            }
        }
        private void OrientMoverToSlope()
        {
            RaycastHit hit;
            if (Physics.Raycast(_controllerData.GroundCheckRay, out hit, 1.5f, ~LayerMask.GetMask("Player")))
            {
                _groundOrientation.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }
        private IEnumerator ChangeMaxSpeed(float goal, float time)
        {
            
            if(_maxSpeed > goal)
            {
                float difference = _maxSpeed - goal;
                while (_maxSpeed > goal)
                {
                    _maxSpeed -= (difference / time) * Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
           
            }
            else if (_maxSpeed < goal)
            {
                float difference = goal - _maxSpeed;
                while (_maxSpeed < goal)
                {
                    _maxSpeed += (difference / time) * Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
            }
            _maxSpeed = goal;
        }
        
    }
}