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

        public void StartDrag()
        {
            _rigidbody.useGravity = false;
        }

        public void EndDrag()
        {
           _rigidbody.useGravity = true;
        }
    }
}

