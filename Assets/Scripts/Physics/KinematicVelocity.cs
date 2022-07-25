using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KinematicPhysics
{
    public class KinematicVelocity : MonoBehaviour
    {
        [SerializeField] private bool _calculateVelocity;
        [SerializeField] private bool _calculateAngularVelocity;

        private Vector3 _velocity;
        private Vector3 _angularVelocity;

        private Vector3 _lastPosition;
        private Vector3 _lastAngle;

        public Vector3 Velocity { get => _velocity; }
        public Vector3 AngularVelocity { get => _angularVelocity; }

        public Vector3 TransformedVelocity { get => transform.TransformDirection(_velocity); }

        private void Awake()
        {
            _lastPosition = transform.position;
            _lastAngle = transform.rotation.eulerAngles;
        }

        private void FixedUpdate()
        {
            if (transform.hasChanged == false) return;
            CalculateVelocity();
            CalculateAngularVelocity();
        }

        private void CalculateVelocity()
        {
            if (_calculateVelocity == false) return;

            _velocity = (transform.position - _lastPosition) / Time.fixedDeltaTime;
            _lastPosition = transform.position;
        }
        private void CalculateAngularVelocity()
        {
            if (_calculateAngularVelocity == false) return;

            _angularVelocity = (transform.rotation.eulerAngles - _lastAngle) / Time.fixedDeltaTime;
            _lastAngle = transform.rotation.eulerAngles;
        }
    }
}
