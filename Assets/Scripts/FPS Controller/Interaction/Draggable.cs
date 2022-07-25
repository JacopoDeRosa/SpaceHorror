using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicPhysics;



namespace FPS.Interaction
{
    [RequireComponent(typeof(Rigidbody), typeof(KinematicVelocity), typeof(CharacterController))]
    public class Draggable : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private CharacterController _dragController;
        [SerializeField] private KinematicVelocity _kinematicVelocity;

        public Rigidbody Rigidbody { get => _rigidbody; }
        public CharacterController DragController { get => _dragController; }
        public KinematicVelocity KinematicVelocity { get => _kinematicVelocity; }

        private void OnValidate()
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            if (_dragController == null) _dragController = GetComponent<CharacterController>();
            if (_kinematicVelocity == null) _kinematicVelocity = GetComponent<KinematicVelocity>();
        }
        private void Awake()
        {
            _kinematicVelocity.enabled = false;
            _dragController.enabled = false;
        }

        public void StartDrag()
        {
            foreach(Collider collider in _colliders)
            {
                collider.enabled = false;
            }

            _rigidbody.isKinematic = true;
            _kinematicVelocity.enabled = true;
            _dragController.enabled = true;
        }

        public void EndDrag()
        {
            foreach (Collider collider in _colliders)
            {
                collider.enabled = true;
            }

            _rigidbody.isKinematic = false;
            _rigidbody.velocity = _kinematicVelocity.Velocity;
            _kinematicVelocity.enabled = false;
            _dragController.enabled = false;
        }
    }
}

