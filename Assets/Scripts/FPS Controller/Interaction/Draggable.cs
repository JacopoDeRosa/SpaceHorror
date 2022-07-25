using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicPhysics;



namespace FPS.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class Draggable : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody { get => _rigidbody; }

        private void OnValidate()
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
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

